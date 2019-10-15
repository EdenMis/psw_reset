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
using System.Media;


namespace psw_reset
{
    public partial class eClock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Label1.Text = "目前時間為：" + DateTime.Now.ToLongTimeString();
                string ip = Request.QueryString["ip"];
                string name = Request.QueryString["name"];
                string mac = Request.QueryString["mac"];
                string tClientIP = HttpContext.Current.Request.UserHostAddress.ToString();
                lab_IP.Text = "此台電腦IP為：" + tClientIP;
                lab_mac.Text = "此台電腦MAC為：" + mac;
                lab_name.Text = "此台電腦名稱為：" + name; 
                txb_mac.Text = mac;
                txb_ip.Text = tClientIP;
                txb_name.Text = name;
                bool ip_chk_in_out = fn_chk_in_out(tClientIP);
                bool ip_chk = fn_chk_IP(tClientIP, txb_mac.Text.Trim()); 
                HyperLink1.NavigateUrl = "ADD_USER.aspx?mac=" + txb_mac.Text.Trim() + "&ip=" + txb_ip.Text.Trim() + "&name=" + txb_name.Text.Trim();
                HyperLink2.NavigateUrl = "Edit_User.aspx?mac=" + txb_mac.Text.Trim() + "&ip=" + txb_ip.Text.Trim() + "&name=" + txb_name.Text.Trim();

                if (ip_chk == true)
                {
                    Lab_msg.ForeColor = System.Drawing.Color.Blue;
                    Lab_msg.Text = "此電腦可正常使用線上打卡";
                }

                else
                {
                    Lab_msg.Text = "此電腦之MAC未開通，請申請開通此電腦IP";
                }
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Label1.Text = "目前時間為：" + DateTime.Now.ToLongTimeString();
        }
        public bool fn_chk_in_out(string ip)
        {
            bool chk_in_out = false;
            String[] substrings = ip.Split('.');
            if (substrings[0] == "192" && substrings[1] == "168")
            {
                chk_in_out = true;
            }

            if (substrings[0] == "10")
            {
                chk_in_out = true;
            }

            return chk_in_out;
        }

        public bool fn_chk_IP(string c_ip, string c_mac)
        {
            bool ip_chk = false;
            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            string sqlstring = @"SELECT  [clkserverid]
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
  FROM [05200169].[dbo].[clocks] where veryfied ='1' and mac='" + c_mac + "' order by clkserverid;";
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            try
            {
                HrsConn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    ip_chk = true;
                    HrsConn.Close();
                }
                else
                {
                    ip_chk = false;
                    HrsConn.Close();
                }
            }
            catch
            {
                ip_chk = false;
                HrsConn.Close();
            }

            finally
            {
                HrsConn.Close();
            }
            return ip_chk;
        }

        public bool fn_log_in(string emp_no, string emp_psw)
        {
            bool log_in = false;
            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            string sqlstring = @"SELECT [EMPLOYEE_NO]
                                                  ,[EMPLOYEE_CNAME]
                                                  ,[PORTAL_USER]
                                                  ,[USER_PASSWORD]
                                                  ,[EMPLOYEE_PWD]
                                                  ,[EMPLOYEE_ID]
                                                   FROM [05200169].[dbo].[zz_vw_portal_pwd] where EMPLOYEE_NO ='" + emp_no + "' and USER_PASSWORD = '" + emp_psw + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            HrsConn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                log_in = true;
            }
            HrsConn.Close();
            return log_in;
        }

        public bool fn_chk_emp(string c_mac)
        {
            bool emp_chk = false;
            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            string sqlstring = @"SELECT  [clkserverid]
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
  FROM [05200169].[dbo].[clocks] where veryfied ='1' and mac='" + c_mac + "' and user_list like '%"+txb_user_id.Text+"%'order by clkserverid;";
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            try
            {
                HrsConn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    emp_chk = true;
                    HrsConn.Close();
                }
                else
                {
                    emp_chk = false;
                    HrsConn.Close();
                }
            }
            catch
            {
                emp_chk = false;
                HrsConn.Close();
            }

            finally
            {
                HrsConn.Close();
            }
            return emp_chk;
        }

        protected void btn_check_Click(object sender, EventArgs e)
        {
            string tClientIP = HttpContext.Current.Request.UserHostAddress.ToString();
            string emp_no = txb_user_id.Text.Trim();
            string emp_psw = covHREnCode(txb_user_psw.Text.Trim());
            bool ip_chk_in_out = fn_chk_in_out(tClientIP);
            bool ip_chk = fn_chk_IP(tClientIP, txb_mac.Text);
            bool log_in = fn_log_in(emp_no, emp_psw);
            bool emp_chk = fn_chk_emp(txb_mac.Text);

            if (ip_chk == true)
            {
                if (log_in == true)
                {
                    if (emp_chk == true)
                    {
                        string CARD_DATE = DateTime.Now.ToString("yyyMMdd");
                        string CARD_TIME = DateTime.Now.ToString("HHmmss");
                        string CARD_NO = txb_user_id.Text;
                        SqlConnection HRSConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                        HRSConn.Open();
                        string sqlstring = @"Insert Into HRS_CARD_READ 
                                                        (CARD_NO
                                                        ,CARD_DATE
                                                        ,CARD_TIME
                                                        ,CARD_TYPE
                                                        ,MACHINE_NO
                                                        ,READ_DATE
                                                        ,READ_TIME
                                                        ,EMP_NO
                                                        ,DUTY_TYPE
                                                        ,UPDATE_USER
                                                        ,UPDATE_DATE
                                                        ,UPDATE_TIME
                                                        ,RECORD_OWNER
                                                        ,IP
                                                        ,MAC) values(
                                                         @CARD_NO
                                                        ,@CARD_DATE
                                                        ,@CARD_TIME
                                                        ,@CARD_TYPE
                                                        ,@MACHINE_NO
                                                        ,@READ_DATE
                                                        ,@READ_TIME
                                                        ,@EMP_NO
                                                        ,@DUTY_TYPE
                                                        ,@UPDATE_USER
                                                        ,@UPDATE_DATE
                                                        ,@UPDATE_TIME
                                                        ,@RECORD_OWNER
                                                        ,@IP
                                                        ,@MAC)";
                        SqlCommand cmd = new SqlCommand(sqlstring, HRSConn);
                        cmd.Parameters.AddWithValue("@CARD_NO", CARD_NO);
                        cmd.Parameters.AddWithValue("@CARD_DATE", CARD_DATE);
                        cmd.Parameters.AddWithValue("@CARD_TIME", CARD_TIME);
                        cmd.Parameters.AddWithValue("@CARD_TYPE","");
                        cmd.Parameters.AddWithValue("@MACHINE_NO", "1");
                        cmd.Parameters.AddWithValue("@READ_DATE", CARD_DATE);
                        cmd.Parameters.AddWithValue("@READ_TIME", CARD_TIME);
                        cmd.Parameters.AddWithValue("@EMP_NO", CARD_NO);
                        cmd.Parameters.AddWithValue("@DUTY_TYPE", "1");
                        cmd.Parameters.AddWithValue("@UPDATE_USER", "EDENCLOCK");
                        cmd.Parameters.AddWithValue("@UPDATE_DATE", CARD_DATE);
                        cmd.Parameters.AddWithValue("@UPDATE_TIME", CARD_TIME);
                        cmd.Parameters.AddWithValue("@RECORD_OWNER", CARD_NO);
                        cmd.Parameters.AddWithValue("@IP", txb_ip.Text);
                        cmd.Parameters.AddWithValue("@MAC", txb_mac.Text);
                        cmd.ExecuteNonQuery();
                        HRSConn.Close();
                        fn_update_time(txb_mac.Text);
                        lab_media.Text = @"<embed src='check_ok.wav' hidden=true autostart=true loop=false>";
                        lab_sys_msg.ForeColor = System.Drawing.Color.Blue;
                        lab_sys_msg.Text = "同工「"+CARD_NO+"」，於「"+DateTime.Now.ToLongTimeString()+"」打卡成功!!";
                        txb_user_id.Text = "";
                    }

                    else
                    {
                        lab_media.Text = @"<embed src='not_allow.wav' hidden=true autostart=true loop=false>";
                        lab_sys_msg.ForeColor = System.Drawing.Color.Red;
                        lab_sys_msg.Text = "此同工不允許在此電腦上打卡";
                    }

                }

                else
                {
                    lab_media.Text = @"<embed src='psw_error.wav' hidden=true autostart=true loop=false>";
                    lab_sys_msg.ForeColor = System.Drawing.Color.Red;
                    lab_sys_msg.Text = "帳號密碼錯誤";
                }
            }

            else
            {
                lab_media.Text = @"<embed src='not_open.wav' hidden=true autostart=true loop=false>";
                lab_sys_msg.ForeColor = System.Drawing.Color.Red;
                lab_sys_msg.Text = "此電腦未開通打卡功能";
            }

        }
        static string covHREnCode(string new_psw)
        {
            clsEncode en = new clsEncode();
            string encode = en.Encode(new_psw);
            return encode;
        }

        public void fn_update_time(string mac)
        {
            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            string sqlstring = @"UPDATE clocks set lastlogtime='"+ DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where mac ='"+mac+"'";
            HrsConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            cmd.ExecuteNonQuery();
            HrsConn.Close();
        }
    }
}