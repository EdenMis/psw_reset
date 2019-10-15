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
    public partial class pwd_forget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_recover_psw_Click(object sender, EventArgs e)
        {
            //1. 同工進行申請
            //2. 檢查是否有重複申請 -> 錯誤訊息(已申請重置，請等待主管審核)
            //3. 找到申請同工
            //4. 找出該同工的上層主管
            //5. 將申請資料寫進申請表格(申請人，重置旗標，時間標記)
            //6. 發信給主管

            string user_id = txb_user_id.Text.Trim();
            string IDC_id = txb_ID.Text.Trim();

            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            string sqlstring = @"SELECT TOP (1000) [EMPLOYEE_ID] ,[EMPLOYEE_NO],[DEPARTMENT_CODE] ,[EMPLOYEE_CNAME] ,[EMPLOYEE_IDC_NO],[TOPIC_FIRST_BOSS_ID] FROM [05200169].[dbo].[vwZZ_EMPLOYEE] where EMPLOYEE_IDC_NO=@EMPLOYEE_IDC_NO and EMPLOYEE_NO=@EMPLOYEE_NO";
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            cmd.Parameters.AddWithValue("@EMPLOYEE_IDC_NO", IDC_id);
            cmd.Parameters.AddWithValue("@EMPLOYEE_NO", user_id);
            try
            {
                HrsConn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    string EMP_ID = dr["EMPLOYEE_ID"].ToString();  //非同工編號
                    string EMP_NO = dr["EMPLOYEE_NO"].ToString(); //同工編號
                    string EMP_CName = dr["EMPLOYEE_CNAME"].ToString(); //同工姓名
                    string BOSS_ID = dr["TOPIC_FIRST_BOSS_ID"].ToString();//第一階主管，非同工編號
                    string dept_id = dr["DEPARTMENT_CODE"].ToString();//重置密碼同工部門
                    HrsConn.Close();


                    HrsConn.Open();
                    string sqlstring4 = @"SELECT  [EMPLOYEE_NO] ,[EMPLOYEE_CNAME] ,[PORTAL_USER] ,[USER_PASSWORD] ,[EMPLOYEE_PWD] ,[EMPLOYEE_ID] FROM [05200169].[dbo].[zz_vw_portal_pwd] where EMPLOYEE_NO='" + EMP_NO + "'";
                    SqlCommand cmd4 = new SqlCommand(sqlstring4, HrsConn);
                    SqlDataReader dr4 = cmd4.ExecuteReader();
                    dr4.Read();
                    string EMP_User_Pwd = dr4["USER_PASSWORD"].ToString();  //非同工編號
                    HrsConn.Close();

                    //檢查是否有申請紀錄
                    SqlConnection PWDconn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Pwd_Reset"].ConnectionString);
                    string sqlstring2 = @"SELECT [Application_No],[Application_user_ID] ,[Application_Name] ,[Application_allow] ,[Application_time],Application_over FROM [Pwd_Reset].[dbo].[Application_List] where [Application_user_ID]='" + EMP_NO + "' and Application_allow='0' and Application_over='0'";
                    SqlCommand cmd2 = new SqlCommand(sqlstring2, PWDconn);
                    PWDconn.Open();
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (!dr2.HasRows)
                    {
                        PWDconn.Close();
                        SqlConnection HrsConn2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                        HrsConn2.Open();
                        string sqlstring3 = @"SELECT [EMPLOYEE_ID] ,[EMPLOYEE_NO] ,[EMPLOYEE_CNAME] FROM [05200169].[dbo].[vwZZ_EMPLOYEE] where  EMPLOYEE_ID='" + BOSS_ID + "'";
                        SqlCommand cmd3 = new SqlCommand(sqlstring3, HrsConn2);
                        SqlDataReader dr3 = cmd3.ExecuteReader();
                        dr3.Read();
                        string BOSS_NO = dr3["EMPLOYEE_NO"].ToString();//第一階主管，同工編號
                        string BOSS_Cname = dr3["EMPLOYEE_CNAME"].ToString();//第一階主管，姓名
                        HrsConn2.Close();

                        string Boss_mail = "eden" + BOSS_NO + "@eden.org.tw";
                        string Boss_title = "系統通知：同工" + EMP_NO + "_" + EMP_CName + "申請密碼重置，請確認";
                        string Boss_txt = "<a href=http://missys.eden.org.tw/pwd_reset.aspx?userNo=" + EMP_NO + "&user_pwd=" + EMP_User_Pwd + ">點此重置密碼</a><br />此連結三天後自動失效";
                        SendMail_BOSS(Boss_title, Boss_mail, Boss_txt);

                        string Application_User_Mail = "eden" + EMP_NO + "@eden.org.tw";
                        string Application_User_title = "系統通知：您已經申請密碼重置作業，請等待主管進行重置";
                        string Application_User_txt = "您已經申請密碼重置作業，請等待主管進行重置";

                        SendMail_Application(Application_User_title, Application_User_Mail, Application_User_txt);

                        ins_Application_List(EMP_NO, EMP_CName);

                        string script = "alert('申請成功，請等待主管審核')";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);

                    }

                    else
                    {
                        PWDconn.Close();
                        string script = "alert('您已申請重置，請耐心等待主管審核')";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                    }

                }
                else
                {
                    string script = "alert('查無此同工，請確認')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                    HrsConn.Close();
                }
            }

            catch (Exception ex)
            {
                Lab_msg.Text = ex.ToString();
            }
        }

        public static string SendMail_BOSS(string title, string address, string txt)
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

        public static string SendMail_Application(string title, string address, string txt)
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

        public void ins_Application_List(string Application_user_ID, string Application_CName)
        {
            DateTime Application_over_time = DateTime.Now.AddHours(72);
            SqlConnection PWDconn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Pwd_Reset"].ConnectionString);
            PWDconn.Open();
            string sqlstring = @"INSERT INTO Application_List ([Application_user_ID] ,[Application_Name] ,[Application_allow] ,[Application_time],Application_over_time,Application_over) VALUES (@Application_user_ID,@Application_Name,'0',@Application_time,@Application_over_time,'0') ";
            SqlCommand cmd = new SqlCommand(sqlstring, PWDconn);
            cmd.Parameters.AddWithValue("@Application_user_ID", Application_user_ID);
            cmd.Parameters.AddWithValue("@Application_Name", Application_CName);
            cmd.Parameters.AddWithValue("@Application_time", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@Application_over_time", Application_over_time.ToString("yyyy/MM/dd HH:mm:ss"));
            cmd.ExecuteNonQuery();
            PWDconn.Close();
        }
    }
}
