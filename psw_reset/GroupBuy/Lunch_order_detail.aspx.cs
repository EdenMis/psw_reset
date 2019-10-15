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
using Xceed.Words.NET;

namespace psw_reset.GroupBuy
{
    public partial class Lunch_order_detail : System.Web.UI.Page
    {
        private int sum = 0;
        private int sum_2 = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fn_load_Group_main_table();
            }

            if (Request.Form["__EVENTTARGET"] == "delete_order")
            {
                SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
                GroupBuyConn.Open();
                string sqlstring = @"DELETE FROM lunch_order_table WHERE PK=@PK;";
                SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
                cmd.Parameters.AddWithValue("@PK", Request.Form["__EVENTARGUMENT"]);
                cmd.ExecuteNonQuery();
                GroupBuyConn.Close();
                GridView2.DataBind();
                GridView3.DataBind();
                GridView4.DataBind();
                GridView5.DataBind();
            }

            if (Request.Form["__EVENTTARGET"] == "update_pay_state")
            {
                SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
                GroupBuyConn.Open();
                string sqlstring = @"UPDATE lunch_order_table SET 支付狀態='已支付' WHERE 訂餐日期=@訂餐日期 and 訂餐者同編=@訂餐者同編;";
                SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
                cmd.Parameters.AddWithValue("@訂餐日期", Request.QueryString["order_date"]);
                cmd.Parameters.AddWithValue("@訂餐者同編", Request.Form["__EVENTARGUMENT"]);
                cmd.ExecuteNonQuery();
                GroupBuyConn.Close();
                GridView2.DataBind();
            }
        }

        protected void GridView2_PreRender(object sender, EventArgs e)
        {
            int money_all = 0;
            string emp_id = "";
            string emp_id_2 = "";
            string emp_name = "";
            string emp_dep = "";
            GridViewRow titlerow = null;
            TableCell title = null;
            TableCell title_btn = null;
            TableCell title2 = null;
            GridViewRow money_row = null;
            GridViewRow datarow = null;
            TableCell meun_name = null;
            //TableCell meun_note = null;
            TableCell meun_money = null;
            TableCell meun_number = null;
            TableCell meun_money_sum = null;
            TableCell pay_state = null;
            TableCell delete_btn = null;
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                GridView2.Rows[i].Visible = false;
                //檔案類型!=目前的檔案類型...就產生一個新的標題列
                if (emp_id != "" && emp_id != GridView2.Rows[i].Cells[2].Text)
                {
                    //產生新<tr>，後面的參數不要問我為什麼，我抄來的
                    money_row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    emp_id_2 = GridView2.Rows[i].Cells[2].Text;
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

                if (emp_id != GridView2.Rows[i].Cells[2].Text)
                {
                    //產生新<tr>，後面的參數不要問我為什麼，我抄來的
                    titlerow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    //取得目前的檔案類型
                    emp_dep = GridView2.Rows[i].Cells[1].Text;
                    emp_id = GridView2.Rows[i].Cells[2].Text;
                    emp_name = GridView2.Rows[i].Cells[3].Text;
                    //產生新的<td>
                    title = new TableCell();
                    title_btn = new TableCell();
                    //合併<td>
                    title.ColumnSpan = 5;
                    //塞入文字，檔案類型
                    title.Text = emp_dep + "_" + emp_id + "_" + emp_name;
                    title_btn.Text = "<input id='" + GridView2.Rows[i].Cells[2].Text + "' type='button' value='支付' onclick='update_pay_state(this)'/>";
                    //把td塞到tr
                    titlerow.Controls.Add(title);
                    titlerow.Controls.Add(title_btn);
                    //加入css樣式
                    //title.CssClass = "table-info";
                    title.Attributes.Add("style", "background-color:#B8DAFF;text-align: center;");
                    title_btn.Attributes.Add("style", "background-color:#B8DAFF;text-align: center;");
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
                pay_state = new TableCell();
                delete_btn = new TableCell();
                meun_name.Text = GridView2.Rows[i].Cells[4].Text;
                //meun_note.Text= GridView2.Rows[i].Cells[4].Text;
                meun_money.Text = GridView2.Rows[i].Cells[5].Text;
                meun_number.Text = GridView2.Rows[i].Cells[6].Text;
                meun_money_sum.Text = GridView2.Rows[i].Cells[7].Text;
                pay_state.Text = GridView2.Rows[i].Cells[8].Text;
                delete_btn.Text = "<input id='" + GridView2.Rows[i].Cells[0].Text + "' type='button' value='刪除' onclick='delete_order(this)'/>";
                meun_name.Attributes.Add("style", "text-align: center;width:400px;");
                //meun_note.Attributes.Add("style", "text-align: center;width:250px;");
                meun_money.Attributes.Add("style", "text-align: center;width:100px");
                meun_number.Attributes.Add("style", "text-align: center;width:100px");
                meun_money_sum.Attributes.Add("style", "text-align: center;width:100px");
                delete_btn.Attributes.Add("style", "background-color:#B8DAFF;text-align: center;");
                if (pay_state.Text == "已支付")
                {
                    pay_state.Attributes.Add("style", "text-align: center;width:100px;color:blue");
                }
                if (pay_state.Text == "未支付")
                {
                    pay_state.Attributes.Add("style", "text-align: center;width:100px;color:red");
                }
                //算錢
                money_all = money_all + Convert.ToInt32(GridView2.Rows[i].Cells[7].Text);
                //把tr塞到td
                datarow.Controls.Add(meun_name);
                //datarow.Controls.Add(meun_note);
                datarow.Controls.Add(meun_money);
                datarow.Controls.Add(meun_number);
                datarow.Controls.Add(meun_money_sum);
                datarow.Controls.Add(pay_state);
                datarow.Controls.Add(delete_btn);
                //把tr塞到gridview
                GridView2.Controls[0].Controls.Add(datarow);

                if (i == GridView2.Rows.Count - 1)
                {
                    //產生新<tr>，後面的參數不要問我為什麼，我抄來的
                    money_row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    emp_id_2 = GridView2.Rows[i].Cells[2].Text;
                    //產生新的<td>
                    title2 = new TableCell();
                    //合併<td>
                    title2.ColumnSpan = 7;
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
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
        }
        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                sum += Convert.ToInt32(e.Row.Cells[2].Text);
                sum_2 += Convert.ToInt32(e.Row.Cells[3].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "合計：";
                e.Row.Cells[2].Text = sum.ToString();
                e.Row.Cells[3].Text = sum_2.ToString();
            }
        }
        protected void btn_export_Click(object sender, EventArgs e)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"SELECT [訂餐者同編],[訂餐人姓名],[訂餐者部門],[訂餐品項],[品項單價],[品項數量],[品項單價]*[品項數量] as [總額],[支付狀態] FROM [lunch_order_table] WHERE [訂餐日期] = '" + Request.QueryString["order_date"] + "' order by 訂餐者部門,訂餐者同編";
            SqlDataAdapter oda = new SqlDataAdapter(sqlstring, GroupBuyConn);
            DataTable dt = new DataTable();
            oda.Fill(dt);

            SqlConnection GroupBuyConn2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn2.Open();
            string sqlstring2 = @"SELECT [訂餐品項],[品項單價],sum([品項數量]) as 總數量,[品項單價]*sum([品項數量]) as 總額 FROM [lunch_order_table] WHERE ([訂餐日期] = '" + Request.QueryString["order_date"] + "' ) group by [訂餐品項],[品項單價]";
            SqlDataAdapter oda2 = new SqlDataAdapter(sqlstring2, GroupBuyConn2);
            DataTable dt2 = new DataTable();
            oda2.Fill(dt2);

            //建立Excel 2003檔案
            IWorkbook wb = new HSSFWorkbook();
            ISheet ws;
            ISheet ws2;
            HSSFCellStyle style1 = (HSSFCellStyle)wb.CreateCellStyle();

            ////建立Excel 2007檔案
            //IWorkbook wb = new XSSFWorkbook();
            //ISheet ws;

            if (dt.TableName != string.Empty)
            {
                ws = wb.CreateSheet(dt.TableName);
                ws2 = wb.CreateSheet(dt.TableName);
            }
            else
            {
                ws = wb.CreateSheet("跟團名單");
                ws2 = wb.CreateSheet("各項目訂購數整理");
            }

            //sheet1
            ws.CreateRow(0);//第一行為欄位名稱
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ws.GetRow(0).CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
            }

            int start = 1;
            int end = 0;
            int money = 0;
            int money2 = 0;
            int total = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ws.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j == 6)
                    {
                        money = money + Convert.ToInt32(dt.Rows[i][j]);
                        ws.GetRow(i + 1).CreateCell(j).SetCellValue(Convert.ToInt32(dt.Rows[i][j]));
                    }

                    else
                    {
                        ws.GetRow(i + 1).CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }

                    if (i > 0 && j < 1)
                    {
                        if (dt.Rows[i][j].ToString() == dt.Rows[i - 1][j].ToString())
                        {
                            end++;
                            if (i == dt.Rows.Count - 1)
                            {
                                CellRangeAddress region = new CellRangeAddress(start, start + end, 0, 0);
                                ws.AddMergedRegion(region);
                                CellRangeAddress region2 = new CellRangeAddress(start, start + end, 1, 1);
                                ws.AddMergedRegion(region2);
                                CellRangeAddress region3 = new CellRangeAddress(start, start + end, 2, 2);
                                ws.AddMergedRegion(region3);
                                money = money + Convert.ToInt32(dt.Rows[i][6]);
                                ws.GetRow(i + 1).CreateCell(dt.Columns.Count).SetCellValue("合計");
                                ws.GetRow(i + 1).CreateCell(dt.Columns.Count + 1).SetCellValue(money);
                                start = i + 1;
                                end = 0;
                                money = 0;
                            }
                        }

                        else
                        {
                            CellRangeAddress region = new CellRangeAddress(start, start + end, 0, 0);
                            ws.AddMergedRegion(region);
                            CellRangeAddress region2 = new CellRangeAddress(start, start + end, 1, 1);
                            ws.AddMergedRegion(region2);
                            CellRangeAddress region3 = new CellRangeAddress(start, start + end, 2, 2);
                            ws.AddMergedRegion(region3);
                            ws.GetRow(i).CreateCell(dt.Columns.Count).SetCellValue("合計");
                            ws.GetRow(i).CreateCell(dt.Columns.Count + 1).SetCellValue(money);
                            start = i + 1;
                            end = 0;
                            money = 0;

                            if (i == dt.Rows.Count - 1)
                            {
                                money = money + Convert.ToInt32(dt.Rows[i][6]);
                                ws.GetRow(i + 1).CreateCell(dt.Columns.Count).SetCellValue("合計");
                                ws.GetRow(i + 1).CreateCell(dt.Columns.Count + 1).SetCellValue(money);
                                start = i + 1;
                                end = 0;
                                money = 0;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ws.AutoSizeColumn(i);
            }

            //SHEET2
            ws2.CreateRow(0);//第一行為欄位名稱
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                ws2.GetRow(0).CreateCell(i).SetCellValue(dt2.Columns[i].ColumnName);
            }

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                ws2.CreateRow(i + 1);
                for (int j = 0; j < dt2.Columns.Count; j++)
                {
                    if (j == 2)
                    {
                        total = total + Convert.ToInt32(dt2.Rows[i][j]);
                        ws2.GetRow(i + 1).CreateCell(j).SetCellValue(Convert.ToInt32(dt2.Rows[i][j]));
                    }

                    else if (j == 3)
                    {
                        money2 = money2 + Convert.ToInt32(dt2.Rows[i][j]);
                        ws2.GetRow(i + 1).CreateCell(j).SetCellValue(Convert.ToInt32(dt2.Rows[i][j]));
                    }

                    else
                    {
                        ws2.GetRow(i + 1).CreateCell(j).SetCellValue(dt2.Rows[i][j].ToString());
                    }
                }
                if (i == dt2.Rows.Count - 1)
                {
                    ws2.CreateRow(i + 2);
                    ws2.GetRow(i + 2).CreateCell(dt2.Columns.Count - 3).SetCellValue("合計");
                    ws2.GetRow(i + 2).CreateCell(dt2.Columns.Count - 2).SetCellValue(total);
                    ws2.GetRow(i + 2).CreateCell(dt2.Columns.Count - 1).SetCellValue(money2);
                }
            }

            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                ws2.AutoSizeColumn(i);
            }
            MemoryStream MS = new MemoryStream();
            wb.Write(MS);
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=\"" + HttpUtility.UrlEncode("跟團者名單" + DateTime.Now, System.Text.Encoding.UTF8) + ".xls");
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            System.Web.HttpContext.Current.Response.BinaryWrite(MS.ToArray());
            wb = null;
            MS.Close();
            MS.Dispose();
            GroupBuyConn.Close();
            GroupBuyConn2.Close();
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
            //FileStream file= new FileStream(@"E:\npoi.xls", FileMode.Create);//產生檔案
            //wb.Write(file);
            //file.Close();
        }
        public void fn_Update_Lunch_set_main_state(string state)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"UPDATE Lunch_set_main SET 是否可訂=@是否可訂 WHERE 日期='" + Request.QueryString["order_date"] + "';";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@是否可訂", state);
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }
        public static string SendMail(string title, string address, string txt)
        {
            /*
              result      訊息
              mailserver  mailserver 位置
              emailFrom   寄信人帳號(隨便設定，格式要對)      
              password    寄信人密碼(隨便設定)
              emailTo     收信人      
              subject     信件主旨
              body        信件內容
            */

            string result = "信件發送成功";
            string mailserver = "192.168.1.10";
            string emailFrom = "edenSYSADMIN@eden.org.tw";
            string password = "SYSADMIN";
            string emailTo = address;
            string subject = title;
            string body = txt;

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    // 若內容是HTML格式，則為True
                    mail.IsBodyHtml = true;


                    using (SmtpClient smtp = new SmtpClient(mailserver))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }
        protected void btn_group_success_Click1(object sender, EventArgs e)
        {
            string mail_addr = "";
            int i;
            for (i = 0; i < GridView4.Rows.Count; i++)
            {
                string emp_no = GridView4.Rows[i].Cells[0].Text.ToString();
                SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                HrsConn.Open();
                string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + emp_no + "'";
                SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        if (mail_addr == "")
                        {
                            mail_addr = dr2["EMPLOYEE_EMAIL_1"].ToString();
                        }
                        else
                        {
                            mail_addr = mail_addr + "," + dr2["EMPLOYEE_EMAIL_1"].ToString();
                        }
                    }
                    HrsConn.Close();
                }
            }
            string today = Request.QueryString["order_date"];
            string emp_name = Session["User_Name"].ToString();
            string mail_title = "本日「" + today + "」之午餐訂餐已成團結單，請有訂餐的同工到總務進行付款作業";
            string mail_txt = txb_mail_txt.Text.Trim();

            //Response.Write(mail_addr);
            fn_Update_Lunch_set_main_state("0");
            SendMail(mail_title, mail_addr, mail_txt);
            string script = "window.parent.opener.location.href='../GroupBuy/Lunch_order_list.aspx';";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "reload", script, true);

            string script2 = "window.close();";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "close", script2, true);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            fn_Update_Lunch_set_main_state("1");
            ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('本日午餐訂餐已再度開放!!');", true);
        }
        public void fn_load_Group_main_table()
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @"SELECT[PK]
                                                  ,[店名]
                                                  ,[地址]
                                                  ,[電話]
                                                  ,[備註]
                                                  ,[good]
                                                  ,[ordinary]
                                                  ,[bad] FROM Lunch_Store_main 
                                                   where PK='" + Request.QueryString["store_id"] + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                lab_store_name.Text = dr["店名"].ToString();
                lab_store_addr.Text = dr["地址"].ToString();
                lab_store_tel.Text = dr["電話"].ToString();
                txb_group_store_note.Text = dr["備註"].ToString();
                lab_good.Text = dr["good"].ToString();
                lab_ordinary.Text = dr["ordinary"].ToString();
                lab_bad.Text = dr["bad"].ToString();
            }
            GroupBuyConn.Close();
        }
        protected void btn_show_meun_Click(object sender, EventArgs e)
        {
            if (div_meun.Visible == true)
            {
                div_meun.Visible = false;
            }

            else
            {
                div_meun.Visible = true;
            }
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
            bool chk = true;
            string order_date = DateTime.Now.ToString("yyyy-MM-dd");
            string store_name = lab_store_name.Text.Trim();
            string order_emp_name = "";
            string order_emp_id = "";
            string order_emp_dep = "";
            if (txb_emp_id.Text != "")
            {
                SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                string sqlstring = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + txb_emp_id.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlstring, HrsConn);
                HrsConn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    order_emp_id = dr["EMPLOYEE_NO"].ToString();
                    order_emp_name = dr["EMPLOYEE_CNAME"].ToString();
                    order_emp_dep = dr["DEPARTMENT_CODE"].ToString();
                    cmd.Cancel();
                    dr.Close();
                    HrsConn.Close();
                    HrsConn.Dispose();
                }
                else
                {
                    string script = "alert('查無此同工，請確認')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                    chk = false;
                    HrsConn.Close();
                }
            }

            else
            {
                string script = "alert('同工編號不可為空!!')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                chk = false;
            }

            for (int i = 0; i < GV_lunch_meun.Rows.Count; i++)
            {
                TextBox tb = (TextBox)GV_lunch_meun.Rows[i].FindControl("txb_buy_num");
                string meun_number = tb.Text.Trim();
                if (!(new System.Text.RegularExpressions.Regex("^[0-9]+$")).IsMatch(meun_number))
                {
                    chk = false;
                }
            }

            if (chk == true)
            {
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
                            ins_lunch_order_table(order_date, store_name, order_emp_name, order_emp_id, order_emp_dep, meun_name, meun_money, meun_number, meun_note);
                            ins_lunch_Voting_rights(order_date);
                        }
                        GridView2.DataBind();
                        GridView3.DataBind();
                        GridView4.DataBind();
                        GridView5.DataBind();
                        txb_emp_id.Text = "";
                    }
                }
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('已幫同工「"+order_emp_id+"」訂餐成功!!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('下單量請填整數!!');", true);
            }
        }

        public void ins_lunch_order_table(string order_date, string store_name,string order_emp_name, string order_emp_id, string order_emp_dep, string meun_name, int meun_money, int meun_number, string meun_note)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"INSERT INTO lunch_order_table 
(訂餐日期,午餐店家,訂餐人姓名,訂餐者同編,訂餐品項,品項單價,品項數量,支付狀態,訂餐者部門,備註) 
VALUES (@訂餐日期,@午餐店家,@訂餐人姓名,@訂餐者同編,@訂餐品項,@品項單價,@品項數量,'未支付',@訂餐者部門,@備註)";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@訂餐日期", order_date);
            cmd.Parameters.AddWithValue("@午餐店家", store_name);
            cmd.Parameters.AddWithValue("@訂餐人姓名", order_emp_name);
            cmd.Parameters.AddWithValue("@訂餐者同編", order_emp_id);
            cmd.Parameters.AddWithValue("@訂餐者部門", order_emp_dep);
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

        protected void btn_export2_Click(object sender, EventArgs e)
        {
            int tag_strat = Convert.ToInt32(Dlist_tag_start.Text.Trim())-1;
            int i = 0;
            int j = 0;
            int k = 0;
            string tag = "";
            string word = "";
            string templatePath = Request.PhysicalApplicationPath + "NPOI\\tag33_99.docx";

            // 讀入該範本檔
            DocX document = DocX.Load(templatePath);

            for (k = 0; k < tag_strat; k++)
            {
                tag = "#A" + k.ToString() + "#";
                document.ReplaceText(tag, "");
            }

            for (i = 0; i < GridView5.Rows.Count; i++)
            {
                word = GridView5.Rows[i].Cells[3].Text.ToString() + "–價格：" + GridView5.Rows[i].Cells[4].Text.ToString()+ "\n(" + GridView5.Rows[i].Cells[1].Text.ToString() + "–" + GridView5.Rows[i].Cells[2].Text.ToString() + "–" + GridView5.Rows[i].Cells[0].Text.ToString()+")";
                tag = "#A" + (i+tag_strat).ToString() + "#";
                document.ReplaceText(tag, word);
            }

            for (j = (i + tag_strat); j < 99; j++)
            {
                tag = "#A" + j.ToString() + "#";
                document.ReplaceText(tag, "");
            }
            // 指定成品儲存路徑及檔名並執行儲存
            string savePath = Request.PhysicalApplicationPath + "NPOI\\";
            string fileName = string.Format("標籤檔.docx", DateTime.Now.ToString("yyyyMMddHHmmss"));
            document.SaveAs(savePath + fileName);
            FileInfo DownloadFile = new FileInfo(savePath + fileName);
            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = false;
            //Response.ContentType指定檔案類型 
            //可以為application/ms-excel || application/ms-word || application/ms-txt 
            //application/ms-html || 或其他瀏覽器可以支援的文件
            Response.ContentType = "application/octet-stream";//所有類型
                                                              //下面這行很重要， attachment 参数表示作為附件下載，可以改成 online線上開啟
                                                              //filename=FileFlow.xls 指定输出檔案名稱，注意其附檔名和指定檔案類型相符，
                                                              //可以為：.doc || .xls || .txt ||.htm
            Response.AppendHeader("Content-Disposition", "attachment;filename=" +
            HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());//取得檔案大小

            Response.WriteFile(DownloadFile.FullName);
            Response.Flush();
            Response.End();
        }
    }
}