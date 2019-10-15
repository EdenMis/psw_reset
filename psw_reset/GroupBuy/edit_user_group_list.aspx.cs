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

namespace psw_reset.GroupBuy
{
    public partial class edit_user_group_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Login"].ToString() == null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('停留過久沒動作或沒經過驗證進來本頁，以上行為都需重新登入');location.href='../GroupBuy/'login.aspx'", true);
                }
            }

            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('停留過久沒動作或沒經過驗證進來本頁，以上行為都需重新登入');location.href='../GroupBuy/login.aspx'", true);
            }

            if (!Page.IsPostBack)
            {
                string PK = Request.QueryString["PK"];
                if (PK != "0")
                {
                    bool chk_owner = fn_chk_owner();
                    if (chk_owner == true)
                    {
                        fn_load_user_group_list();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('你沒有修改之權限');location.href='../GroupBuy/login.aspx'", true);
                    }
                }
            }
        }

        protected void btn_add_group_store_Click(object sender, EventArgs e)
        {
            string PK = Request.QueryString["PK"];
            if (PK == "0")
            {
                if (txb_user_group_name.Text != "")
                {
                    fn_ins_user_group_list();
                    string script = "window.parent.opener.location.href='../GroupBuy/user_group_list.aspx';";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "reload", script, true);
                    string script2 = "window.close();";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "close", script2, true);
                }
                else
                {
                    string script2 = "alert('群名未填寫，請確認!!')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
                }
            }
            else
            {
                if (txb_user_group_name.Text != "")
                {
                    fn_update_user_group_list(PK);
                    string script = "window.parent.opener.location.href='../GroupBuy/user_group_list.aspx';";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "reload", script, true);
                    string script2 = "window.close();";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "close", script2, true);
                }
                else
                {
                    string script2 = "alert('群名未填寫，請確認!!')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
                }
            }
        }

        public void fn_ins_user_group_list()
        {
            bool chk = fn_chk_dep_emp();
            if (chk == true)
            {
                SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
                GroupBuyConn.Open();
                string sqlstring = @"INSERT INTO user_group_list
                                                   (owner
                                                   ,user_group_name
                                                   ,dep
                                                   ,emp)
                                             VALUES
                                                   (@owner
                                                   ,@user_group_name
                                                   ,@dep
                                                   ,@emp)";
                SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
                cmd.Parameters.AddWithValue("@owner", Session["User_ID"].ToString());
                cmd.Parameters.AddWithValue("@user_group_name", txb_user_group_name.Text.Trim());
                cmd.Parameters.AddWithValue("@emp", txb_emp_Notice.Text.Trim());
                cmd.Parameters.AddWithValue("@dep", txb_dep_Notice.Text.Trim());
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
            else
            {
                string script2 = "alert('查無對應部門或同工，請確認格式或同工編號正確')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
            }
        }

        public void fn_update_user_group_list(string PK)
        {
            bool chk = fn_chk_dep_emp();
            if (chk == true)
            {
                SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
                GroupBuyConn.Open();
                string sqlstring = @"UPDATE user_group_list set
                                                   owner=@owner,user_group_name=@user_group_name,dep=@dep,emp=@emp where PK=@PK";
                SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
                cmd.Parameters.AddWithValue("@PK", PK);
                cmd.Parameters.AddWithValue("@owner", Session["User_ID"].ToString());
                cmd.Parameters.AddWithValue("@user_group_name", txb_user_group_name.Text.Trim());
                cmd.Parameters.AddWithValue("@emp", txb_emp_Notice.Text.Trim());
                cmd.Parameters.AddWithValue("@dep", txb_dep_Notice.Text.Trim());
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
            else
            {
                string script2 = "alert('查無對應部門或同工，請確認格式或同工編號正確')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
            }
        }

        public void fn_load_user_group_list()
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @"SELECT [PK]
      ,[owner]
      ,[user_group_name]
      ,[dep]
      ,[emp]
  FROM [GroupBuy].[dbo].[user_group_list] where PK='" + Request.QueryString["PK"] + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                txb_user_group_name.Text = dr["user_group_name"].ToString();
                txb_dep_Notice.Text = dr["dep"].ToString();
                txb_emp_Notice.Text = dr["emp"].ToString();
            }
            GroupBuyConn.Close();
        }

        public bool fn_chk_dep_emp()
        {
            bool chk = true;
            string err_emp="";
            if (txb_dep_Notice.Text != "")
            {
                String word = txb_dep_Notice.Text;
                String[] substrings = word.Split();

                foreach (var substring in substrings)
                {
                    SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                    HrsConn.Open();
                    string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where DEPARTMENT_CODE = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'  and EMPLOYEE_NO<>'1551'";
                    SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (!dr2.HasRows)
                    {
                        chk = false;
                    }
                    HrsConn.Close();
                }
            }

            if (txb_emp_Notice.Text != "")
            {
                String word = txb_emp_Notice.Text;
                String[] substrings = word.Split();
                foreach (var substring in substrings)
                {
                    SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                    HrsConn.Open();
                    string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'";
                    SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (!dr2.HasRows)
                    {
                        err_emp = err_emp +","+ substring;
                        chk = false;
                    }
                    HrsConn.Close();
                }
            }
            if (chk == false)
            {
                txb_emp_Notice.Text = err_emp+"為錯誤同工編號";
            }
            return chk;
        }

        public bool fn_chk_owner()
        {
            bool chk_owner = false;
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @"SELECT [PK]
                                                  ,[owner]
                                                  ,[user_group_name]
                                                  ,[dep]
                                                  ,[emp]
                                                FROM [GroupBuy].[dbo].[user_group_list] where PK='" + Request.QueryString["PK"] + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                string owner = dr["owner"].ToString();
                if (owner == Session["User_ID"].ToString())
                {
                    chk_owner = true;
                }
            }
            GroupBuyConn.Close();
            return chk_owner;
        }
    }
}