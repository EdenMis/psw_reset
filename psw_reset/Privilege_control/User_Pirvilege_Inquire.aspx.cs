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


namespace psw_reset.Privilege_control
{
    public partial class User_Pirvilege_Inquire : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txb_user_id.Text != "")
            {
                SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                string sqlstring = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + txb_user_id.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
                HrsConn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    string user_group_name = Fn_Get_User_Group_name(txb_user_id.Text);
                    Label1.Text = "使用者「" + txb_user_id.Text + "」的身分群組為「" + user_group_name + "」對應權限如下表";
                    SqlDataSource1.SelectCommand = @"SELECT id,group_name,[first_level_function_name], [Visible_state] FROM [First_level_function] where group_name='" + user_group_name + "'";
                    SqlDataSource2.SelectCommand = @"SELECT id,[first_level_function_name], [Second_level_function_name], [fn_select], [fn_delete], [fn_insert], [fn_update], [fn_1], [fn_2], [fn_3], [fn_4] FROM [Second_level_function] where group_name='" + user_group_name + "'";
                    GV_level_1.DataBind();
                    GV_level_2.DataBind();
                }
                else
                {
                    string script = "alert('查無此同工，請確認')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                    HrsConn.Close();
                }
            }

            else
            {
                string script = "alert('同工編號不可為空!!')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
            }
        }

        public string Fn_Get_User_Group_name(string User_ID)
        {
            string user_group_name = "";
            SqlConnection Privilege_control_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Privilege_control"].ConnectionString);
            string sqlstring = @"SELECT top(1) [group_name] FROM [dbo].[Function_Privilege_Table] where group_user_id like '%" + User_ID + "%'";
            Privilege_control_Conn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, Privilege_control_Conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                user_group_name = dr["group_name"].ToString();
            }

            else
            {
                user_group_name = "All_User";
            }
            Privilege_control_Conn.Close();
            return user_group_name;
        }

        public class Emp_Info
        {
            public string User_ID { get; set; }
            public string User_CName { get; set; }
            public string User_Dep_ID { get; set; }
            public string User_Dep_Name { get; set; }
            public string First_Boss_ID { get; set; }
            public string First_Boss_CName { get; set; }
            public string Second_Boss_ID { get; set; }
            public string Second_Boss_CName { get; set; }
            public string User_Mail { get; set; }
        }

        public Emp_Info GetInfo(string user_id)
        {
            string FN_User_ID = "";
            string FN_User_CName = "";
            string FN_User_Dep_ID = "";
            string FN_User_Dep_CName = "";
            string FN_First_Boss_ID = "";
            string FN_First_Boss_CName = "";
            string FN_Second_Boss_ID = "";
            string FN_Second_Boss_CName = "";
            string FN_User_Mail = "";

            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
            string sqlstring = @"SELECT 
                                            a.[EMPLOYEE_NO],
                                            a.[EMPLOYEE_CNAME],
                                            a.[DEPARTMENT_CODE],
                                            a.[DEPARTMENT_CNAME],
                                            b.EMPLOYEE_NO as TOPIC_FIRST_BOSS_NO,
                                            b.EMPLOYEE_CNAME as TOPIC_FIRST_BOSS_CNAME,
                                            c.EMPLOYEE_NO as TOPIC_SECOND_BOSS_NO,
                                            c.EMPLOYEE_CNAME as TOPIC_SECOND_BOSS_CNAME,
                                            a.[EMPLOYEE_EMAIL_1]
                                            FROM [vwZZ_EMPLOYEE] as a left join 
                                            dbo.vwZZ_EMPLOYEE  as b on a.TOPIC_FIRST_BOSS_ID =b.EMPLOYEE_ID left join 
                                            [vwZZ_EMPLOYEE] as c on a.TOPIC_SECOND_BOSS_ID =c.EMPLOYEE_ID WHERE a.EMPLOYEE_NO='"+user_id+"'";
            HrsConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                FN_User_ID = dr["EMPLOYEE_NO"].ToString();
                FN_User_CName = dr["EMPLOYEE_CNAME"].ToString();
                FN_User_Dep_ID = dr["DEPARTMENT_CODE"].ToString();
                FN_User_Dep_CName = dr["DEPARTMENT_CNAME"].ToString();
                FN_First_Boss_ID = dr["TOPIC_FIRST_BOSS_NO"].ToString();
                FN_First_Boss_CName = dr["TOPIC_FIRST_BOSS_CNAME"].ToString();
                FN_Second_Boss_ID = dr["TOPIC_SECOND_BOSS_NO"].ToString();
                FN_Second_Boss_CName = dr["TOPIC_SECOND_BOSS_CNAME"].ToString();
                FN_User_Mail = dr["EMPLOYEE_EMAIL_1"].ToString();
            }

            return new Emp_Info
            {
                User_ID = FN_User_ID,
                User_CName = FN_User_CName,
                User_Dep_ID = FN_User_Dep_ID,
                User_Dep_Name = FN_User_Dep_CName,
                First_Boss_ID = FN_First_Boss_ID,
                First_Boss_CName = FN_First_Boss_CName,
                Second_Boss_ID = FN_Second_Boss_ID,
                Second_Boss_CName = FN_Second_Boss_CName,
                User_Mail = FN_User_Mail
            };
        }
    }
}