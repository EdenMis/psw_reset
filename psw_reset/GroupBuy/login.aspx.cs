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
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            string pwd_encode = covHREnCode(txb_password.Text);
            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            string sqlstring = @"SELECT [EMPLOYEE_NO]
                                                  ,[EMPLOYEE_CNAME]
                                                  ,[PORTAL_USER]
                                                  ,[USER_PASSWORD]
                                                  ,[EMPLOYEE_PWD]
                                                  ,[EMPLOYEE_ID]
                                                   FROM [05200169].[dbo].[zz_vw_portal_pwd] where EMPLOYEE_NO ='" + txb_user_ID.Text + "' and USER_PASSWORD = '" + pwd_encode + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            try
            {
                HrsConn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    HrsConn.Close();
                    HrsConn.Open();
                    string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '"+txb_user_ID.Text+"'";
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
                        Response.Redirect("../GroupBuy/index.aspx");
                    }
                }

                else
                {
                    string script = "alert('查無此同工或密碼錯誤，請確認')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                    HrsConn.Close();
                }
            }

            catch
            {

            }
            finally
            {
                HrsConn.Close();
            }
        }
        static string covHREnCode(string new_psw)
        {
            clsEncode en = new clsEncode();
            string encode = en.Encode(new_psw);
            return encode;
        }
    }
}