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

namespace psw_reset
{
    public partial class pwd_reset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["userNo"] != null && Request.QueryString["user_pwd"]!=null)
                {
                    txb_user_no.Text = Request.QueryString["userNo"];
                    txb_user_pwd.Text = Request.QueryString["user_pwd"];
                    string emp_no = txb_user_no.Text;
                    string emp_pwd = txb_user_pwd.Text;
                    DateTime now_time = new DateTime();
                    DateTime over_time = new DateTime();
                    string allow = "0";

                    SqlConnection PWDconn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Pwd_Reset"].ConnectionString);
                    string sqlstring2 = @"SELECT top(1) [Application_No] ,[Application_user_ID],[Application_Name] ,[Application_allow] ,[Application_time],[Application_over_time] FROM [Pwd_Reset].[dbo].[Application_List] where [Application_user_ID]='" + emp_no + "' and Application_allow='0' order by Application_No desc";
                    SqlCommand cmd2 = new SqlCommand(sqlstring2, PWDconn);
                    PWDconn.Open();
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        dr2.Read();
                        string Application_over_time = dr2["Application_over_time"].ToString();//過期時間
                        PWDconn.Close();
                        now_time = DateTime.Now;
                        over_time = Convert.ToDateTime(Application_over_time);
                    }
                    else
                    {
                        allow = "1";
                    }

                    if (DateTime.Compare(now_time, over_time) < 0 && allow=="0") //連結沒過期
                    {
                        SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                        string sqlstring = @"SELECT [EMPLOYEE_NO]
                                                  ,[EMPLOYEE_CNAME]
                                                  ,[PORTAL_USER]
                                                  ,[USER_PASSWORD]
                                                  ,[EMPLOYEE_PWD]
                                                  ,[EMPLOYEE_ID]
                                                   FROM [05200169].[dbo].[zz_vw_portal_pwd] where EMPLOYEE_NO ='" + emp_no + "' and USER_PASSWORD = '"+emp_pwd+"'";
                        SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
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

                                string new_psw = "00" + EMP_NO;
                                string new_pswMd5 = EMP_NO + "00" + EMP_NO;
                                string new_psw_EnMd5 = covMD5(new_pswMd5);
                                string new_psw_HrEnCode = covHREnCode(new_psw);

                                updateAD(EMP_NO, new_psw);
                                updateHR_user_psw(EMP_ID, new_psw_HrEnCode);
                                updateHR_MD5_psw(EMP_ID, new_psw_EnMd5);
                                update_ERP_psw(EMP_NO, new_psw);
                                update_Application_List_allow(EMP_NO);

                                Label1.Text = "同工「" + EMP_CName + "」的密碼已經重置成功。";
                                HrsConn.Close();
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
                            Label1.Text = ex.ToString();
                        }
                    }

                    else
                    {
                        SqlConnection PWDconn2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["Pwd_Reset"].ConnectionString);
                        PWDconn2.Open();
                        string sqlstring = @"UPDATE Application_List set Application_over ='1' where Application_user_ID='"+ emp_no + "'";
                        SqlCommand cmd = new SqlCommand(sqlstring, PWDconn2);
                        cmd.ExecuteNonQuery();
                        PWDconn2.Close();
                        Label1.Text = "連結已失效。";
                        ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('連結已失效');window.close()", true);
                    }
                }
                else
                {
                    //ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('請透過重置連結重置密碼');location.href='http://eip.eden.org.tw'", true);
                    ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('請透過重置連結重置密碼');window.close()", true);
                }
            }
        }

        public void updateAD(string user_ID, string new_psw)
        {
            string QueryString = "LDAP://ad2012.eden.org.tw";
            DirectoryEntry de = new DirectoryEntry(QueryString, "adapi", "Aa8f4w86><.");
            DirectorySearcher ds = new DirectorySearcher(de);
            //設定查詢條件：依姓名及工號查詢
            ds.Filter = @"(samaccountname=" + user_ID + ")";
            //將查詢結果放入Results
            SearchResult result = ds.FindOne();
            DirectoryEntry user = result.GetDirectoryEntry();
            user.Invoke("SetPassword", new_psw);
            de.Close();
        }

        public void updateHR_user_psw(string user_ID, string new_psw_HrEnCode)
        {
            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            HrsConn.Open();
            string sqlstring = @"UPDATE ZZ_EDEN_PORTAL_PWD set USER_PASSWORD =@NEW_USER_PASSWORD where EMPLOYEE_ID='" + user_ID + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            cmd.Parameters.AddWithValue("@NEW_USER_PASSWORD", new_psw_HrEnCode);
            cmd.ExecuteNonQuery();
            HrsConn.Close();
        }

        public void updateHR_MD5_psw(string user_ID, string new_psw_EnMd5)
        {
            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            HrsConn.Open();
            string sqlstring = @"UPDATE PORTAL_EMPLOYEE_DATA set EMPLOYEE_PWD = '" + new_psw_EnMd5 + "' where EMPLOYEE_ID='" + user_ID + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            cmd.Parameters.AddWithValue("@NEW_EMPLOYEE_PWD", new_psw_EnMd5);
            cmd.ExecuteNonQuery();
            HrsConn.Close();
        }

        public void update_ERP_psw(string user_NO, string new_psw)
        {
            SqlConnection ErpConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ERP"].ConnectionString);
            ErpConn.Open();
            string sqlstring = @"UPDATE USERS set PWD =@NEW_PWD  where USERID='" + user_NO + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, ErpConn);
            cmd.Parameters.AddWithValue("@NEW_PWD", new_psw);
            cmd.ExecuteNonQuery();
            ErpConn.Close();
        }

        public void update_Application_List_allow(string user_NO)
        {
            SqlConnection PWDconn2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["Pwd_Reset"].ConnectionString);
            PWDconn2.Open();
            string sqlstring = @"UPDATE Application_List set Application_allow ='1' where Application_user_ID ='"+user_NO+"'";
            SqlCommand cmd = new SqlCommand(sqlstring, PWDconn2);
            cmd.ExecuteNonQuery();
            PWDconn2.Close();
        }

        static string covMD5(string new_pswMd5)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] myData = md5Hasher.ComputeHash(Encoding.Default.GetBytes(new_pswMd5));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < myData.Length; i++)
            {
                sBuilder.Append(myData[i].ToString("x2"));
            }
            return string.Format(sBuilder.ToString()).ToUpper();
        }

        static string covHREnCode(string new_psw)
        {
            clsEncode en = new clsEncode();
            string encode = en.Encode(new_psw);
            return encode;
        }

    }
}