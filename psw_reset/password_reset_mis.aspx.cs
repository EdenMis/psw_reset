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
            if (Request.Form["EmployeeID"] == "6475" || Request.Form["EmployeeID"] == "1321" || Request.Form["EmployeeID"] == "4714" || Request.Form["EmployeeID"] == "7751" || Request.Form["EmployeeID"] == "8416" || Request.Form["EmployeeID"] == "8614")
            {
                txb_login_emp_no.Text = Request.Form["EmployeeID"];
            }

            else
            {
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('沒有權限使用此功能');location.href='http://eip.eden.org.tw'", true);
            }
        }

        protected void btn_recover_psw_Click(object sender, EventArgs e)
        {
            Lab_msg.Text = "";
            //if (!(new System.Text.RegularExpressions.Regex("^[A-Za-z0-9]+$")).IsMatch(txb_new_pwd.Text.Trim()))
            //{
            //    psw_check_false.Checked = true;
            //    Lab_msg.Text = "密碼請使用數字跟英文，請勿使用符號";
            //}

            string emp_no = txb_emp_no.Text.ToUpper();
            string new_password = "00" + emp_no;
            if (emp_no == "")
            {
                emp_no = "null";
            }
            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            string sqlstring = @"SELECT EMPLOYEE_NO,EMPLOYEE_ID FROM zz_vw_portal_pwd where EMPLOYEE_NO='" + emp_no + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            try
            {
                HrsConn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    string EMP_ID = dr["EMPLOYEE_ID"].ToString();  //非同工編號
                    string new_pswMd5 = emp_no + new_password;
                    string new_psw_EnMd5 = covMD5(new_pswMd5);
                    string new_psw_HrEnCode = covHREnCode(new_password);


                    updateAD(emp_no,new_password);
                    updateHR_user_psw(EMP_ID, new_psw_HrEnCode);
                    updateHR_MD5_psw(EMP_ID, new_psw_EnMd5);
                    update_ERP_psw(emp_no, new_password);
                    update_PSM_psw(emp_no, new_password);
                    updatePMS_user_HrEnCodepsw(EMP_ID, new_psw_HrEnCode);

                    string script = "alert('同工編號「" + emp_no + "」的密碼已修改完成')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                    HrsConn.Close();
                }

                else
                {
                    string script = "alert('HR内的「 zz_vw_portal_pwd」查無此同工，請確認')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                    HrsConn.Close();
                }
            }

            catch (Exception ex)
            {
                Lab_msg.Text = ex.ToString();
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

        public void update_PSM_psw(string user_NO, string new_psw)
        {
            SqlConnection PMSConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["PMS"].ConnectionString);
            PMSConn.Open();
            string sqlstring = @"UPDATE View_UserPWD set PWD =@NEW_PWD  where No='" + user_NO + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, PMSConn);
            cmd.Parameters.AddWithValue("@NEW_PWD", new_psw);
            cmd.ExecuteNonQuery();
            PMSConn.Close();
        }

        public void updatePMS_user_HrEnCodepsw(string user_ID, string new_psw_HrEnCode)
        {
            SqlConnection PMSConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["PMS"].ConnectionString);
            PMSConn.Open();
            string sqlstring = @"UPDATE VIEW_EmployeePWD set USER_PASSWORD =@NEW_USER_PASSWORD where No='" + user_ID + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, PMSConn);
            cmd.Parameters.AddWithValue("@NEW_USER_PASSWORD", new_psw_HrEnCode);
            cmd.ExecuteNonQuery();
            PMSConn.Close();
        }
    }
}