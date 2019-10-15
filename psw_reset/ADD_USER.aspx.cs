using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Web.Configuration;
using EDEN_Encode;
using System.Net.Mail;
using System.Net;

namespace psw_reset
{
    public partial class ADD_USER : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string mac = Request.QueryString["mac"];
                string ip = Request.QueryString["ip"];
                string name = Request.QueryString["name"];
                lab_ip.Text = ip;
                lab_mac.Text = mac;
                lab_name.Text = name;
            }
        }

        protected void btn_open_Click(object sender, EventArgs e)
        {
            bool check = true;
            bool check2 = true;
            if (txb_name.Text!="" & txb_emp_Notice.Text != "" && txb_PC_addr.Text != "" && txb_PC_dep.Text != "" && txb_tel.Text != "" && txb_re.Text != "")
            {
                String word = txb_emp_Notice.Text;
                String[] substrings = word.Split(',');
                foreach (var substring in substrings)
                {
                    SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                    HrsConn.Open();
                    string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'";
                    SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (!dr2.HasRows)
                    {
                        check = false;
                    }
                    HrsConn.Close();
                }

                if (check == true)
                {
                    SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                    HrsConn.Open();
                    string sqlstring2 = @"SELECT  [clkserverid]
                                                      , [veryfied]
                                                      ,[clktp]
                                                      ,[proxy]
                                                      ,[ip]
                                                      ,[machinename]
                                                      ,[lastlogtime]
                                                      ,[sys_keyer]
                                                      ,[sys_mkeyer]
                                                      ,[sys_adate]
                                                      ,[sys_mdate]
                                                      ,[remark]
                                                  FROM [05200169].[dbo].[clocks] where mac='" + lab_mac.Text + "' order by clkserverid;";
                    SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        check2 = false;
                    }
                    HrsConn.Close();

                    if (check2 == true)
                    {
                        string note = txb_PC_dep.Text.Trim() + "_" + txb_PC_addr.Text.Trim() + "_"+txb_re.Text.Trim();
                        SqlConnection HRSConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                        HRSConn.Open();
                        string sqlstring = @"INSERT INTO clocks
                                                                                ([veryfied]
                                                                                  ,[clktp]
                                                                                  ,[proxy]
                                                                                  ,[ip]
                                                                                  ,[machinename]
                                                                                  ,[lastlogtime]
                                                                                  ,[sys_keyer]
                                                                                  ,[sys_mkeyer]
                                                                                  ,[sys_adate]
                                                                                  ,[sys_mdate]
                                                                                  ,[remark]
                                                                                ,user_list
                                                                                ,mac
                                                                                ,tel) 
                                                                                VALUES 
                                                                                (@veryfied
                                                                                ,@clktp
                                                                                ,@proxy
                                                                                ,@ip
                                                                                ,@machinename
                                                                                ,@lastlogtime
                                                                                ,@sys_keyer
                                                                                ,@sys_mkeyer
                                                                                ,@sys_adate
                                                                                ,@sys_mdate
                                                                                ,@remark
                                                                            ,@user_list
                                                                            ,@mac
                                                                            ,@tel);";
                        SqlCommand cmd = new SqlCommand(sqlstring, HRSConn);
                        cmd.Parameters.AddWithValue("@veryfied", "0");
                        cmd.Parameters.AddWithValue("@clktp", "1");
                        cmd.Parameters.AddWithValue("@proxy", lab_ip.Text.Trim());
                        cmd.Parameters.AddWithValue("@ip", lab_ip.Text.Trim());
                        cmd.Parameters.AddWithValue("@machinename", lab_name.Text);
                        cmd.Parameters.AddWithValue("@lastlogtime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@sys_keyer", "sys");
                        cmd.Parameters.AddWithValue("@sys_mkeyer", "sys");
                        cmd.Parameters.AddWithValue("@sys_adate", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@sys_mdate", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@remark", note);
                        cmd.Parameters.AddWithValue("@user_list", txb_emp_Notice.Text.Trim());
                        cmd.Parameters.AddWithValue("@mac", lab_mac.Text);
                        cmd.Parameters.AddWithValue("@tel", txb_tel.Text);
                        cmd.ExecuteNonQuery();
                        HRSConn.Close();
                        send();
                        string script = "alert('申請完成，請等待人資進行開通!!')";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                    }
                    else
                    {
                        string script = "alert('此電腦已經申請過開通，請與人資聯絡，並請人資啟用!!')";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                    }
                }

                else
                {
                    string script = "alert('同工編號有誤，或輸入格式錯誤，請確認!!')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                }

            }

            else
            {
                string script = "alert('使用同工欄位、電腦所在單位、電腦所在地址，申請人聯絡電話，申請原因不可為空，請確認!!')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
            }
        }

        public void send()
        {
            string mac = Request.QueryString["mac"];
            string ip = Request.QueryString["ip"];
            string name = Request.QueryString["name"];
            string mail_title = "單位「" + txb_PC_dep.Text.Trim() + "」申請開通電腦卡鐘通知!!";
            string mail_txt = "本申請單，相關資訊如下<br/>申請單位：" + txb_PC_dep.Text.Trim() +
                  "<br/>申請人姓名：" + txb_name.Text.Trim() +
                "<br/>申請人連絡電話：" + txb_tel.Text.Trim() +
                  "<br/>申請原因：" + txb_re.Text.Trim() +
                "<br/> 要開通電腦名稱：" + name +
                "<br/>要開通電腦MAC：" + mac +
                "<br/>要開通電腦IP：" + ip +
                "<br/><a href=http://192.168.246.11:1000/login.aspx> 請點此進入管理系統處理</a>";
            string mail_title2 = "主管您好，貴單位「" + txb_PC_dep.Text.Trim() + "」申請開通電腦卡鐘，系統知會!!";
            string mail_txt2 = "本申請單，相關資訊如下<br/>申請單位：" + txb_PC_dep.Text.Trim() +
              "<br/>申請人姓名：" + txb_name.Text.Trim() +
            "<br/>申請人連絡電話：" + txb_tel.Text.Trim() +
              "<br/>申請原因：" + txb_re.Text.Trim() +
            "<br/> 要開通電腦名稱：" + name +
            "<br/>要開通電腦MAC：" + mac +
            "<br/>要開通電腦IP：" + ip;
            string learder_mail = fn_get_dep_leader_mail(txb_PC_dep.Text.Trim());
            SendMail(mail_title, "eden6475@eden.org.tw", mail_txt);
            SendMail(mail_title2, learder_mail, mail_txt2);
        }
        public static string SendMail(string title, string address, string txt)
        {
            /*
              result      訊息
              mailserver  mailserver 位置
              emailFrom   寄信人帳號(隨便設定，格式要對)      
              password    寄信人密碼(隨便設定)
              emailTo     收信人      
              subject     信件主旨
              body        信件內容
            */

            string result = "信件發送成功";
            string mailserver = "192.168.1.10";
            string emailFrom = "edenSYSADMIN@eden.org.tw";
            string password = "SYSADMIN";
            string emailTo = address;
            string subject = title;
            string body = txt;

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    // 若內容是HTML格式，則為True
                    mail.IsBodyHtml = true;


                    using (SmtpClient smtp = new SmtpClient(mailserver))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

        public string fn_get_dep()
        {
            string dep = "";
            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            string sqlstring = @"SELECT user_list,substring([remark],1,3) as dep
  FROM [05200169].[dbo].[clocks] where veryfied ='1' and mac='" + Request.QueryString["mac"] + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            HrsConn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                dep = dr["dep"].ToString();
                dr.Close();
                HrsConn.Close();
            }
            return dep;
        }

        public string fn_get_dep_leader_mail(string dep)
        {
            string leader_mail = "";
            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            string sqlstring = @"SELECT a.[DEPARTMENT_ID]
      ,a.[COMPANY_ID]
      ,a.[DEPARTMENT_CODE]
      ,a.[DEPARTMENT_CNAME]
      ,[DEPARTMENT_ENAME]
      ,[DEPARTMENT_ABBR]
      ,[PART_DEPARTMENT_ID]
      ,b.EMPLOYEE_NO as DEP_LEADER_NO
	  ,b.EMPLOYEE_CNAME as DEP_LEADER_CNAME
	  ,b.EMPLOYEE_EMAIL_1 as DEP_LEADER_MAIL
      ,[DEPARTMENT_STATUS]
  FROM [05200169].[dbo].[vwZZ_DEPARTMENT] as a left join vwZZ_EMPLOYEE as b on a.DEPARTMENT_LEADER_ID=b.EMPLOYEE_ID WHERE A.COMPANY_ID='1' AND A.DEPARTMENT_CODE='" + dep + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            HrsConn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                leader_mail = dr["DEP_LEADER_MAIL"].ToString();
                dr.Close();
                HrsConn.Close();
            }

            return leader_mail;
        }

        protected void txb_PC_dep_TextChanged(object sender, EventArgs e)
        {
            string user_list = "";
            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            HrsConn.Open();
            string sqlstring2 = @"SELECT [EMPLOYEE_NO] FROM [05200169].[dbo].[vwZZ_EMPLOYEE] where DEPARTMENT_CODE=@DEPARTMENT_CODE  and EMPLOYEE_WORK_STATUS='1' ";
            SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
            cmd2.Parameters.AddWithValue("@DEPARTMENT_CODE", txb_PC_dep.Text.Trim());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    if (user_list == "")
                    {
                        user_list = dr2["EMPLOYEE_NO"].ToString();
                    }

                    else
                    {
                        user_list=user_list+","+ dr2["EMPLOYEE_NO"].ToString();
                    }
                }

                txb_emp_Notice.Text = user_list;
            }
        }
    }
}