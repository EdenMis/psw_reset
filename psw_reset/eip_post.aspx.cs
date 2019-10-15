using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace psw_reset
{
    public partial class eip_post : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Form["EmployeeID"] != null && Request.Form["EmployeePWD"] != null)
                {
                    txb_emp_no.Text = Request.Form["EmployeeID"];
                    txb_user_pwd.Text = Request.Form["EmployeePWD"];
                }

                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('請透過EIP進來本頁');location.href='http://eip.eden.org.tw'", true);
                }
            }
        }
    }
}