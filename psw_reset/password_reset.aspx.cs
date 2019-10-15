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
    public partial class password_reset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_recover_psw_Click(object sender, EventArgs e)
        {
            string title = Request.QueryString["BPM_serial"];
            Lab_msg.Text = "";
            psw_check_false.Checked = false;

            if (txb_new_pwd.Text.Trim().Length < 6)
            {
                psw_check_false.Checked = true;
                Lab_msg.Text = "密碼長度不足，密碼請在6~10字之間";
            }

            //if (!(new System.Text.RegularExpressions.Regex("^[A-Za-z0-9]+$")).IsMatch(txb_new_pwd.Text.Trim()))
            //{
            //    psw_check_false.Checked = true;
            //    Lab_msg.Text = "密碼請使用數字跟英文，請勿使用符號";
            //}

            string IDC_ID = txb_ID.Text.ToUpper();
            if (IDC_ID == "")
            {
                IDC_ID = "null";
            }
            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            string sqlstring = @"SELECT [EMPLOYEE_ID],[EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_IDC_NO] FROM [05200169].[dbo].[vwZZ_EMPLOYEE] where EMPLOYEE_IDC_NO='"+ IDC_ID + "'";
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

                    if (txb_new_pwd.Text.Trim() == txb_new_pwd_chk.Text.Trim() && psw_check_false.Checked==false)
                    {
                        string new_psw = txb_new_pwd.Text.Trim();
                        string new_pswMd5 = EMP_NO + new_psw;
                        string new_psw_EnMd5 = covMD5(new_pswMd5);
                        string new_psw_HrEnCode = covHREnCode(new_psw);

                        updateAD(EMP_NO, new_psw);
                        updateHR_user_psw(EMP_ID, new_psw_HrEnCode);
                        updateHR_MD5_psw(EMP_ID, new_psw_EnMd5);
                        update_ERP_psw(EMP_NO, new_psw);

                        string script = "alert('同工「"+EMP_CName+"」的密碼已修改完成，下次請使用新密碼進入')";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                        HrsConn.Close();

                        //Response.Write(new_psw+"<br />");
                        //Response.Write(new_pswMd5 + "<br />");
                        //Response.Write(new_psw_EnMd5 + "<br />");
                        //Response.Write(new_psw_HrEnCode + "<br />");                 
                    }

                    else
                    {
                        string script = "alert('新密碼確認未通過，請確認密碼一致')";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                        HrsConn.Close();
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

   

            //foreach (SearchResult result in Results)
            //{
            //    if (result.Properties["description"].Count > 0)
            //    {
            //        // 透過AD裡的屬性「description」，取得工號
            //        string resultNo = result.Properties["displayname"][0].ToString();
            //        Response.Write(resultNo);
            //        Response.Write("<br>");
            //    }
            //}
            //ds.Dispose(); // 釋放資源
            //de.Dispose();

            //Response.Write(FormsAuthentication.HashPasswordForStoringInConfigFile("6475740719", "MD5"));

        }

        public void updateAD(string user_ID,string new_psw)
        {
            string QueryString = "LDAP://ad2012.eden.org.tw";
            DirectoryEntry de = new DirectoryEntry(QueryString, "adapi", "Aa8f4w86><.");
            DirectorySearcher ds = new DirectorySearcher(de);
            //設定查詢條件：依姓名及工號查詢
            ds.Filter = @"(samaccountname="+user_ID+")";
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

        static string  covMD5(string new_pswMd5)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] myData = md5Hasher.ComputeHash(Encoding.Default.GetBytes(new_pswMd5));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < myData.Length; i++)
            {
                sBuilder.Append(myData[i].ToString("x2"));
            }
            return  string.Format(sBuilder.ToString()).ToUpper();
        }

        static string covHREnCode(string new_psw)
        {
            clsEncode en = new clsEncode();
            string encode = en.Encode(new_psw);
            return encode;
        }

    }
}