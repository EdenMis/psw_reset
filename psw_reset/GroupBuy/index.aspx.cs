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
    public partial class index1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Session["User_Dep_ID"].ToString() == "104" ||
        Session["User_Dep_ID"].ToString() == "105" ||
        Session["User_Dep_ID"].ToString() == "106" ||
        Session["User_Dep_ID"].ToString() == "107" ||
        Session["User_Dep_ID"].ToString() == "180" ||
        Session["User_Dep_ID"].ToString() == "351" ||
        Session["User_Dep_ID"].ToString() == "100" ||
        Session["User_Dep_ID"].ToString() == "091" ||
        Session["User_Dep_ID"].ToString() == "092" ||
        Session["User_Dep_ID"].ToString() == "093" ||
        Session["User_Dep_ID"].ToString() == "205" ||
        Session["User_Dep_ID"].ToString() == "182" ||
        Session["User_Dep_ID"].ToString() == "137" ||
        Session["User_Dep_ID"].ToString() == "138" ||
        Session["User_Dep_ID"].ToString() == "139" ||
        Session["User_Dep_ID"].ToString() == "333" ||
        Session["User_Dep_ID"].ToString() == "465" ||
        Session["User_Dep_ID"].ToString() == "368" ||
        Session["User_Dep_ID"].ToString() == "408" ||
        Session["User_Dep_ID"].ToString() == "460" ||
        Session["User_Dep_ID"].ToString() == "360" ||
        Session["User_Dep_ID"].ToString() == "183" ||
        Session["User_Dep_ID"].ToString() == "352" ||
        Session["User_Dep_ID"].ToString() == "505" ||
        Session["User_Dep_ID"].ToString() == "504" ||
        Session["User_Dep_ID"].ToString() == "177" ||
        Session["User_Dep_ID"].ToString() == "465" ||
        Session["User_Dep_ID"].ToString() == "366" ||
        Session["User_Dep_ID"].ToString() == "181" ||
        Session["User_Dep_ID"].ToString() == "446" ||
        Session["User_Dep_ID"].ToString() == "393" ||
        Session["User_Dep_ID"].ToString() == "178" ||
        Session["User_Dep_ID"].ToString() == "423" ||
        Session["User_Dep_ID"].ToString() == "424" ||
        Session["User_Dep_ID"].ToString() == "103" ||
        Session["User_Dep_ID"].ToString() == "152" ||
        Session["User_Dep_ID"].ToString() == "400" ||
        Session["User_Dep_ID"].ToString() == "479")
                    //if (Session["User_Dep_ID"].ToString() == "107")
                    {
                        lunch_div.Visible = true;
                        int lunch_open_id = GetInfo().lunch_open_id;
                        int store_id = GetInfo().store_id;
                        int store_good = GetInfo().store_good;
                        int store_ordinary = GetInfo().store_ordinary;
                        int store_bad = GetInfo().store_bad;
                        string can_order = GetInfo().can_order;
                        string store_name = GetInfo().store_name;
                        string lunch_note = GetInfo().lunch_note;
                        if (lunch_open_id != 0)
                        {
                            hyp_evaluation.NavigateUrl = "../GroupBuy/Store_evaluation.aspx?store_id=" + store_id + "";
                            hyp_evaluation.Text = "點此看評價";
                            lab_store_name.Text = store_name;
                            lab_good.Text = store_good.ToString();
                            lab_ordinary.Text = store_ordinary.ToString();
                            lab_bad.Text = store_bad.ToString();
                            lab_lunch_note.Text = lunch_note;
                            SqlDataSource3.SelectCommand = @"SELECT [PK],[店ID],[店名],[品名],[價格] FROM [Lunch_Store_meun] where 店ID='" + store_id + "' order by 品名";
                            GV_lunch_meun.DataBind();
                        }
                        else
                        {
                            string script = "alert('本日午餐尚未決定，請稍等喔!!')";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                        }

                        if (can_order != "True")
                        {
                            if (can_order == "False")
                            {
                                LinkButton2.Visible = true;
                            }
                            GV_lunch_meun.Visible = false;
                            btn_order.Visible = false;
                        }
                    }
                }

                catch
                {
                    ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('停留過久沒動作或沒經過驗證進來本頁，以上行為都需重新登入');location.href='../GroupBuy/login.aspx'", true);
                }
            }
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
            GroupBuyConn.Close();

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
        protected void GV_lunch_meun_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("OnMouseover", "this.style.backgroundColor='#ffff99'");
            }

            if (e.Row.RowState == DataControlRowState.Alternate)
            {
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");
            }

            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#6B696B'");
            }
            else
            {
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#F7F7DE'");
            }
        }
        protected void btn_order_Click(object sender, EventArgs e)
        {
            int sum = 0;
            bool chk = true;
            for (int i = 0; i < GV_lunch_meun.Rows.Count; i++)
            {
                TextBox tb = (TextBox)GV_lunch_meun.Rows[i].FindControl("txb_buy_num");
                string meun_number = tb.Text.Trim();
                if (!(new System.Text.RegularExpressions.Regex("^[0-9]+$")).IsMatch(meun_number))
                {
                    chk = false;
                }
            }

            for (int i = 0; i < GV_lunch_meun.Rows.Count; i++)
            {
                try
                {
                    TextBox tb = (TextBox)GV_lunch_meun.Rows[i].FindControl("txb_buy_num");
                    sum = sum + Convert.ToInt32(tb.Text.Trim());
                }
                catch
                {
                    chk = false;
                }
            }

            if (chk == true && sum != 0)
            {
                string order_date = DateTime.Now.ToString("yyyy-MM-dd");
                string store_name = GetInfo().store_name;


                for (int i = 0; i < GV_lunch_meun.Rows.Count; i++)
                {
                    string meun_name = GV_lunch_meun.Rows[i].Cells[0].Text;
                    int meun_money = Convert.ToInt32(GV_lunch_meun.Rows[i].Cells[1].Text);
                    TextBox tb = (TextBox)GV_lunch_meun.Rows[i].FindControl("txb_buy_num");
                    int meun_number = Convert.ToInt32(tb.Text.Trim());
                    TextBox tb2 = (TextBox)GV_lunch_meun.Rows[i].FindControl("txb_buy_note");
                    string meun_note = tb2.Text.Trim();
                    if (meun_note != "")
                    {
                        meun_name = meun_name + '_' + meun_note;
                    }
                    if (meun_number != 0)
                    {
                        for (int j = 0; j < meun_number; j++)
                        {
                            del_lunch_Voting_rights(order_date);
                            ins_lunch_order_table(order_date, store_name, meun_name, meun_money, 1, meun_note);
                            ins_lunch_Voting_rights(order_date);
                        }
                    }
                }
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('訂餐成功!!');location.href='../GroupBuy/My_lunch_order.aspx'", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('未填寫訂單，或訂單數量未填整數!!');", true);
            }
        }
        public void ins_lunch_order_table(string order_date, string store_name, string meun_name, int meun_money, int meun_number, string meun_note)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"INSERT INTO lunch_order_table 
(訂餐日期,午餐店家,訂餐人姓名,訂餐者同編,訂餐品項,品項單價,品項數量,支付狀態,訂餐者部門,備註) 
VALUES (@訂餐日期,@午餐店家,@訂餐人姓名,@訂餐者同編,@訂餐品項,@品項單價,@品項數量,'未支付',@訂餐者部門,@備註)";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@訂餐日期", order_date);
            cmd.Parameters.AddWithValue("@午餐店家", store_name);
            cmd.Parameters.AddWithValue("@訂餐人姓名", Session["User_Name"].ToString());
            cmd.Parameters.AddWithValue("@訂餐者同編", Session["User_ID"].ToString());
            cmd.Parameters.AddWithValue("@訂餐者部門", Session["User_Dep_ID"].ToString());
            cmd.Parameters.AddWithValue("@訂餐品項", meun_name.Trim());
            cmd.Parameters.AddWithValue("@品項單價", meun_money);
            cmd.Parameters.AddWithValue("@品項數量", meun_number);
            cmd.Parameters.AddWithValue("@備註", meun_note);
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }
        public void del_lunch_Voting_rights(string order_date)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"DELETE FROM  lunch_Voting_rights WHERE  日期=@日期 AND 訂餐人同編=@訂餐人同編";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@日期", order_date);
            cmd.Parameters.AddWithValue("@訂餐人同編", Session["User_ID"].ToString());
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }
        public void ins_lunch_Voting_rights(string order_date)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"INSERT INTO lunch_Voting_rights 
                                                    ([日期],[訂餐人同編],[是否投票]) 
                                                    VALUES (@日期,@訂餐人同編,'0')";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@日期", order_date);
            cmd.Parameters.AddWithValue("@訂餐人同編", Session["User_ID"].ToString());
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }
    }
}