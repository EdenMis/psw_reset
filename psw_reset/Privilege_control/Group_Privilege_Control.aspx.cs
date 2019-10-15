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
    public partial class Group_Privilege_Control : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Dlist_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataSource2.SelectCommand= @"SELECT a.[id]
                                                                                  ,a.[First_level_function_id]
	                                                                              ,b.first_level_function_name
                                                                                  ,[group_name]
                                                                                  ,[Visible_state]
                                                                                  FROM [Privilege_control].[dbo].[First_level_function_detail] as a left join
                                                                                  First_level_function as b on a.First_level_function_id=b.First_level_function_id where group_name='"+Dlist_group.Text+"'";
            GV_first_fn.DataBind();

            SqlDataSource3.SelectCommand = @"SELECT 
                                                                                  a.id,
                                                                                  a.Second_level_function_id,
                                                                                  b.Second_level_function_name,
                                                                                  a.group_name,
                                                                                  a.Visible_state,
                                                                                  a.fn_select,
                                                                                  a.fn_update,
                                                                                  a.fn_insert,
                                                                                  a.fn_delete,
                                                                                  a.fn_1,
                                                                                  a.fn_2,
                                                                                  a.fn_3,
                                                                                  a.fn_4
                                                                                  FROM Second_level_function_detail AS a left join 
                                                                                  Second_level_function as b on a.Second_level_function_id=b.Second_level_function_id where group_name='" + Dlist_group.Text + "'";
            GV_Second_fn.DataBind();

            SqlDataSource4.SelectCommand = @"SELECT [id], [user_id], [user_name], [user_group] FROM [group_user] where user_group='" + Dlist_group.Text + "'";
            GV_User.DataBind();

            SqlDataSource5.SelectCommand = @"SELECT [id], [dep_id], [dep_name], [dep_group] FROM [group_dep] where dep_group='" + Dlist_group.Text + "'";
            GV_Dep.DataBind();
        }
        protected void GV_first_fn_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SqlDataSource2.SelectCommand = @"SELECT a.[id]
                                                                                  ,a.[First_level_function_id]
	                                                                              ,b.first_level_function_name
                                                                                  ,[group_name]
                                                                                  ,[Visible_state]
                                                                                 FROM [Privilege_control].[dbo].[First_level_function_detail] as a left join
                                                                                 First_level_function as b on a.First_level_function_id=b.First_level_function_id where group_name='" + Dlist_group.Text + "'";
            GV_first_fn.DataBind();
        }
        protected void GV_first_fn_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            SqlDataSource2.SelectCommand = @"SELECT a.[id]
                                                                                  ,a.[First_level_function_id]
	                                                                              ,b.first_level_function_name
                                                                                  ,[group_name]
                                                                                  ,[Visible_state]
                                                                                  FROM [Privilege_control].[dbo].[First_level_function_detail] as a left join
                                                                                  First_level_function as b on a.First_level_function_id=b.First_level_function_id where group_name='" + Dlist_group.Text + "'";
            GV_first_fn.DataBind();
        }
        protected void GV_first_fn_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SqlDataSource2.SelectCommand = @"SELECT a.[id]
      ,a.[First_level_function_id]
	  ,b.first_level_function_name
      ,[group_name]
      ,[Visible_state]
     FROM [Privilege_control].[dbo].[First_level_function_detail] as a left join
     First_level_function as b on a.First_level_function_id=b.First_level_function_id where group_name='" + Dlist_group.Text + "'";
            GV_first_fn.DataBind();
        }
        protected void GV_Second_fn_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SqlDataSource3.SelectCommand = @"  SELECT 
                                                                                  a.id,
                                                                                  a.Second_level_function_id,
                                                                                  b.Second_level_function_name,
                                                                                  a.group_name,
                                                                                  a.Visible_state,
                                                                                  a.fn_select,
                                                                                  a.fn_update,
                                                                                  a.fn_insert,
                                                                                  a.fn_delete,
                                                                                  a.fn_1,
                                                                                  a.fn_2,
                                                                                  a.fn_3,
                                                                                  a.fn_4
                                                                                  FROM Second_level_function_detail AS a left join 
                                                                                  Second_level_function as b on a.Second_level_function_id=b.Second_level_function_id where group_name='" + Dlist_group.Text + "'";
            GV_Second_fn.DataBind();
        }
        protected void GV_Second_fn_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            SqlDataSource3.SelectCommand = @"  SELECT 
                                                                                  a.id,
                                                                                  a.Second_level_function_id,
                                                                                  b.Second_level_function_name,
                                                                                  a.group_name,
                                                                                  a.Visible_state,
                                                                                  a.fn_select,
                                                                                  a.fn_update,
                                                                                  a.fn_insert,
                                                                                  a.fn_delete,
                                                                                  a.fn_1,
                                                                                  a.fn_2,
                                                                                  a.fn_3,
                                                                                  a.fn_4
                                                                                  FROM Second_level_function_detail AS a left join 
                                                                                  Second_level_function as b on a.Second_level_function_id=b.Second_level_function_id where group_name='" + Dlist_group.Text + "'";
            GV_Second_fn.DataBind();
        }
        protected void GV_Second_fn_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SqlDataSource3.SelectCommand = @"  SELECT 
                                                                                  a.id,
                                                                                  a.Second_level_function_id,
                                                                                  b.Second_level_function_name,
                                                                                  a.group_name,
                                                                                  a.Visible_state,
                                                                                  a.fn_select,
                                                                                  a.fn_update,
                                                                                  a.fn_insert,
                                                                                  a.fn_delete,
                                                                                  a.fn_1,
                                                                                  a.fn_2,
                                                                                  a.fn_3,
                                                                                  a.fn_4
                                                                                  FROM Second_level_function_detail AS a left join 
                                                                                  Second_level_function as b on a.Second_level_function_id=b.Second_level_function_id where group_name='" + Dlist_group.Text + "'";
            GV_Second_fn.DataBind();
        }

        protected void btn_add_group_Click(object sender, EventArgs e)
        {
            if (txb_group_name.Text != "")
            {
                bool group_name_chk = Fn_check_group_name(txb_group_name.Text);
                if (group_name_chk == true)
                {
                    Fn_cearte_group(txb_group_name.Text.Trim());
                    ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('新建群組「"+ txb_group_name.Text + "」完成，請選擇該群組進行權限設定');location.href='Group_Privilege_Control.aspx'", true);
                }
                else
                {
                    string script = "alert('已有重複之群組名稱!!')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                }
            }

            else
            {
                string script = "alert('群組名稱未填!!')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
            }
        }

        protected void btn_delete_group_Click(object sender, EventArgs e)
        {
            if (Dlist_group.Text != "")
            {
                FN_del_group_user(Dlist_group.Text);
                FN_del_group_dep(Dlist_group.Text);
                FN_del_Second_level_function_detail(Dlist_group.Text);
                FN_del_First_level_function_detail(Dlist_group.Text);
                FN_del_group_list(Dlist_group.Text);
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('刪除群組「" + Dlist_group.Text + "」完成');location.href='../Privilege_control/Group_Privilege_Control.aspx'", true);
            }
            else
            {
                string script = "alert('未選擇要刪除的群組')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
            }
        }

        public void Fn_cearte_group(string group_name)
        {
            FN_ins_group_list(group_name);
            int first_fn_id = 0;
            int second_fn_id = 0;
            SqlConnection Privilege_control_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Privilege_control"].ConnectionString);
            string sqlstring = @"SELECT [First_level_function_id] ,[first_level_function_name] FROM [dbo].[First_level_function]";
            Privilege_control_Conn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, Privilege_control_Conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while(dr.Read())
                {
                    first_fn_id = Convert.ToInt32(dr["First_level_function_id"]);
                    FN_ins_First_level_function_detail(first_fn_id, group_name);
                }
            }
            Privilege_control_Conn.Close();
            Privilege_control_Conn.Open();

            string sqlstring2 = @"SELECT [Second_level_function_id]
                                                                    ,[Second_level_function_name]
                                                                     FROM [Privilege_control].[dbo].[Second_level_function]";
            SqlCommand cmd2 = new SqlCommand(sqlstring2, Privilege_control_Conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    second_fn_id = Convert.ToInt32(dr2["Second_level_function_id"]);
                    FN_ins_Second_level_function_detail(second_fn_id, group_name);
                }
            }
            Privilege_control_Conn.Close();
        }
        public void FN_ins_group_list(string group_name_0)
        {
            SqlConnection Privilege_control_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Privilege_control"].ConnectionString);
            Privilege_control_Conn.Open();
            string sqlstring = @"INSERT INTO group_list 
                                                  (group_name) 
                                                  VALUES (@group_name)";
            SqlCommand cmd = new SqlCommand(sqlstring, Privilege_control_Conn);
            cmd.Parameters.AddWithValue("@group_name", group_name_0);
            cmd.ExecuteNonQuery();
            Privilege_control_Conn.Close();
        }
        public void FN_ins_First_level_function_detail(int First_level_function_id, string group_name_1)
        {
            SqlConnection Privilege_control_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Privilege_control"].ConnectionString);
            Privilege_control_Conn.Open();
            string sqlstring = @"INSERT INTO First_level_function_detail 
                                                  (First_level_function_id,group_name) 
                                                  VALUES (@First_level_function_id,@group_name)";
            SqlCommand cmd = new SqlCommand(sqlstring, Privilege_control_Conn);
            cmd.Parameters.AddWithValue("@First_level_function_id", First_level_function_id);
            cmd.Parameters.AddWithValue("@group_name", group_name_1);
            cmd.ExecuteNonQuery();
            Privilege_control_Conn.Close();
        }
        public void FN_ins_Second_level_function_detail(int Second_level_function_id, string group_name_2)
        {
            SqlConnection Privilege_control_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Privilege_control"].ConnectionString);
            Privilege_control_Conn.Open();
            string sqlstring = @"INSERT INTO Second_level_function_detail 
                                                  (Second_level_function_id,group_name) 
                                                  VALUES (@Second_level_function_id,@group_name)";
            SqlCommand cmd = new SqlCommand(sqlstring, Privilege_control_Conn);
            cmd.Parameters.AddWithValue("@Second_level_function_id", Second_level_function_id);
            cmd.Parameters.AddWithValue("@group_name", group_name_2);
            cmd.ExecuteNonQuery();
            Privilege_control_Conn.Close();
        }
        public bool Fn_check_group_name(string group_name)
        {
            bool chk = true;
            SqlConnection Privilege_control_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Privilege_control"].ConnectionString);
            string sqlstring = @"SELECT [group_id]
                                                                ,[group_name]
                                                              FROM [dbo].[group_list] where group_name='"+ group_name + "'";
            Privilege_control_Conn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, Privilege_control_Conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                chk = false;
            }
            return chk;
        }

        public void FN_del_group_list(string group_name_0)
        {
            SqlConnection Privilege_control_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Privilege_control"].ConnectionString);
            Privilege_control_Conn.Open();
            string sqlstring = @"delete from group_list where group_name=@group_name";
            SqlCommand cmd = new SqlCommand(sqlstring, Privilege_control_Conn);
            cmd.Parameters.AddWithValue("@group_name", group_name_0);
            cmd.ExecuteNonQuery();
            Privilege_control_Conn.Close();
        }
        public void FN_del_First_level_function_detail(string group_name_1)
        {
            SqlConnection Privilege_control_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Privilege_control"].ConnectionString);
            Privilege_control_Conn.Open();
            string sqlstring = @"delete from First_level_function_detail where group_name=@group_name";
            SqlCommand cmd = new SqlCommand(sqlstring, Privilege_control_Conn);
            cmd.Parameters.AddWithValue("@group_name", group_name_1);
            cmd.ExecuteNonQuery();
            Privilege_control_Conn.Close();
        }
        public void FN_del_Second_level_function_detail(string group_name_2)
        {
            SqlConnection Privilege_control_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Privilege_control"].ConnectionString);
            Privilege_control_Conn.Open();
            string sqlstring = @"delete from Second_level_function_detail where group_name=@group_name";
            SqlCommand cmd = new SqlCommand(sqlstring, Privilege_control_Conn);
            cmd.Parameters.AddWithValue("@group_name", group_name_2);
            cmd.ExecuteNonQuery();
            Privilege_control_Conn.Close();
        }
        public void FN_del_group_user(string group_name)
        {
            SqlConnection Privilege_control_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Privilege_control"].ConnectionString);
            Privilege_control_Conn.Open();
            string sqlstring = @"delete from [dbo].[group_user] where user_group=@user_group";
            SqlCommand cmd = new SqlCommand(sqlstring, Privilege_control_Conn);
            cmd.Parameters.AddWithValue("@user_group", group_name);
            cmd.ExecuteNonQuery();
            Privilege_control_Conn.Close();
        }
        public void FN_del_group_dep(string group_name)
        {
            SqlConnection Privilege_control_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Privilege_control"].ConnectionString);
            Privilege_control_Conn.Open();
            string sqlstring = @"delete from [dbo].[group_dep] where dep_group=@dep_group";
            SqlCommand cmd = new SqlCommand(sqlstring, Privilege_control_Conn);
            cmd.Parameters.AddWithValue("@dep_group", group_name);
            cmd.ExecuteNonQuery();
            Privilege_control_Conn.Close();
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
                                            [vwZZ_EMPLOYEE] as c on a.TOPIC_SECOND_BOSS_ID =c.EMPLOYEE_ID WHERE a.EMPLOYEE_NO='" + user_id + "'";
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