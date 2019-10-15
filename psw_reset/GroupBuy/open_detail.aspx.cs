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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace psw_reset.GroupBuy
{
    public partial class open_detail : System.Web.UI.Page
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
            //if (!Page.IsPostBack)
            //{
                string store_name = fn_get_group_store_name();
                if (store_name != "")
                {
                    fn_load_Group_main_table();
                    fn_load_Group_store_table(store_name);
                    fn_load_Group_store_meun_table(store_name);
                }

                string store_type = lab_store_type.Text;
                if (store_type == "飲料")
                {
                    table_drink_order.Visible = true;
                }
                else
                {
                    table_other_order.Visible = true;
                }
            //}
        }

        public void fn_load_Group_main_table()
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @"SELECT [PK],[開團人部門],[開團人],開團備註,[開團標題],[狀態] FROM Group_main_table where PK='" + Request.QueryString["PK"] + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                lab_group_name.Text = dr["開團標題"].ToString();
                lab_group_emp_name.Text = dr["開團人"].ToString();
                txb_group_note.Text = dr["開團備註"].ToString();
            }
            GroupBuyConn.Close();
        }

        public void fn_load_Group_store_table(string store_name)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @"SELECT 店名,店家網址,店家備註,菜單檔名,類型 FROM Group_store_table where 店名='" + store_name + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                lab_store_name.Text = dr["店名"].ToString();
                lab_store_type.Text = dr["類型"].ToString();
                if (dr["店家網址"].ToString() != "")
                {
                    hyp_store_web_addr.Visible = true;
                    hyp_store_web_addr.Text = "店家連結網址";
                    hyp_store_web_addr.NavigateUrl = dr["店家網址"].ToString();
                }
                txb_group_store_note.Text = dr["店家備註"].ToString();

                if (dr["菜單檔名"].ToString() != "")
                {
                    img_meun.Visible = true;
                    img_meun.ImageUrl = "~/menu/" + dr["菜單檔名"].ToString();
                    img_meun.NavigateUrl = "~/menu/" + dr["菜單檔名"].ToString();
                }
            }
            GroupBuyConn.Close();
        }
        public void fn_load_Group_store_meun_table(string store_name)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @"SELECT file_name FROM Group_store_meun_table where store_name='" + store_name + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int i = 0;
                    Label html_string = new Label();
                    //HyperLink aa = new HyperLink();
                    //aa.Text = dr["file_name"].ToString();
                    //aa.ImageUrl = "~/menu/" + dr["file_name"].ToString();
                    //aa.NavigateUrl = "~/menu/" + dr["file_name"].ToString();
                    //aa.CssClass = "img_group";
                    //aa.ID = "img" + i.ToString();
                    string img_id = "img" + i.ToString();
                    string url = "../menu/"+ dr["file_name"].ToString();;
               

                    html_string.Text = "<a href='" + url + "' class='img_group'><img src='" + url + "' id="+img_id+" /></a>";

                    img_panel.Controls.Add(html_string);
                    i++;
                }
            }
            GroupBuyConn.Close();
        }

        protected void btn_follow2_Click(object sender, EventArgs e)
        {
            string store_type = lab_store_type.Text;
            if (txb_order_name.Text != "" && txb_order_num.Text != "" && txb_meun_money.Text != "")
            {
                if (store_type == "飲料")
                {
                    bool chk = true;
                    string order_name = txb_order_name.Text.Trim();
                    string order_number = txb_order_num.Text.Trim();
                    string size = "";
                    string ice = "";
                    string sweet = "";
                    string meun_name = "";
                    string note = "";
                    if (Dlist_drink_size.Text != "請選擇")
                    {
                        size = Dlist_drink_size.Text;
                    }
                    if (Dlist_drink_size.Text != "請選擇")
                    {
                        ice = Dlist_drink_ice.Text;
                    }
                    if (Dlist_drink_size.Text != "請選擇")
                    {
                        sweet = Dlist_drink_sweetness.Text;
                    }
                    if (size != "")
                    {
                        if (note == "")
                        {
                            note = size;
                        }
                        else
                        {
                            note = note + "-" + size;
                        }
                    }
                    if (ice != "")
                    {
                        if (note == "")
                        {
                            note = ice;
                        }
                        else
                        {
                            note = note + "-" + ice;
                        }
                    }

                    if (sweet != "")
                    {
                        if (note == "")
                        {
                            note = sweet;
                        }
                        else
                        {
                            note = note + "-" + sweet;
                        }
                    }
                    if (txb_drink_note.Text.Trim() != "")
                    {
                        if (note == "")
                        {
                            note = txb_drink_note.Text.Trim();
                        }
                        else
                        {
                            note = note + "-" + txb_drink_note.Text.Trim();
                        }
                    }

                    if (note != "")
                    {
                        meun_name = order_name + "(" + note + ")";
                    }
                    else
                    {
                        meun_name = order_name;
                    }

                    string meun_money = txb_meun_money.Text.Trim();
                    string meun_number = txb_order_num.Text.Trim();
                    string meun_note = txb_drink_note.Text.Trim();

                    if (!(new System.Text.RegularExpressions.Regex("^[0-9]+$")).IsMatch(order_number))
                    {
                        chk = false;
                    }

                    if (!(new System.Text.RegularExpressions.Regex("^[0-9]+$")).IsMatch(meun_money))
                    {
                        chk = false;
                    }

                    if (chk == true)
                    {
                        string Group_NO = "";
                        string Group_name = "";
                        SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
                        string sqlstring = @"SELECT [PK],[開團人部門],[開團人],開團備註,[開團標題],[菜單檔名],[狀態] FROM Group_main_table where PK='" + Request.QueryString["PK"] + "'";
                        GroupBuyConn.Open();
                        SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            Group_NO = dr["PK"].ToString().Trim();
                            Group_name = dr["開團標題"].ToString().Trim();
                        }
                        GroupBuyConn.Close();
                        ins_fixed_follow_Group_table(Group_NO, Group_name, meun_name, meun_money, meun_number, meun_note);
                        GridView2.DataBind();
                        GridView3.DataBind();
                        ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('跟團成功!!');", true);
                        string store_name = fn_get_group_store_name();
                        fn_load_Group_store_meun_table(store_name);
                    }

                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('數量及金額格式不正確!!');", true);
                        string store_name = fn_get_group_store_name();
                        fn_load_Group_store_meun_table(store_name);
                    }
                }
                else
                {
                    bool chk = true;
                    string order_name = txb_order_name.Text.Trim();
                    string order_number = txb_order_num.Text.Trim();
                    if (txb_other_note.Text.Trim() != "")
                    {
                        order_name = order_name + "(" + txb_other_note.Text.Trim() + ")";
                    }
                    string meun_name = order_name;
                    string meun_money = txb_meun_money.Text.Trim();
                    string meun_number = txb_order_num.Text.Trim();
                    string meun_note = txb_other_note.Text.Trim();

                    if (!(new System.Text.RegularExpressions.Regex("^[0-9]+$")).IsMatch(order_number))
                    {
                        chk = false;
                    }

                    if (!(new System.Text.RegularExpressions.Regex("^[0-9]+$")).IsMatch(meun_money))
                    {
                        chk = false;
                    }

                    if (chk == true)
                    {
                        string Group_NO = "";
                        string Group_name = "";
                        SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
                        string sqlstring = @"SELECT [PK],[開團人部門],[開團人],開團備註,[開團標題],[菜單檔名],[狀態] FROM Group_main_table where PK='" + Request.QueryString["PK"] + "'";
                        GroupBuyConn.Open();
                        SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            Group_NO = dr["PK"].ToString().Trim();
                            Group_name = dr["開團標題"].ToString().Trim();
                        }
                        GroupBuyConn.Close();
                        ins_fixed_follow_Group_table(Group_NO, Group_name, meun_name, meun_money, meun_number, meun_note);
                        GridView2.DataBind();
                        GridView3.DataBind();
                        ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('跟團成功!!');", true);
                    }

                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('數量及金額格式不正確!!');", true);
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('品名，數量，價格不可為空');", true);
            }
            //bool chk = true;
            //for (int i = 0; i <GV_fixed_meun.Rows.Count; i++)
            //{
            //    TextBox tb = (TextBox)GV_fixed_meun.Rows[i].FindControl("txb_buy_num");
            //    string meun_number = tb.Text.Trim();
            //    if (!(new System.Text.RegularExpressions.Regex("^[0-9]+$")).IsMatch(meun_number))
            //    {
            //        chk = false;
            //    }
            //}

            //if (chk == true)
            //{
            //    string Group_NO ="";
            //    string Group_name="";
            //    SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            //    string sqlstring = @"SELECT [PK],[開團人部門],[開團人],開團備註,[開團標題],[菜單檔名],[狀態] FROM Group_main_table where PK='" + Request.QueryString["PK"] + "'";
            //    GroupBuyConn.Open();
            //    SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            //    SqlDataReader dr = cmd.ExecuteReader();
            //    if (dr.HasRows)
            //    {
            //        dr.Read();
            //        Group_NO = dr["PK"].ToString().Trim();
            //        Group_name = dr["開團標題"].ToString().Trim();
            //    }
            //    GroupBuyConn.Close();


            //    for (int i = 0; i < GV_fixed_meun.Rows.Count; i++)
            //    {
            //        string meun_name = GV_fixed_meun.Rows[i].Cells[0].Text;
            //        int meun_money =Convert.ToInt32(GV_fixed_meun.Rows[i].Cells[1].Text);
            //        TextBox tb = (TextBox)GV_fixed_meun.Rows[i].FindControl("txb_buy_num");
            //        int meun_number = Convert.ToInt32(tb.Text.Trim());
            //        TextBox tb2 = (TextBox)GV_fixed_meun.Rows[i].FindControl("txb_buy_note");
            //        string meun_note = tb2.Text.Trim();
            //        if (meun_number != 0)
            //        {
            //            ins_fixed_follow_Group_table(Group_NO, Group_name, meun_name, meun_money, meun_number, meun_note);
            //        }
            //    }
            //    ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('跟團成功!!');window.close();", true);
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('下單量請填整數!!');", true);
            //}
        }

        protected void GridView2_PreRender(object sender, EventArgs e)
        {
            int money_all = 0;
            string emp_id = "";
            string emp_id_2 = "";
            string emp_name = "";
            GridViewRow titlerow = null;
            TableCell title = null;
            TableCell title2 = null;
            GridViewRow money_row = null;
            GridViewRow datarow = null;
            TableCell meun_name = null;
            //TableCell meun_note = null;
            TableCell meun_money = null;
            TableCell meun_number = null;
            TableCell meun_money_sum = null;
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                GridView2.Rows[i].Visible = false;
                //檔案類型!=目前的檔案類型...就產生一個新的標題列
                if (emp_id != "" && emp_id != GridView2.Rows[i].Cells[0].Text)
                {
                    //產生新<tr>，後面的參數不要問我為什麼，我抄來的
                    money_row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    emp_id_2 = GridView2.Rows[i].Cells[0].Text;
                    //產生新的<td>
                    title2 = new TableCell();
                    //合併<td>
                    title2.ColumnSpan = 6;
                    //塞入文字，檔案類型
                    title2.Text = "合計：" + money_all.ToString();
                    //把td塞到tr
                    money_row.Controls.Add(title2);
                    //加入css樣式
                    title2.Attributes.Add("style", "background-color:#C3E6CB;text-align: center;");
                    //把tr塞到gridview
                    GridView2.Controls[0].Controls.Add(money_row);
                    money_all = 0;
                }

                if (emp_id != GridView2.Rows[i].Cells[0].Text)
                {
                    //產生新<tr>，後面的參數不要問我為什麼，我抄來的
                    titlerow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    //取得目前的檔案類型
                    emp_id = GridView2.Rows[i].Cells[0].Text;
                    emp_name = GridView2.Rows[i].Cells[1].Text;
                    //產生新的<td>
                    title = new TableCell();
                    //合併<td>
                    title.ColumnSpan = 6;
                    //塞入文字，檔案類型
                    title.Text = emp_id + "_" + emp_name;
                    //把td塞到tr
                    titlerow.Controls.Add(title);
                    //加入css樣式
                    //title.CssClass = "table-info";
                    title.Attributes.Add("style", "background-color:#B8DAFF;text-align: center;");
                    //把tr塞到gridview
                    GridView2.Controls[0].Controls.Add(titlerow);
                }
                //再來塞資料囉~~~
                datarow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                meun_name = new TableCell();
                //meun_note = new TableCell();
                meun_money = new TableCell();
                meun_number = new TableCell();
                meun_money_sum = new TableCell();
                meun_name.Text = GridView2.Rows[i].Cells[2].Text;
                //meun_note.Text = GridView2.Rows[i].Cells[3].Text;
                meun_money.Text = GridView2.Rows[i].Cells[3].Text;
                meun_number.Text = GridView2.Rows[i].Cells[4].Text;
                meun_money_sum.Text = GridView2.Rows[i].Cells[5].Text;
                meun_name.Attributes.Add("style", "text-align: center;width:400px;");
                //meun_note.Attributes.Add("style", "text-align: center;width:150px;");
                meun_money.Attributes.Add("style", "text-align: center;width:100px");
                meun_number.Attributes.Add("style", "text-align: center;width:100px");
                meun_money_sum.Attributes.Add("style", "text-align: center;width:100px");
                //算錢
                money_all = money_all + Convert.ToInt32(GridView2.Rows[i].Cells[5].Text);
                //把tr塞到td
                datarow.Controls.Add(meun_name);
                //datarow.Controls.Add(meun_note);
                datarow.Controls.Add(meun_money);
                datarow.Controls.Add(meun_number);
                datarow.Controls.Add(meun_money_sum);
                //把tr塞到gridview
                GridView2.Controls[0].Controls.Add(datarow);

                if (i == GridView2.Rows.Count - 1)
                {
                    //產生新<tr>，後面的參數不要問我為什麼，我抄來的
                    money_row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    emp_id_2 = GridView2.Rows[i].Cells[0].Text;
                    //產生新的<td>
                    title2 = new TableCell();
                    //合併<td>
                    title2.ColumnSpan = 6;
                    //塞入文字，檔案類型
                    title2.Text = "合計：" + money_all.ToString();
                    //把td塞到tr
                    money_row.Controls.Add(title2);
                    //加入css樣式
                    title2.Attributes.Add("style", "background-color:#C3E6CB;text-align: center;");
                    //把tr塞到gridview
                    GridView2.Controls[0].Controls.Add(money_row);
                    money_all = 0;
                }
            }
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //隱藏ID欄位
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
            }
        }
        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                sum += Convert.ToInt32(e.Row.Cells[3].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "合計金額：";
                e.Row.Cells[3].Text = sum.ToString();
            }
        }

        public string fn_get_group_store_name()
        {
            string store_name = "";
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @"SELECT [PK]
                                          ,[開團人部門]
                                          ,[開團人]
                                          ,[開團標題]
                                          ,[菜單類型]
                                          ,[店家名稱]
                                          ,[菜單網址]
                                          ,[菜單檔名]
                                          ,[開團備註]
                                          ,[通知部門]
                                          ,[通知同工]
                                          ,[本團總金額]
                                          ,[開團時間]
                                          ,[結單時間]
                                          ,[狀態]
                                          ,[全開放]
                                          ,[結單通知] FROM Group_main_table where PK='" + Request.QueryString["PK"] + "' and 店家名稱<>''";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                store_name = dr["店家名稱"].ToString();
            }
            GroupBuyConn.Close();
            return store_name;
        }
        public void ins_fixed_follow_Group_table(string Group_No, string Group_name, string meun_name, string meun_money, string meun_number, string meun_note)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"INSERT INTO fixed_follow_Group_table (團號,團名,跟團人姓名,跟團者同編,跟團品項,品項單價,品項數量,支付狀態,跟團者部門,備註) VALUES (@團號,@團名,@跟團人姓名,@跟團者同編,@跟團品項,@品項單價,@品項數量,'未支付',@跟團者部門,@備註)";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@團號", Group_No);
            cmd.Parameters.AddWithValue("@團名", Group_name);
            cmd.Parameters.AddWithValue("@跟團人姓名", Session["User_Name"].ToString());
            cmd.Parameters.AddWithValue("@跟團者同編", Session["User_ID"].ToString());
            cmd.Parameters.AddWithValue("@跟團者部門", Session["User_Dep_ID"].ToString());
            cmd.Parameters.AddWithValue("@跟團品項", meun_name.Trim());
            cmd.Parameters.AddWithValue("@品項單價", meun_money);
            cmd.Parameters.AddWithValue("@品項數量", meun_number);
            cmd.Parameters.AddWithValue("@備註", meun_note);
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }

        private int sum = 0;

        //==停用==
        //protected void btn_meun_select_Click(object sender, EventArgs e)
        //{
        //    string store_name = fn_get_group_store_name();
        //    if (txb_meun_select.Text != "")
        //    {
        //        SqlDataSource3.SelectCommand = @"SELECT PK,[店名], [品名], [價格] FROM [follow_meun] where 品名 like '%" + txb_meun_select.Text + "%' and 店名='" + store_name + "' order by 品名";
        //        GV_fixed_meun.DataBind();
        //    }
        //    else
        //    {
        //        SqlDataSource3.SelectCommand = @"SELECT PK,[店名], [品名], [價格] FROM [follow_meun] where 店名='" + store_name + "' order by 品名";
        //        GV_fixed_meun.DataBind();
        //    }
        //}

        //protected void GV_fixed_meun_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes.Add("OnMouseover", "this.style.backgroundColor='#ffff99'");
        //    }

        //    if (e.Row.RowState == DataControlRowState.Alternate)
        //    {
        //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");
        //    }

        //    else if (e.Row.RowType == DataControlRowType.Header)
        //    {
        //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#6B696B'");
        //    }
        //    else
        //    {
        //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#F7F7DE'");
        //    }
        //}

        //public Boolean chk_have_follow()
        //{
        //    bool chk = false;
        //    SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
        //    string sqlstring = @"SELECT [PK],[團號],[團名],[跟團人姓名],[跟團者同編],[跟團品項],[金額] FROM Follow_Group_table where 團號='" + Request.QueryString["PK"] + "' and 跟團者同編 = '" + Session["User_ID"] + "'";
        //    GroupBuyConn.Open();
        //    SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        chk = true;
        //    }
        //    GroupBuyConn.Close();
        //    return chk;
        //}


        //public void ins_Group_Buy_Main(string Group_No, string Group_name)
        //{
        //    SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
        //    GroupBuyConn.Open();
        //    string sqlstring = @"INSERT INTO Follow_Group_table (團號,團名,跟團人姓名,跟團者同編,跟團品項,金額,支付狀態,跟團者部門) VALUES (@團號,@團名,@跟團人姓名,@跟團者同編,@跟團品項,@金額,'未支付',@跟團者部門)";
        //    SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
        //    cmd.Parameters.AddWithValue("@團號", Group_No);
        //    cmd.Parameters.AddWithValue("@團名", Group_name);
        //    cmd.Parameters.AddWithValue("@跟團人姓名", Session["User_Name"].ToString());
        //    cmd.Parameters.AddWithValue("@跟團者同編", Session["User_ID"].ToString());
        //    cmd.Parameters.AddWithValue("@跟團者部門", Session["User_Dep_ID"].ToString());
        //    cmd.Parameters.AddWithValue("@跟團品項", txb_buy_list.Text.Trim());
        //    cmd.Parameters.AddWithValue("@金額", txb_buy_money.Text.Trim());
        //    cmd.ExecuteNonQuery();
        //    GroupBuyConn.Close();
        //}

        //public void load_open_detail()
        //{
        //    SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
        //    string sqlstring = @"SELECT [PK],[開團人部門],[開團人],開團備註,[開團標題],[菜單網址],[菜單檔名],[狀態] FROM Group_main_table where PK='" + Request.QueryString["PK"] + "'";
        //    GroupBuyConn.Open();
        //    SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        Label1.Text = dr["開團標題"].ToString();
        //        txb_Group_name.Text = dr["開團標題"].ToString();
        //        txb_GroupEmp_name.Text = dr["開團人"].ToString();
        //        txb_group_note.Text = dr["開團備註"].ToString();
        //        string filename = dr["菜單檔名"].ToString().Trim();
        //        String savePath = Request.PhysicalApplicationPath + "menu\\";
        //        string file_root = savePath + dr["菜單檔名"].ToString();
        //        if (dr["菜單檔名"].ToString() != "")
        //        {
        //            img_meun.Visible = true;
        //            img_meun.ImageUrl = "~/menu/" + filename;
        //            img_meun.NavigateUrl = "~/menu/" + filename;
        //        }
        //        if (dr["菜單網址"].ToString() != "")
        //        {
        //            meun_addr.NavigateUrl = dr["菜單網址"].ToString();
        //            hyp_store_addr.NavigateUrl = dr["菜單網址"].ToString();
        //        }
        //        else
        //        {
        //            meun_addr.Visible = false;
        //            lab_no_store_note.Visible = true;
        //            hyp_store_addr.Visible=false;
        //        }
        //    }
        //    GroupBuyConn.Close();
        //}



        //protected void btn_follow_Click(object sender, EventArgs e)
        //{
        //    if (txb_buy_list.Text != "" && txb_buy_money.Text != "")
        //    {
        //        bool follow_chk = chk_have_follow();
        //        bool check = true;
        //        if (follow_chk == false)
        //        {
        //            if (!(new System.Text.RegularExpressions.Regex("^[0-9]+$")).IsMatch(txb_buy_money.Text.Trim()))
        //            {
        //                check = false;
        //            }

        //            if (check == true)
        //            {
        //                SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
        //                string sqlstring = @"SELECT [PK],[開團人部門],[開團人],開團備註,[開團標題],[菜單檔名],[狀態] FROM Group_main_table where PK='" + Request.QueryString["PK"] + "'";
        //                GroupBuyConn.Open();
        //                SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
        //                SqlDataReader dr = cmd.ExecuteReader();
        //                if (dr.HasRows)
        //                {
        //                    dr.Read();
        //                    string Group_NO = dr["PK"].ToString().Trim();
        //                    string Group_name = dr["開團標題"].ToString().Trim();
        //                    ins_Group_Buy_Main(Group_NO, Group_name);
        //                    ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('跟團成功!!');window.close();", true);
        //                }
        //                GroupBuyConn.Close();
        //            }

        //            else
        //            {
        //                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('金額請填整數!!');", true);
        //            }
        //        }

        //        else
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('您已經跟團了!!');", true);
        //        }
        //    }


        //    else
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('品項及金額不可為空!!');", true);
        //    }
        //}
    }
}