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
    public partial class Lunch_voting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    bool voting = fn_chk_voting();
                    if (voting == true)
                    {
                        lab_store_name.Text = GetInfo().store_name.ToString();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('您沒有投票權或已投過票');window.open('','_self','');window.close();", true);
                    }
                }
                catch
                {

                }
            }
        }
        public bool fn_chk_voting()
        {
            string order_date = DateTime.Now.ToString("yyyy-MM-dd");
            string emp_id = Session["User_ID"].ToString();
            bool chk_voting = false;
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @"SELECT [PK]
      ,[日期]
      ,[訂餐人同編]
      ,[是否投票]
  FROM [dbo].[lunch_Voting_rights] where 日期='"+ order_date + "' AND 訂餐人同編='" + emp_id + "' AND 是否投票='0'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                chk_voting = true;
            }
            GroupBuyConn.Close();
            return chk_voting;
        }

        public class Store_Info
        {
            public int lunch_open_id { get; set; }
            public int store_id { get; set; }
            public int store_good { get; set; }
            public int store_ordinary { get; set; }
            public int store_bad { get; set; }
            public string can_order { get; set; }
            public string store_name { get; set; }
            public string lunch_note { get; set; }
        }
        public Store_Info GetInfo()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            int fn_lunch_open_id = 0;
            int fn_store_id = 0;
            int fn_store_good = 0;
            int fn_store_ordinary = 0;
            int fn_store_bad = 0;
            string fn_can_order = "";
            string fn_store_name = "";
            string fn_lunch_note = "";

            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @"SELECT a.日期,a.PK as 午餐團ID,a.店ID,a.店名,b.地址,b.電話,b.備註 as 店家備註,a.備註 as 總務備註,b.good as 好吃,b.ordinary as 普通,b.bad as 難吃,a.是否可訂 from Lunch_set_main as a left join Lunch_Store_main as b on a.店ID=b.PK where a.日期 ='" + today + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                fn_lunch_open_id = Convert.ToInt32(dr["午餐團ID"]);
                fn_store_id = Convert.ToInt32(dr["店ID"]);
                fn_store_good = Convert.ToInt32(dr["好吃"]);
                fn_store_ordinary = Convert.ToInt32(dr["普通"]);
                fn_store_bad = Convert.ToInt32(dr["難吃"]);
                fn_store_name = dr["店名"].ToString();
                fn_lunch_note = dr["總務備註"].ToString();
                fn_can_order = dr["是否可訂"].ToString();
            }

            return new Store_Info
            {
                lunch_open_id = fn_lunch_open_id,
                store_id = fn_store_id,
                store_good = fn_store_good,
                store_ordinary = fn_store_ordinary,
                store_bad = fn_store_bad,
                store_name = fn_store_name,
                lunch_note = fn_lunch_note,
                can_order = fn_can_order
            };
        }

        protected void btn_voting_Click(object sender, EventArgs e)
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            int lunch_open_id = GetInfo().lunch_open_id;
            int store_id = GetInfo().store_id;
            int store_good = GetInfo().store_good;
            int store_ordinary = GetInfo().store_ordinary;
            int store_bad = GetInfo().store_bad;
            string can_order = GetInfo().can_order;
            string voting_item = "";

            if (rdo_bad.Checked == true)
            {
                store_bad = store_bad + 1;
                voting_item = "難吃";
            }
            if (rdo_ordinary.Checked == true)
            {
                store_ordinary = store_ordinary + 1;
                voting_item = "普通";
            }
            if (rdo_good.Checked == true)
            {
                store_good = store_good + 1;
                voting_item = "好吃";
            }

            ins_lunch_Voting_detail(store_id, today, txb_note.Text.Trim(), voting_item);
            update_Voting_rights(today);
            update_store_evaluation(store_good, store_ordinary, store_bad, store_id);
            string script = "window.parent.opener.location.href='../GroupBuy/index.aspx';";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "reload", script, true);
            ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('投票完成');window.open('','_self','');window.close()", true);
        }

        public void ins_lunch_Voting_detail(int store_id, string voting_date, string voting_note, string voting_item)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"INSERT INTO lunch_Voting_detail 
                                                    ([店ID],[投票日期],[投票人同編],[投票人姓名],[投票人部門],[選項],[意見]) 
                                                    VALUES (@店ID,@投票日期,@投票人同編,@投票人姓名,@投票人部門,@選項,@意見)";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@店ID", store_id);
            cmd.Parameters.AddWithValue("@投票日期",voting_date);
            cmd.Parameters.AddWithValue("@投票人同編", Session["User_ID"].ToString());
            cmd.Parameters.AddWithValue("@投票人姓名", Session["User_Name"].ToString());
            cmd.Parameters.AddWithValue("@投票人部門", Session["User_Dep_ID"].ToString());
            cmd.Parameters.AddWithValue("@選項", voting_item);
            cmd.Parameters.AddWithValue("@意見", voting_note);
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }
        public void update_Voting_rights(string voting_date)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"UPDATE dbo.lunch_Voting_rights SET 是否投票='1' WHERE 日期=@日期 and 訂餐人同編=@訂餐人同編";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@日期", voting_date);
            cmd.Parameters.AddWithValue("@訂餐人同編", Session["User_ID"].ToString());
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }
        public void update_store_evaluation(int store_good,int store_ordinary,int store_bad,int store_id)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"UPDATE dbo.Lunch_Store_main SET good=@good,ordinary=@ordinary,bad=@bad WHERE PK=@PK";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@good", store_good);
            cmd.Parameters.AddWithValue("@ordinary", store_ordinary);
            cmd.Parameters.AddWithValue("@bad", store_bad);
            cmd.Parameters.AddWithValue("@PK", store_id);
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }
    }
}