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


namespace psw_reset.GroupBuy
{
    public partial class SSO_mail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Form["EmployeeID"] != null && Request.Form["EmployeePWD"] != null)
                {
                    txb_user_no.Text = Request.Form["EmployeeID"];
                    txb_user_pwd.Text = Request.Form["EmployeePWD"];
                    string emp_no = txb_user_no.Text;
                    string emp_pwd = txb_user_pwd.Text;
                    SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                    string sqlstring = @"SELECT [EMPLOYEE_NO]
                                                      ,[EMPLOYEE_CNAME]
                                                      ,[PORTAL_USER]
                                                      ,[USER_PASSWORD]
                                                      ,[EMPLOYEE_PWD]
                                                      ,[EMPLOYEE_ID]
                                                       FROM [05200169].[dbo].[zz_vw_portal_pwd] where EMPLOYEE_NO ='" + emp_no + "' and USER_PASSWORD = '" + emp_pwd + "'";
                    SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
                    try
                    {
                        HrsConn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            HrsConn.Close();
                            HrsConn.Open();
                            string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + txb_user_no.Text + "'";
                            SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            if (dr2.HasRows)
                            {
                                dr2.Read();
                                Session["User_ID"] = dr2["EMPLOYEE_NO"].ToString();
                                Session["User_Name"] = dr2["EMPLOYEE_CNAME"].ToString();
                                Session["User_Mail"] = dr2["EMPLOYEE_EMAIL_1"].ToString();
                                Session["User_Dep_ID"] = dr2["DEPARTMENT_CODE"].ToString();
                                Session["User_Dep_Name"] = dr2["DEPARTMENT_CNAME"].ToString();
                                Session["Login"] = "OK";

                                cmd2.Cancel();
                                dr2.Close();
                                HrsConn.Close();
                                HrsConn.Dispose();
                                Response.Redirect("/GroupBuy/index.aspx");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('查無此同工');location.href='../GroupBuy/login.aspx'", true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('EIP尚未登入，請輸入EIP帳號密碼直接登入，謝謝!!');location.href='../GroupBuy/login.aspx'", true);
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
            string sqlstring = @"UPDATE Application_List set Application_allow ='1' where Application_user_ID ='" + user_NO + "'";
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