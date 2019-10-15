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
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using System.Media;
using System.Security.Cryptography;
using System.Text;


namespace psw_reset
{
    public partial class Pms_SSO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Form["EmployeeID"] != null && Request.Form["EmployeePWD"] != null)
                {
                    string year = (DateTime.Now.Year - 1911).ToString();
                    txb_user_no.Text = Request.Form["EmployeeID"];
                    txb_user_pwd.Text = Request.Form["EmployeePWD"];

                    string emp = txb_user_no.Text;
                    byte[] bytes = System.Text.Encoding.GetEncoding("utf-8").GetBytes(emp);
                    string emp_base64 = Convert.ToBase64String(bytes);

                    SHA512 sha512 = new SHA512CryptoServiceProvider();
                    //byte[] source = Encoding.Default.GetBytes(emp_base64+"108"+"v3geQ23Z");
                    string ss = emp_base64 + year + "v3geQ23Z";
                    byte[] source = Encoding.Default.GetBytes(emp_base64 + year + "v3geQ23Z");
                    byte[] crypto = sha512.ComputeHash(source);//進行SHA512加密
                    string hash = BitConverter.ToString(crypto).Replace("-", String.Empty);
                    string url = "http://pms.eden.org.tw/SingleSignOnLogin.aspx?token=" + emp_base64 + "&year=" + year + "&hash=" + hash;
                    Response.Redirect(url);
                }

                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('沒有使用此系統之權限。');location.href='http://eip.eden.org.tw'", true);
                }
            }
        }
    }
}