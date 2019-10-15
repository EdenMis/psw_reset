using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.XSSF.UserModel;
using System.Net.Mail;
using System.Net;

namespace psw_reset
{
    public partial class Edit_User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lab_user_list.Text = fn_get_user_list();
            }
        }

        protected void btn_open_Click(object sender, EventArgs e)
        {
            bool check = true;
            if(txb_PC_addr.Text!="" && txb_tel.Text!="" && txb_name.Text!="" && txb_re.Text!="")
            {
                String word = fn_new_user_list();
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
                    string dep = fn_get_dep();
                    string mac = Request.QueryString["mac"];
                    string ip = Request.QueryString["ip"];
                    string name = Request.QueryString["name"];
                    string mail_title = "單位「" + dep + "」申請變更電腦卡鐘使用同工通知!!";
                    string mail_txt = "本申請單，相關資訊如下<br/>申請單位：" + dep.Trim() +
                        "<br/>申請人姓名：" + txb_name.Text.Trim() +
                        "<br/>申請人連絡電話：" + txb_tel.Text.Trim() +
                        "<br/>申請原因：" + txb_re.Text.Trim() +
                        "<br/> 電腦名稱：" + name +
                        "<br/>電腦MAC：" + mac +
                        "<br/>電腦IP：" + ip +
                        "<br/>目前可打卡同工名單：" + fn_get_user_list() +
                        "<br/>追加可打卡同工：" + txb_add.Text.Trim()+
                        "<br/>追減可打卡同工：" + txb_less.Text.Trim()+
                        "<br/>新可打卡同工名單：" + fn_new_user_list();

                    string mail_title2 = "主管您好，貴單位「" +dep.Trim() + "」申請變更電腦卡鐘使用同工，系統知會!!";
                    string mail_txt2 = "本申請單，相關資訊如下<br/>申請單位：" + dep.Trim() +
                        "<br/>申請人姓名：" + txb_name.Text.Trim() +
                        "<br/>申請人連絡電話：" + txb_tel.Text.Trim() +
                        "<br/>申請原因：" + txb_re.Text.Trim() +
                        "<br/> 電腦名稱：" + name +
                        "<br/>電腦MAC：" + mac +
                        "<br/>電腦IP：" + ip +
                        "<br/>目前可打卡同工名單：" + fn_get_user_list() +
                        "<br/>追加可打卡同工：" + txb_add.Text.Trim() +
                        "<br/>追減可打卡同工：" + txb_less.Text.Trim() +
                        "<br/>新可打卡同工名單：" + fn_new_user_list();

                    fn_ins_clock_user_change_log(mac);
                    fn_update_user_list(mac);
                    string learder_mail = fn_get_dep_leader_mail(dep);
                    SendMail(mail_title, "eden6475@eden.org.tw", mail_txt);
                    SendMail(mail_title2, learder_mail, mail_txt2);
                    string script2 = "alert('處理完成!!')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
                }

                else
                {
                    string script2 = "alert('同工編號有誤，或輸入格式錯誤。請確認目前可打卡人員內是否有離職同工，並將該人員填入追減欄位!!')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
                }
            }
           
            else
            {
                string script2 = "alert('電腦所在單位，連絡人電話，電腦所在地址及備註不可為空!!')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
            }
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

        public string fn_get_user_list()
        {
            string user_list = "";
              SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            string sqlstring = @"SELECT user_list
  FROM [05200169].[dbo].[clocks] where veryfied ='1' and mac='" + Request.QueryString["mac"] + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            HrsConn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                user_list = dr["user_list"].ToString();
                dr.Close();
                HrsConn.Close();
            }

            else
            {
                user_list = "此電腦尚未開通";
                HrsConn.Close();
            }
            return user_list;
        }

        public void fn_update_user_list(string mac)
        {
            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            string sqlstring = @"UPDATE clocks set user_list=@user_list where mac ='" + mac + "'";
            HrsConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            cmd.Parameters.AddWithValue("@user_list", fn_new_user_list());
            cmd.ExecuteNonQuery();
            HrsConn.Close();
        }

        public void fn_ins_clock_user_change_log(string mac)
        {
            SqlConnection HRSConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            HRSConn.Open();
            string sqlstring = @"INSERT INTO clock_user_change_log
                                                                                ([mac]
      ,[applicant]
      ,[applicant_tel]
      ,[original]
      ,[add]
      ,[less]
      ,[reason]
      ,[applicant_date]) 
                                                                                VALUES 
                                                                                (@mac
      ,@applicant
      ,@applicant_tel
      ,@original
      ,@add
      ,@less
      ,@reason
      ,@applicant_date);";
            SqlCommand cmd = new SqlCommand(sqlstring, HRSConn);
            cmd.Parameters.AddWithValue("@mac", mac);
            cmd.Parameters.AddWithValue("@applicant", txb_name.Text.Trim());
            cmd.Parameters.AddWithValue("@applicant_tel", txb_tel.Text.Trim());
            cmd.Parameters.AddWithValue("@original", lab_user_list.Text.Trim());
            cmd.Parameters.AddWithValue("@add", txb_add.Text.Trim());
            cmd.Parameters.AddWithValue("@less", txb_less.Text.Trim());
            cmd.Parameters.AddWithValue("@reason",txb_re.Text.Trim());
            cmd.Parameters.AddWithValue("@applicant_date", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            cmd.ExecuteNonQuery();
            HRSConn.Close();
        }

        public string fn_new_user_list()
        {
            bool add_chk = false;
            string user_list = "";
            String o = lab_user_list.Text.Trim();
            String add = txb_add.Text.Trim();
            String less = txb_less.Text.Trim();
            String[] o_substrings = o.Split(',');
            String[] add_substrings = add.Split(',');
            String[] less_substrings = less.Split(',');
            int o_leg = o_substrings.Length;
            if (txb_add.Text != "")
            {
                for (int i = 0; i < add_substrings.Length; i++)
                {
                    for (int j = 0; j < o_leg; j++)
                    {
                        if (add_substrings[i] == o_substrings[j])
                        {
                            add_chk = true;
                            break;
                        }
                        else
                        {
                            add_chk = false;
                        }
                    }

                    if (add_chk == false)
                    {
                        System.Array.Resize(ref o_substrings, o_substrings.Length + 1);
                        o_substrings[o_substrings.Length - 1] = add_substrings[i];
                    }
                }
            }
           
            for (int i = 0; i < less_substrings.Length; i++)
            {
                o_substrings = o_substrings.Where(val => val != less_substrings[i]).ToArray();
            }

            for (int i = 0; i < o_substrings.Length; i++)
            {
                if (user_list == "")
                {
                    user_list = o_substrings[i];
                }

                else
                {
                    user_list = user_list + "," + o_substrings[i];
                }
            }
            return user_list;
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
                dep= dr["dep"].ToString();
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
  FROM [05200169].[dbo].[vwZZ_DEPARTMENT] as a left join vwZZ_EMPLOYEE as b on a.DEPARTMENT_LEADER_ID=b.EMPLOYEE_ID WHERE A.COMPANY_ID='1' AND A.DEPARTMENT_CODE='"+dep+"'";
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
    }
}