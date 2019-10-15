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

namespace psw_reset.GroupBuy
{
    public partial class My_Open_Group_detail : System.Web.UI.Page
    {
        private int sum = 0;
        private int sum_2 = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string store_name = fn_get_group_store_name();
                fn_load_Group_store_table(store_name);
                fn_load_Group_main_table();
                bool chk_end = chk_group_end();

                if (chk_end == false)
                {
                    txb_group_name.Enabled = false;
                    txb_group_name.BackColor = System.Drawing.Color.Gainsboro;
                    txb_emp_Notice.Enabled = false;
                    txb_emp_Notice.BackColor = System.Drawing.Color.Gainsboro;
                    txb_dep_Notice.Enabled = false;
                    txb_dep_Notice.BackColor = System.Drawing.Color.Gainsboro;
                    txb_note.Enabled = false;
                    txb_note.BackColor = System.Drawing.Color.Gainsboro;
                    txb_mail_txt.Enabled = false;
                    txb_mail_txt.BackColor = System.Drawing.Color.Gainsboro;
                    txb_end_time.Enabled = false;
                    txb_end_time.BackColor = System.Drawing.Color.Gainsboro;
                    rdo_dep_emp.Enabled = false;
                    rdo_all.Enabled = false;

                    btn_group_lost.Visible = false;
                    btn_group_success.Visible = false;
                    btn_update_buy_info.Visible = false;
                    btn_update_buy_info_2.Visible = false;
                }
            }

            if (Request.Form["__EVENTTARGET"] == "delete_order")
            {
                SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
                GroupBuyConn.Open();
                string sqlstring = @"DELETE FROM fixed_follow_Group_table WHERE PK=@PK;";
                SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
                cmd.Parameters.AddWithValue("@PK", Request.Form["__EVENTARGUMENT"]);
                cmd.ExecuteNonQuery();
                GroupBuyConn.Close();
                GridView2.DataBind();
                GridView3.DataBind();
                GridView4.DataBind();
            }

            if (Request.Form["__EVENTTARGET"] == "update_pay_state")
            {
                SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
                GroupBuyConn.Open();
                string sqlstring = @"UPDATE fixed_follow_Group_table SET 支付狀態='已支付' WHERE 團號=@團號 and 跟團者同編=@跟團者同編;";
                SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
                cmd.Parameters.AddWithValue("@團號", Request.QueryString["PK"]);
                cmd.Parameters.AddWithValue("@跟團者同編", Request.Form["__EVENTARGUMENT"]);
                cmd.ExecuteNonQuery();
                GroupBuyConn.Close();
                GridView2.DataBind();    
            }
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

        protected void btn_update_buy_info_Click(object sender, EventArgs e)
        {
            if (txb_group_name.Text != "")
            {
                if (rdo_dep_emp.Checked == true && txb_dep_Notice.Text == "" && txb_emp_Notice.Text == "")
                {
                    string script2 = "alert('若讀取權限為僅被通知部門與同工，則通知部門或通知同工必填')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
                }

                else
                {
                    Boolean check = true;
                    Boolean check2 = true;
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
                                check = false;
                            }
                            HrsConn.Close();
                        }

                        if (check == false)
                        {
                            string script2 = "alert('查無通知部門，請確認格式或部門編號正確')";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
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
                                check2 = false;
                            }
                            HrsConn.Close();
                        }

                        if (check2 == false)
                        {
                            string script2 = "alert('查無通知同工，請確認格式或同工編號正確')";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
                        }
                    }

                    if (check == true && check2 == true)
                    {
                        fn_Update_Group_main_table();
                        string script = "alert('資訊修改完成!!')";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                    }
                }
            }

            else
            {
                string script = "alert('請輸入開團標題')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
            }
        }

        protected void btn_update_buy_info_2_Click(object sender, EventArgs e)
        {
            String savePath = Request.PhysicalApplicationPath + "NPOI\\";
            string emp_name_dep = "";
            string mail_title_dep = "";
            string mail_txt_dep = "";
            string mail_addr_dep = "";
            string emp_name_emp = "";
            string mail_title_emp = "";
            string mail_txt_emp = "";
            string mail_addr_emp = "";

            if (txb_group_name.Text != "")
            {
                try
                {
                    if (rdo_dep_emp.Checked == true && txb_dep_Notice.Text == "" && txb_emp_Notice.Text == "")
                    {
                        string script2 = "alert('若讀取權限為僅被通知部門與同工，則通知部門或通知同工必填')";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
                    }

                    else
                    {
                        Boolean check = true;
                        Boolean check2 = true;
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
                                    check = false;
                                }
                                HrsConn.Close();
                            }

                            if (check == true)
                            {
                                foreach (var substring in substrings)
                                {
                                    SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                                    HrsConn.Open();
                                    string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where DEPARTMENT_CODE = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'  and EMPLOYEE_NO<>'1551'";
                                    SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
                                    SqlDataReader dr2 = cmd2.ExecuteReader();
                                    if (dr2.HasRows)
                                    {
                                        while (dr2.Read())
                                        {
                                            if (mail_addr_dep == "")
                                            {
                                                mail_addr_dep = dr2["EMPLOYEE_EMAIL_1"].ToString();
                                            }
                                            else
                                            {
                                                mail_addr_dep = mail_addr_dep + "," + dr2["EMPLOYEE_EMAIL_1"].ToString();
                                            }
                                        }
                                    }
                                    HrsConn.Close();
                                }
                                emp_name_dep = Session["User_Name"].ToString();
                                mail_title_dep = txb_group_name.Text.Trim() + "開團資訊有進行變更，請確認";
                                mail_txt_dep = "團名：" + mail_title_dep + "<br/>開團人：" + emp_name_dep + "<br/>開團資訊變更，請進系統確認!!<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統登入頁面，帳號密碼為EIP密碼喔!!</a>";
                                //Response.Write(mail_addr);           
                            }

                            else
                            {
                                string script2 = "alert('查無通知部門，請確認格式或部門編號正確')";
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
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
                                string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "'";
                                SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
                                SqlDataReader dr2 = cmd2.ExecuteReader();
                                if (!dr2.HasRows)
                                {
                                    check2 = false;
                                }
                                HrsConn.Close();
                            }

                            if (check2 == true)
                            {
                                foreach (var substring in substrings)
                                {
                                    SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
                                    HrsConn.Open();
                                    string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "'";
                                    SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
                                    SqlDataReader dr2 = cmd2.ExecuteReader();
                                    if (dr2.HasRows)
                                    {
                                        while (dr2.Read())
                                        {
                                            if (mail_addr_emp == "")
                                            {
                                                mail_addr_emp = dr2["EMPLOYEE_EMAIL_1"].ToString();
                                            }
                                            else
                                            {
                                                mail_addr_emp = mail_addr_emp + "," + dr2["EMPLOYEE_EMAIL_1"].ToString();
                                            }
                                        }
                                        HrsConn.Close();
                                    }
                                }
                                emp_name_emp = Session["User_Name"].ToString();
                                mail_title_emp = txb_group_name.Text.Trim() + "開團資訊有進行變更，請確認";
                                mail_txt_emp = "團名：" + mail_title_emp + "<br/>開團人：" + emp_name_emp + "<br/>開團資訊變更，請進系統確認!!<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統登入頁面，帳號密碼為EIP密碼喔!!</a>";
                                //Response.Write(mail_addr);
                            }
                            else
                            {
                                string script2 = "alert('查無通知同工，請確認格式或同工編號正確')";
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
                            }
                        }

                        if (check == true && check2 == true)
                        {
                            fn_Update_Group_main_table();
                            SendMail(mail_title_dep, mail_addr_dep, mail_txt_dep);
                            SendMail(mail_title_emp, mail_addr_emp, mail_txt_emp);

                            string script = "alert('資訊修改完成，並重新發信通知!!')";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                        }
                    }
                }

                catch (Exception ex)
                {
                    Response.Write(ex);
                }
            }

            else
            {
                string script = "alert('請輸入開團標題')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
            }
        }

        protected void btn_group_success_Click(object sender, EventArgs e)
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
            string emp_name = Session["User_Name"].ToString();
            string mail_title = "恭喜，" + txb_group_name.Text.Trim() + "已成團結單了";
            string mail_txt = txb_mail_txt.Text.Trim();

            //Response.Write(mail_addr);
            fn_Update_Group_main_table_state("成團結單");
            SendMail(mail_title, mail_addr, mail_txt);
            string script = "window.parent.opener.location.href='../GroupBuy/My_Open_Group.aspx';";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "reload", script, true);

            string script2 = "window.close();";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "close", script2, true);
        }
        //流團
        protected void btn_group_lost_Click(object sender, EventArgs e)
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

            string emp_name = Session["User_Name"].ToString();
            string mail_title = "很遺憾，" + txb_group_name.Text.Trim() + "已流團。";
            string mail_txt = txb_mail_txt.Text.Trim();

            //Response.Write(mail_addr);
            fn_Update_Group_main_table_state("流團");
            SendMail(mail_title, mail_addr, mail_txt);
            string script = "window.parent.opener.location.href='../GroupBuy/My_Open_Group.aspx';";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "reload", script, true);

            string script2 = "window.close();";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "close", script2, true);
        }


        protected void Button1_Click1(object sender, EventArgs e)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"SELECT [跟團者同編],[跟團人姓名],[跟團品項],[品項單價],[品項數量],[品項單價]*[品項數量] as [總額],[支付狀態] FROM [fixed_follow_Group_table] WHERE [團號] = '" + Request.QueryString["PK"] + "' order by 跟團者同編";
            SqlDataAdapter oda = new SqlDataAdapter(sqlstring, GroupBuyConn);
            DataTable dt = new DataTable();
            oda.Fill(dt);

            SqlConnection GroupBuyConn2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn2.Open();
            string sqlstring2 = @"SELECT [跟團品項],[品項單價],sum([品項數量]) as 總數量,[品項單價]*sum([品項數量]) as 總額 FROM [fixed_follow_Group_table] WHERE ([團號] = '" + Request.QueryString["PK"] + "' ) group by [跟團品項],[品項單價],備註";
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
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ws.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j == 5)
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
                                money = money + Convert.ToInt32(dt.Rows[i][5]);
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
                            ws.GetRow(i).CreateCell(dt.Columns.Count).SetCellValue("合計");
                            ws.GetRow(i).CreateCell(dt.Columns.Count + 1).SetCellValue(money);
                            start = i + 1;
                            end = 0;
                            money = 0;
                            if (i == dt.Rows.Count - 1)
                            {
                                money = money + Convert.ToInt32(dt.Rows[i][5]);
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
                    if (j == 3)
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
                    ws2.GetRow(i + 2).CreateCell(dt2.Columns.Count - 2).SetCellValue("合計");
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

        protected void GridView2_PreRender(object sender, EventArgs e)
        {
            int money_all = 0;
            string emp_id = "";
            string emp_id_2 = "";
            string emp_name = "";
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
                if (emp_id != "" && emp_id != GridView2.Rows[i].Cells[1].Text)
                {
                    //產生新<tr>，後面的參數不要問我為什麼，我抄來的
                    money_row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    emp_id_2 = GridView2.Rows[i].Cells[1].Text;
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

                if (emp_id != GridView2.Rows[i].Cells[1].Text)
                {
                    //產生新<tr>，後面的參數不要問我為什麼，我抄來的
                    titlerow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    //取得目前的檔案類型
                    emp_id = GridView2.Rows[i].Cells[1].Text;
                    emp_name = GridView2.Rows[i].Cells[2].Text;
                    //產生新的<td>
                    title = new TableCell();
                    title_btn = new TableCell();
                    //合併<td>
                    title.ColumnSpan = 5;
                    //塞入文字，檔案類型
                    title.Text = emp_id + "_" + emp_name;
                    title_btn.Text = "<input id='" + GridView2.Rows[i].Cells[1].Text + "' type='button' value='支付' onclick='update_pay_state(this)'/>";
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
                meun_name.Text = GridView2.Rows[i].Cells[3].Text;
                //meun_note.Text= GridView2.Rows[i].Cells[4].Text;
                meun_money.Text = GridView2.Rows[i].Cells[4].Text;
                meun_number.Text = GridView2.Rows[i].Cells[5].Text;
                meun_money_sum.Text = GridView2.Rows[i].Cells[6].Text;
                pay_state.Text = GridView2.Rows[i].Cells[7].Text;
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
                money_all = money_all + Convert.ToInt32(GridView2.Rows[i].Cells[6].Text);
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
                    emp_id_2 = GridView2.Rows[i].Cells[1].Text;
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
            }
        }
        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                sum_2 += Convert.ToInt32(e.Row.Cells[3].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "合計金額：";
                e.Row.Cells[3].Text = sum_2.ToString();
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
                                          ,[結單通知] FROM Group_main_table 
                                           where PK='" + Request.QueryString["PK"] + "' and 店家名稱<>''";
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
        public bool chk_group_end()
        {
            bool chk = false;
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @"SELECT [PK]
                                                  ,[開團人部門]
                                                  ,[開團人]
                                                  ,[開團標題]
                                                  ,[菜單檔名]
                                                  ,[開團備註]
                                                  ,[通知部門]
                                                  ,[通知同工]
                                                  ,[本團總金額]
                                                  ,[開團時間]
                                                  ,[結單時間]
                                                  ,[狀態]
                                                  ,[全開放] FROM Group_main_table where PK='" + Request.QueryString["PK"] + "' and 狀態='開團中'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                chk = true;
            }
            return chk;
        }
        public void fn_load_Group_main_table()
        {
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
                                                  ,[結單通知] FROM Group_main_table 
                                                   where PK='" + Request.QueryString["PK"] + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                txb_group_name.Text = dr["開團標題"].ToString();
                if (dr["全開放"].ToString() == "True")
                {
                    rdo_all.Checked = true;
                }
                txb_dep_Notice.Text= dr["通知部門"].ToString();
                txb_emp_Notice.Text= dr["通知同工"].ToString();
                txb_end_time.Text = Convert.ToDateTime(dr["結單時間"]).ToString("yyyy/MM/dd HH:mm:ss");
                txb_note.Text = dr["開團備註"].ToString();
            }
            GroupBuyConn.Close();
        }
        public void fn_load_Group_store_table(string store_name)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @"SELECT [PK]
                                              ,[店名]
                                              ,[類型]
                                              ,[連絡電話]
                                              ,[店家地址]
                                              ,[店家備註]
                                              ,[可使用同工]
                                              ,[可使用部門]
                                              ,[最後更新時間]
                                              ,[最後修改人]
                                              ,[全會可用]
                                              ,[啟用狀態]
                                              ,[店家網址],菜單檔名
                                          FROM [Group_store_table]
                                          WHERE 店名='" + store_name+"'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                lab_store_name.Text = dr["店名"].ToString();
                lab_store_tel.Text = dr["連絡電話"].ToString();
                lab_store_addr.Text = dr["店家地址"].ToString();
                txb_group_store_note.Text = dr["店家備註"].ToString();
                if (dr["店家網址"].ToString() != "")
                {
                    hyp_store_web_addr.Visible = true;
                    hyp_store_web_addr.Text = "店家連結網址";
                    hyp_store_web_addr.NavigateUrl = dr["店家網址"].ToString();
                }

                if (dr["菜單檔名"].ToString() != "")
                {
                    img_meun.Visible = true;
                    img_meun.ImageUrl = "~/menu/" + dr["菜單檔名"].ToString();
                    img_meun.NavigateUrl = "~/menu/" + dr["菜單檔名"].ToString();
                }
            }
        }
        public void fn_Update_Group_main_table()
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"UPDATE Group_main_table SET 開團標題=@開團標題,通知部門=@通知部門,通知同工=@通知同工,全開放=@全開放,開團備註=@開團備註,結單時間=@結單時間  WHERE PK='" + Request.QueryString["PK"] + "';";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@開團標題", txb_group_name.Text.Trim());
            cmd.Parameters.AddWithValue("@通知部門", txb_dep_Notice.Text.Trim());
            cmd.Parameters.AddWithValue("@通知同工", txb_emp_Notice.Text.Trim());
            bool chk_all = false;
            if (rdo_all.Checked == true)
            {
                chk_all = true;
            }
            cmd.Parameters.AddWithValue("@全開放", chk_all.ToString());
            cmd.Parameters.AddWithValue("@開團備註", txb_note.Text.Trim());
            cmd.Parameters.AddWithValue("@結單時間", txb_end_time.Text);
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }
        public void fn_Update_Group_main_table_state(string state)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"UPDATE Group_main_table SET 狀態=@狀態 WHERE PK='" + Request.QueryString["PK"] + "';";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@狀態", state);
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
        //==停用==
        //public void delete_follow_meun(string meun_id)
        //{
        //    SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
        //    GroupBuyConn.Open();
        //    string sqlstring = @"delete from follow_meun where 菜單ID=@菜單ID";
        //    SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
        //    cmd.Parameters.AddWithValue("@菜單ID", meun_id);
        //    cmd.ExecuteNonQuery();
        //    GroupBuyConn.Close();
        //}
        //public void ins_follow_meun(string savePath, string meun_id)
        //{
        //    SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
        //    Conn.Open();
        //    String fileName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + FileUpload2.FileName;
        //    savePath = savePath + fileName;
        //    FileUpload2.SaveAs(savePath);

        //    XSSFWorkbook workbook = new XSSFWorkbook(FileUpload2.FileContent);
        //    XSSFSheet u_sheet = (XSSFSheet)workbook.GetSheetAt(0);

        //    int i = 0;
        //    for (i = u_sheet.FirstRowNum + 1; i <= u_sheet.LastRowNum; i++)
        //    {
        //        XSSFRow row = (XSSFRow)u_sheet.GetRow(i);
        //        string sqlstring4 = @"INSERT INTO follow_meun ([菜單ID]
        //                                          ,[品名]
        //                                          ,[價格]) 
        //                                          VALUES (
        //                                          @0
        //                                          ,@1
        //                                          ,@2)";
        //        SqlCommand cmd = new SqlCommand(sqlstring4, Conn);
        //        cmd.Parameters.AddWithValue("@0", meun_id);
        //        cmd.Parameters.AddWithValue("@1", row.GetCell(0).ToString());
        //        cmd.Parameters.AddWithValue("@2", row.GetCell(1).ToString());
        //        cmd.ExecuteNonQuery();
        //        cmd.Cancel();
        //    }
        //    workbook = null;
        //    u_sheet = null;
        //    Conn.Close();
        //}

        //==停用==
        //匯出-開放式菜單
        //protected void btn_export_Click1(object sender, EventArgs e)
        //{
        //    SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
        //    GroupBuyConn.Open();
        //    string sqlstring = @"SELECT 跟團者部門,[跟團人姓名], [跟團者同編], [跟團品項], [金額] FROM [Follow_Group_table] WHERE ([團號] = '" + Request.QueryString["PK"] + "') order by 跟團者部門,跟團者同編";
        //    //SqlCommand cmd = new SqlCommand(sqlstring, PCLeaseConn);
        //    SqlDataAdapter oda = new SqlDataAdapter(sqlstring, GroupBuyConn);
        //    DataTable dt = new DataTable();
        //    oda.Fill(dt);

        //    //建立Excel 2003檔案
        //    IWorkbook wb = new HSSFWorkbook();
        //    ISheet ws;

        //    ////建立Excel 2007檔案
        //    //IWorkbook wb = new XSSFWorkbook();
        //    //ISheet ws;

        //    if (dt.TableName != string.Empty)
        //    {
        //        ws = wb.CreateSheet(dt.TableName);
        //    }
        //    else
        //    {
        //        ws = wb.CreateSheet("Sheet1");
        //    }

        //    ws.CreateRow(0);//第一行為欄位名稱
        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        ws.GetRow(0).CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
        //    }

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        ws.CreateRow(i + 1);
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            if (j == 4)
        //            {
        //                ws.GetRow(i + 1).CreateCell(j).SetCellValue(Convert.ToInt32(dt.Rows[i][j]));
        //            }

        //            else
        //            {
        //                ws.GetRow(i + 1).CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
        //            }
        //        }
        //    }

        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        ws.AutoSizeColumn(i);
        //    }

        //    MemoryStream MS = new MemoryStream();
        //    wb.Write(MS);
        //    System.Web.HttpContext.Current.Response.Clear();
        //    System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=\"" + HttpUtility.UrlEncode("跟團者名單" + DateTime.Now, System.Text.Encoding.UTF8) + ".xls");
        //    System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    System.Web.HttpContext.Current.Response.BinaryWrite(MS.ToArray());
        //    wb = null;
        //    MS.Close();
        //    MS.Dispose();
        //    GroupBuyConn.Close();
        //    System.Web.HttpContext.Current.Response.Flush();
        //    System.Web.HttpContext.Current.Response.End();
        //    //FileStream file= new FileStream(@"E:\npoi.xls", FileMode.Create);//產生檔案
        //    //wb.Write(file);
        //    //file.Close();
        //}
        //匯出-固定式菜單
        //支付未支付-開放式菜單
        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    int i;
        //    for (i = 0; i < GridView1.Rows.Count; i++)
        //    {
        //        if (GridView1.Rows[i].Cells[4].Text == "未支付")
        //        {
        //            GridView1.Rows[i].Cells[4].ForeColor = System.Drawing.Color.Red;
        //            GridView1.Rows[i].FindControl("btn_pay_money").Visible = true;
        //        }

        //        if (GridView1.Rows[i].Cells[4].Text == "已支付")
        //        {
        //            GridView1.Rows[i].Cells[4].ForeColor = System.Drawing.Color.Blue;
        //            GridView1.Rows[i].FindControl("btn_pay_money").Visible = false;
        //        }
        //    }

        //    if (e.Row.RowIndex >= 0)
        //    {
        //        sum += Convert.ToInt32(e.Row.Cells[3].Text);
        //    }
        //    else if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        e.Row.Cells[2].Text = "合計總金額：";
        //        e.Row.Cells[3].Text = sum.ToString();
        //    }
        //}
        //修改並重發通知
        //protected void btn_update_buy_info_Click(object sender, EventArgs e)
        //{
        //    String savePath = Request.PhysicalApplicationPath + "menu\\";
        //    string emp_name_dep = "";
        //    string mail_title_dep = "";
        //    string mail_txt_dep = "";
        //    string mail_addr_dep = "";
        //    string emp_name_emp = "";
        //    string mail_title_emp = "";
        //    string mail_txt_emp = "";
        //    string mail_addr_emp = "";

        //    //建立資料夾
        //    if (!Directory.Exists(savePath))
        //    {
        //        Directory.CreateDirectory(savePath);
        //    }

        //    if (txb_group_name.Text != "")
        //    {
        //        if (FileUpload1.HasFile)
        //        {
        //            string year = DateTime.Now.Year.ToString();
        //            string month = DateTime.Now.Month.ToString();
        //            string day = DateTime.Now.Day.ToString();
        //            string hh = DateTime.Now.Hour.ToString();
        //            string mm = DateTime.Now.Minute.ToString();
        //            string ss = DateTime.Now.Second.ToString();

        //            string filename = year + month + day + hh + mm + ss + FileUpload1.FileName;
        //            savePath = savePath + filename;
        //            FileUpload1.SaveAs(savePath);
        //            if (rdo_dep_emp.Checked == true && txb_dep_Notice.Text == "" && txb_emp_Notice.Text == "")
        //            {
        //                string script2 = "alert('若讀取權限為僅被通知部門與同工，則通知部門或通知同工必填')";
        //                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
        //            }

        //            else
        //            {
        //                Boolean check = true;
        //                Boolean check2 = true;
        //                if (txb_dep_Notice.Text != "")
        //                {
        //                    String word = txb_dep_Notice.Text;
        //                    String[] substrings = word.Split();

        //                    foreach (var substring in substrings)
        //                    {
        //                        SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                        HrsConn.Open();
        //                        string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where DEPARTMENT_CODE = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'  and EMPLOYEE_NO<>'1551'";
        //                        SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                        SqlDataReader dr2 = cmd2.ExecuteReader();
        //                        if (!dr2.HasRows)
        //                        {
        //                            check = false;
        //                        }
        //                        HrsConn.Close();
        //                    }

        //                    if (check == true)
        //                    {
        //                        foreach (var substring in substrings)
        //                        {
        //                            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                            HrsConn.Open();
        //                            string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where DEPARTMENT_CODE = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1' and EMPLOYEE_NO<>'1551'";
        //                            SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                            SqlDataReader dr2 = cmd2.ExecuteReader();
        //                            if (dr2.HasRows)
        //                            {
        //                                while (dr2.Read())
        //                                {
        //                                    if (dr2["EMPLOYEE_EMAIL_1"].ToString() != "")
        //                                    {
        //                                        if (mail_addr_dep == "")
        //                                        {
        //                                            mail_addr_dep = dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                        else
        //                                        {
        //                                            mail_addr_dep = mail_addr_dep + "," + dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            HrsConn.Close();
        //                        }
        //                        emp_name_dep = Session["User_Name"].ToString();
        //                        mail_title_dep = txb_group_name.Text.Trim() + "開團資訊有進行變更，請確認";
        //                        mail_txt_dep = "團名：" + mail_title_dep + "<br/>開團人：" + emp_name_dep + "<br/>開團資訊變更，請進系統確認!!<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統登入頁面，帳號密碼為EIP密碼喔!!</a>";
        //                        //Response.Write(mail_addr);
        //                    }

        //                    else
        //                    {
        //                        string script2 = "alert('查無通知部門，請確認格式或部門編號正確')";
        //                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
        //                    }
        //                }

        //                if (txb_emp_Notice.Text != "")
        //                {
        //                    String word = txb_emp_Notice.Text;
        //                    String[] substrings = word.Split();
        //                    foreach (var substring in substrings)
        //                    {
        //                        SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                        HrsConn.Open();
        //                        string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'";
        //                        SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                        SqlDataReader dr2 = cmd2.ExecuteReader();
        //                        if (!dr2.HasRows)
        //                        {
        //                            check2 = false;
        //                        }
        //                        HrsConn.Close();
        //                    }

        //                    if (check2 == true)
        //                    {
        //                        foreach (var substring in substrings)
        //                        {
        //                            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                            HrsConn.Open();
        //                            string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'";
        //                            SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                            SqlDataReader dr2 = cmd2.ExecuteReader();
        //                            if (dr2.HasRows)
        //                            {

        //                                while (dr2.Read())
        //                                {
        //                                    if (dr2["EMPLOYEE_EMAIL_1"].ToString() != "")
        //                                    {
        //                                        if (mail_addr_emp == "")
        //                                        {
        //                                            mail_addr_emp = dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                        else
        //                                        {
        //                                            mail_addr_emp = mail_addr_emp + "," + dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                    }
        //                                }
        //                                HrsConn.Close();
        //                            }
        //                        }
        //                        emp_name_emp = Session["User_Name"].ToString();
        //                        mail_title_emp = txb_group_name.Text.Trim() + "開團資訊有進行變更，請確認";
        //                        mail_txt_emp = "團名：" + mail_title_emp + "<br/>開團人：" + emp_name_emp + "<br/>開團資訊變更，請進系統確認!!<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統登入頁面，帳號密碼為EIP密碼喔!!</a>";
        //                        //Response.Write(mail_addr);
        //                    }
        //                    else
        //                    {
        //                        string script2 = "alert('查無通知同工，請確認格式或同工編號正確')";
        //                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
        //                    }
        //                }

        //                if (check == true && check2 == true)
        //                {
        //                    //Response.Write("mail_addr_dep = " + mail_addr_dep+"<br/>");
        //                    //Response.Write("mail_addr_emp = " + mail_addr_emp + "<br/>");
        //                    SendMail(mail_title_dep, mail_addr_dep, mail_txt_dep);
        //                    SendMail(mail_title_emp, mail_addr_emp, mail_txt_emp);
        //                    Update_Group_Buy_Main_filename(filename);
        //                    string script = "alert('資訊修改完成，並重新發信通知!!')";
        //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //                }
        //            }
        //        }

        //        else
        //        {
        //            if (rdo_dep_emp.Checked == true && txb_dep_Notice.Text == "" && txb_emp_Notice.Text == "")
        //            {
        //                string script2 = "alert('若讀取權限為僅被通知部門與同工，則通知部門或通知同工必填')";
        //                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
        //            }

        //            else
        //            {
        //                Boolean check = true;
        //                Boolean check2 = true;
        //                if (txb_dep_Notice.Text != "")
        //                {
        //                    String word = txb_dep_Notice.Text;
        //                    String[] substrings = word.Split();

        //                    foreach (var substring in substrings)
        //                    {
        //                        SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                        HrsConn.Open();
        //                        string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where DEPARTMENT_CODE = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'  and EMPLOYEE_NO<>'1551'";
        //                        SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                        SqlDataReader dr2 = cmd2.ExecuteReader();
        //                        if (!dr2.HasRows)
        //                        {
        //                            check = false;
        //                        }
        //                        HrsConn.Close();
        //                    }

        //                    if (check == true)
        //                    {
        //                        foreach (var substring in substrings)
        //                        {
        //                            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                            HrsConn.Open();
        //                            string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where DEPARTMENT_CODE = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'  and EMPLOYEE_NO<>'1551'";
        //                            SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                            SqlDataReader dr2 = cmd2.ExecuteReader();
        //                            if (dr2.HasRows)
        //                            {
        //                                while (dr2.Read())
        //                                {
        //                                    if (dr2["EMPLOYEE_EMAIL_1"].ToString() != "")
        //                                    {
        //                                        if (mail_addr_dep == "")
        //                                        {
        //                                            mail_addr_dep = dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                        else
        //                                        {
        //                                            mail_addr_dep = mail_addr_dep + "," + dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            HrsConn.Close();
        //                        }
        //                        emp_name_dep = Session["User_Name"].ToString();
        //                        mail_title_dep = txb_group_name.Text.Trim() + "開團資訊有進行變更，請確認";
        //                        mail_txt_dep = "團名：" + mail_title_dep + "<br/>開團人：" + emp_name_dep + "<br/>開團資訊變更，請進系統確認!!<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統登入頁面，帳號密碼為EIP密碼喔!!</a>";
        //                        //Response.Write(mail_addr);
        //                    }

        //                    else
        //                    {
        //                        string script2 = "alert('查無通知部門，請確認格式或部門編號正確')";
        //                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
        //                    }
        //                }

        //                if (txb_emp_Notice.Text != "")
        //                {
        //                    String word = txb_emp_Notice.Text;
        //                    String[] substrings = word.Split();
        //                    foreach (var substring in substrings)
        //                    {
        //                        SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                        HrsConn.Open();
        //                        string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "'";
        //                        SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                        SqlDataReader dr2 = cmd2.ExecuteReader();
        //                        if (!dr2.HasRows)
        //                        {
        //                            check2 = false;
        //                        }
        //                        HrsConn.Close();
        //                    }

        //                    if (check2 == true)
        //                    {
        //                        foreach (var substring in substrings)
        //                        {
        //                            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                            HrsConn.Open();
        //                            string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "'";
        //                            SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                            SqlDataReader dr2 = cmd2.ExecuteReader();
        //                            if (dr2.HasRows)
        //                            {
        //                                while (dr2.Read())
        //                                {
        //                                    if (dr2["EMPLOYEE_EMAIL_1"].ToString() != "")
        //                                    {
        //                                        if (mail_addr_emp == "")
        //                                        {
        //                                            mail_addr_emp = dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                        else
        //                                        {
        //                                            mail_addr_emp = mail_addr_emp + "," + dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                    }
        //                                }
        //                                HrsConn.Close();
        //                            }
        //                        }
        //                        emp_name_emp = Session["User_Name"].ToString();
        //                        mail_title_emp = txb_group_name.Text.Trim() + "開團資訊有進行變更，請確認";
        //                        mail_txt_emp = "團名：" + mail_title_emp + "<br/>開團人：" + emp_name_emp + "<br/>開團資訊變更，請進系統確認!!<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統登入頁面，帳號密碼為EIP密碼喔!!</a>";
        //                        //Response.Write(mail_addr);
        //                    }
        //                    else
        //                    {
        //                        string script2 = "alert('查無通知同工，請確認格式或同工編號正確')";
        //                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
        //                    }
        //                }

        //                if (check == true && check2 == true)
        //                {
        //                    //Response.Write("mail_addr_dep = " + mail_addr_dep + "<br/>");
        //                    //Response.Write("mail_addr_emp = " + mail_addr_emp + "<br/>");
        //                    SendMail(mail_title_dep, mail_addr_dep, mail_txt_dep);
        //                    SendMail(mail_title_emp, mail_addr_emp, mail_txt_emp);
        //                    Update_Group_Buy_Main(txb_meun_addr.Text.Trim());
        //                    string script = "alert('資訊修改完成，並重新發信通知!!')";
        //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //                }
        //            }
        //        }
        //    }

        //    else
        //    {
        //        string script = "alert('請輸入開團標題')";
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //    }
        //}

        //protected void btn_update_buy_info_2_Click(object sender, EventArgs e)
        //{
        //    String savePath = Request.PhysicalApplicationPath + "NPOI\\";
        //    string emp_name_dep = "";
        //    string mail_title_dep = "";
        //    string mail_txt_dep = "";
        //    string mail_addr_dep = "";
        //    string emp_name_emp = "";
        //    string mail_title_emp = "";
        //    string mail_txt_emp = "";
        //    string mail_addr_emp = "";

        //    //建立資料夾
        //    if (!Directory.Exists(savePath))
        //    {
        //        Directory.CreateDirectory(savePath);
        //    }
        //    if (txb_group_name.Text != "")
        //    {
        //        if (FileUpload2.HasFile)
        //        {
        //            try
        //            {
        //                if (rdo_dep_emp.Checked == true && txb_dep_Notice.Text == "" && txb_emp_Notice.Text == "")
        //                {
        //                    string script2 = "alert('若讀取權限為僅被通知部門與同工，則通知部門或通知同工必填')";
        //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
        //                }

        //                else
        //                {
        //                    Boolean check = true;
        //                    Boolean check2 = true;
        //                    if (txb_dep_Notice.Text != "")
        //                    {
        //                        String word = txb_dep_Notice.Text;
        //                        String[] substrings = word.Split();

        //                        foreach (var substring in substrings)
        //                        {
        //                            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                            HrsConn.Open();
        //                            string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where DEPARTMENT_CODE = '" + substring + "'";
        //                            SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                            SqlDataReader dr2 = cmd2.ExecuteReader();
        //                            if (!dr2.HasRows)
        //                            {
        //                                check = false;
        //                            }
        //                            HrsConn.Close();
        //                        }

        //                        if (check == true)
        //                        {
        //                            foreach (var substring in substrings)
        //                            {
        //                                SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                                HrsConn.Open();
        //                                string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where DEPARTMENT_CODE = '" + substring + "'";
        //                                SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                                SqlDataReader dr2 = cmd2.ExecuteReader();
        //                                if (dr2.HasRows)
        //                                {
        //                                    while (dr2.Read())
        //                                    {
        //                                        if (mail_addr_dep == "")
        //                                        {
        //                                            mail_addr_dep = dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                        else
        //                                        {
        //                                            mail_addr_dep = mail_addr_dep + "," + dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                    }
        //                                }
        //                                HrsConn.Close();
        //                            }
        //                            emp_name_dep = Session["User_Name"].ToString();
        //                            mail_title_dep = txb_group_name.Text.Trim() + "開團資訊有進行變更，請確認";
        //                            mail_txt_dep = "團名：" + mail_title_dep + "<br/>開團人：" + emp_name_dep + "<br/>開團資訊變更，請進系統確認!!<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統登入頁面，帳號密碼為EIP密碼喔!!</a>";
        //                            //Response.Write(mail_addr);           
        //                        }

        //                        else
        //                        {
        //                            string script2 = "alert('查無通知部門，請確認格式或部門編號正確')";
        //                            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
        //                        }
        //                    }

        //                    if (txb_emp_Notice.Text != "")
        //                    {
        //                        String word = txb_emp_Notice.Text;
        //                        String[] substrings = word.Split();
        //                        foreach (var substring in substrings)
        //                        {
        //                            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                            HrsConn.Open();
        //                            string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "'";
        //                            SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                            SqlDataReader dr2 = cmd2.ExecuteReader();
        //                            if (!dr2.HasRows)
        //                            {
        //                                check2 = false;
        //                            }
        //                            HrsConn.Close();
        //                        }

        //                        if (check2 == true)
        //                        {
        //                            foreach (var substring in substrings)
        //                            {
        //                                SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                                HrsConn.Open();
        //                                string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "'";
        //                                SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                                SqlDataReader dr2 = cmd2.ExecuteReader();
        //                                if (dr2.HasRows)
        //                                {
        //                                    while (dr2.Read())
        //                                    {
        //                                        if (mail_addr_emp == "")
        //                                        {
        //                                            mail_addr_emp = dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                        else
        //                                        {
        //                                            mail_addr_emp = mail_addr_emp + "," + dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                    }
        //                                    HrsConn.Close();
        //                                }
        //                            }
        //                            emp_name_emp = Session["User_Name"].ToString();
        //                            mail_title_emp = txb_group_name.Text.Trim() + "開團資訊有進行變更，請確認";
        //                            mail_txt_emp = "團名：" + mail_title_emp + "<br/>開團人：" + emp_name_emp + "<br/>開團資訊變更，請進系統確認!!<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統登入頁面，帳號密碼為EIP密碼喔!!</a>";
        //                            //Response.Write(mail_addr);
        //                        }
        //                        else
        //                        {
        //                            string script2 = "alert('查無通知同工，請確認格式或同工編號正確')";
        //                            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
        //                        }
        //                    }

        //                    if (check == true && check2 == true)
        //                    {
        //                        string meun_id = get_meun_id();
        //                        delete_follow_meun(meun_id);
        //                        ins_follow_meun(savePath, meun_id);
        //                        Update_Group_Buy_Main(txb_store_addr.Text);
        //                        SendMail(mail_title_dep, mail_addr_dep, mail_txt_dep);
        //                        SendMail(mail_title_emp, mail_addr_emp, mail_txt_emp);

        //                        string script = "alert('資訊修改完成，並重新發信通知!!')";
        //                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //                    }
        //                }
        //            }

        //            catch (Exception ex)
        //            {
        //                Response.Write(ex);
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                if (rdo_dep_emp.Checked == true && txb_dep_Notice.Text == "" && txb_emp_Notice.Text == "")
        //                {
        //                    string script2 = "alert('若讀取權限為僅被通知部門與同工，則通知部門或通知同工必填')";
        //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
        //                }

        //                else
        //                {
        //                    Boolean check = true;
        //                    Boolean check2 = true;
        //                    if (txb_dep_Notice.Text != "")
        //                    {
        //                        String word = txb_dep_Notice.Text;
        //                        String[] substrings = word.Split();

        //                        foreach (var substring in substrings)
        //                        {
        //                            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                            HrsConn.Open();
        //                            string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where DEPARTMENT_CODE = '" + substring + "'";
        //                            SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                            SqlDataReader dr2 = cmd2.ExecuteReader();
        //                            if (!dr2.HasRows)
        //                            {
        //                                check = false;
        //                            }
        //                            HrsConn.Close();
        //                        }

        //                        if (check == true)
        //                        {

        //                            foreach (var substring in substrings)
        //                            {
        //                                SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                                HrsConn.Open();
        //                                string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where DEPARTMENT_CODE = '" + substring + "'";
        //                                SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                                SqlDataReader dr2 = cmd2.ExecuteReader();
        //                                if (dr2.HasRows)
        //                                {
        //                                    while (dr2.Read())
        //                                    {
        //                                        if (mail_addr_dep == "")
        //                                        {
        //                                            mail_addr_dep = dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                        else
        //                                        {
        //                                            mail_addr_dep = mail_addr_dep + "," + dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                    }
        //                                }
        //                                HrsConn.Close();
        //                            }
        //                            emp_name_dep = Session["User_Name"].ToString();
        //                            mail_title_dep = txb_group_name.Text.Trim() + "開團資訊有進行變更，請確認";
        //                            mail_txt_dep = "團名：" + mail_title_dep + "<br/>開團人：" + emp_name_dep + "<br/>開團資訊變更，請進系統確認!!<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統登入頁面，帳號密碼為EIP密碼喔!!</a>";
        //                            //Response.Write(mail_addr);

        //                        }

        //                        else
        //                        {
        //                            string script2 = "alert('查無通知部門，請確認格式或部門編號正確')";
        //                            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
        //                        }
        //                    }

        //                    if (txb_emp_Notice.Text != "")
        //                    {
        //                        String word = txb_emp_Notice.Text;
        //                        String[] substrings = word.Split();
        //                        foreach (var substring in substrings)
        //                        {
        //                            SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                            HrsConn.Open();
        //                            string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "'";
        //                            SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                            SqlDataReader dr2 = cmd2.ExecuteReader();
        //                            if (!dr2.HasRows)
        //                            {
        //                                check2 = false;
        //                            }
        //                            HrsConn.Close();
        //                        }

        //                        if (check2 == true)
        //                        {
        //                            foreach (var substring in substrings)
        //                            {
        //                                SqlConnection HrsConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HRS"].ConnectionString);
        //                                HrsConn.Open();
        //                                string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "'";
        //                                SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                                SqlDataReader dr2 = cmd2.ExecuteReader();
        //                                if (dr2.HasRows)
        //                                {
        //                                    while (dr2.Read())
        //                                    {
        //                                        if (mail_addr_emp == "")
        //                                        {
        //                                            mail_addr_emp = dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                        else
        //                                        {
        //                                            mail_addr_emp = mail_addr_emp + "," + dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                        }
        //                                    }
        //                                    HrsConn.Close();
        //                                }
        //                            }
        //                            emp_name_emp = Session["User_Name"].ToString();
        //                            mail_title_emp = txb_group_name.Text.Trim() + "開團資訊有進行變更，請確認";
        //                            mail_txt_emp = "團名：" + mail_title_emp + "<br/>開團人：" + emp_name_emp + "<br/>開團資訊變更，請進系統確認!!<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統登入頁面，帳號密碼為EIP密碼喔!!</a>";
        //                            //Response.Write(mail_addr);


        //                        }
        //                        else
        //                        {
        //                            string script2 = "alert('查無通知同工，請確認格式或同工編號正確')";
        //                            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
        //                        }
        //                    }

        //                    if (check == true && check2 == true)
        //                    {
        //                        Update_Group_Buy_Main(txb_store_addr.Text);
        //                        SendMail(mail_title_dep, mail_addr_dep, mail_txt_dep);
        //                        SendMail(mail_title_emp, mail_addr_emp, mail_txt_emp);

        //                        string script = "alert('資訊修改完成，並重新發信通知!!')";
        //                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //                    }
        //                }
        //            }

        //            catch (Exception ex)
        //            {
        //                Response.Write(ex);
        //            }
        //        }
        //    }

        //    else
        //    {
        //        string script = "alert('請輸入開團標題')";
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //    }
        //}
        //成團

        //public void Update_Group_Buy_Main(string addr)
        //{
        //    SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
        //    GroupBuyConn.Open();
        //    string sqlstring = @"UPDATE Group_main_table SET 開團標題=@開團標題,通知部門=@通知部門,通知同工=@通知同工,全開放=@全開放,開團備註=@開團備註,結單時間=@結單時間,菜單網址=@菜單網址 WHERE PK='" + Request.QueryString["PK"] + "';";
        //    SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
        //    cmd.Parameters.AddWithValue("@開團標題", txb_group_name.Text.Trim());
        //    cmd.Parameters.AddWithValue("@通知部門", txb_dep_Notice.Text.Trim());
        //    cmd.Parameters.AddWithValue("@通知同工", txb_emp_Notice.Text.Trim());
        //    bool chk_all = false;
        //    if (rdo_all.Checked == true)
        //    {
        //        chk_all = true;
        //    }
        //    cmd.Parameters.AddWithValue("@全開放", chk_all.ToString());
        //    cmd.Parameters.AddWithValue("@開團備註", txb_note.Text);
        //    cmd.Parameters.AddWithValue("@結單時間", txb_end_time.Text);
        //    cmd.Parameters.AddWithValue("@菜單網址", addr);

        //    cmd.ExecuteNonQuery();
        //    GroupBuyConn.Close();
        //}
    }
}