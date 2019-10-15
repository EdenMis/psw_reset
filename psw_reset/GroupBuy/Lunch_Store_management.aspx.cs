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
    public partial class Lunch_Store_management : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Load_today_lunch();
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource3.SelectCommand = @"select [訂餐者同編] from lunch_order_table where 訂餐日期 ='" + today + "' group by [訂餐者同編]";
                GridView4.DataBind();
            }
        }

        protected void btn_add_store_Click(object sender, EventArgs e)
        {
            bool store_chk = fn_chk_have_store();
            if (store_chk == false)
            {
                SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
                GroupBuyConn.Open();
                string sqlstring = @"INSERT INTO Lunch_Store_main (店名,地址,電話,備註,good,ordinary,bad) VALUES (@店名,@地址,@電話,@備註,'0','0','0')";
                SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
                cmd.Parameters.AddWithValue("@店名", txb_store_name.Text.Trim());
                cmd.Parameters.AddWithValue("@地址", txb_store_addr.Text.Trim());
                cmd.Parameters.AddWithValue("@電話", txb_store_tel.Text.Trim());
                cmd.Parameters.AddWithValue("@備註", txb_note.Text.Trim());
                cmd.ExecuteNonQuery();
                GroupBuyConn.Close();
                GV_store_main.DataBind();
            }

            else
            {
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert(已經有這家店了!!');", true);
            }
        }

        protected void GV_store_main_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void GV_store_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            lab_store_name.Text = GV_store_main.SelectedRow.Cells[0].Text + "菜單編輯";
        }

        protected void btn_add_meun_Click(object sender, EventArgs e)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"INSERT INTO Lunch_Store_meun (店ID,店名,品名,價格) VALUES (@店ID,@店名,@品名,@價格)";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@店ID", GV_store_main.SelectedDataKey.Value);
            cmd.Parameters.AddWithValue("@店名", GV_store_main.SelectedRow.Cells[0].Text);
            cmd.Parameters.AddWithValue("@品名", txb_meun_name.Text.Trim());
            cmd.Parameters.AddWithValue("@價格", txb_meun_money.Text.Trim());
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
            SqlDataSource2.SelectCommand = @"SELECT [PK], 店ID,[店名], [品名], [價格] FROM [Lunch_Store_meun] WHERE ([店ID] ='" + GV_store_main.SelectedDataKey.Value + "' )";
            GV_store_meun.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string store_id = "";
            string store_name = "";
            String savePath = Request.PhysicalApplicationPath + "NPOI\\";
            try
            {
                store_id = GV_store_main.SelectedDataKey.Value.ToString();
                store_name = GV_store_main.SelectedRow.Cells[0].Text;
            }
            catch
            {
                string script = "alert('請先選擇店家')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
            }

            //建立資料夾

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            if (!string.IsNullOrEmpty(store_id))
            {
                if (FileUpload1.HasFile)
                {
                    try
                    {
                        delete_store_meun();
                        SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
                        Conn.Open();
                        String fileName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + FileUpload1.FileName;
                        savePath = savePath + fileName;
                        FileUpload1.SaveAs(savePath);

                        XSSFWorkbook workbook = new XSSFWorkbook(FileUpload1.FileContent);
                        XSSFSheet u_sheet = (XSSFSheet)workbook.GetSheetAt(0);

                        int i = 0;
                        for (i = u_sheet.FirstRowNum + 1; i <= u_sheet.LastRowNum; i++)
                        {
                            XSSFRow row = (XSSFRow)u_sheet.GetRow(i);
                            string sqlstring4 = @"INSERT INTO Lunch_Store_meun ([店ID]
                                                          ,[店名]
                                                          ,[品名]
                                                          ,[價格]) 
                                                  VALUES (
                                                  @0
                                                  ,@1
                                                  ,@2
                                                  ,@3)";
                            SqlCommand cmd = new SqlCommand(sqlstring4, Conn);
                            cmd.Parameters.AddWithValue("@0", GV_store_main.SelectedDataKey.Value);
                            cmd.Parameters.AddWithValue("@1", GV_store_main.SelectedRow.Cells[0].Text);
                            cmd.Parameters.AddWithValue("@2", row.GetCell(0).ToString());
                            cmd.Parameters.AddWithValue("@3", row.GetCell(1).ToString());
                            cmd.ExecuteNonQuery();
                            cmd.Cancel();
                        }
                        workbook = null;
                        u_sheet = null;
                        Conn.Close();
                        GV_store_meun.DataBind();
                    }

                    catch
                    {
                        string script = "alert('檔案格式錯誤')";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                    }
                }
                else
                {
                    string script = "alert('請選擇檔案')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                }
            }
            else
            {
                string script = "alert('請先選擇店家')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
            }
        }

        protected void btn_set_lunch_Click(object sender, EventArgs e)
        {
            string store_id = "";
            string store_name = "";
            try
            {
                store_id = GV_store_main.SelectedDataKey.Value.ToString();
                store_name = GV_store_main.SelectedRow.Cells[0].Text;
            }
            catch
            {
                string script = "alert('請先選擇店家')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
            }

            if (!string.IsNullOrEmpty(store_id))
            {
                delete_today_lunch();
                SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
                GroupBuyConn.Open();
                string sqlstring = @"INSERT INTO Lunch_set_main (店ID,店名,備註,日期,是否可訂) VALUES (@店ID,@店名,@備註,@日期,'1')";
                SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
                cmd.Parameters.AddWithValue("@店ID", GV_store_main.SelectedDataKey.Value);
                cmd.Parameters.AddWithValue("@店名", GV_store_main.SelectedRow.Cells[0].Text);
                cmd.Parameters.AddWithValue("@備註", txb_lunch_note.Text.Trim());
                cmd.Parameters.AddWithValue("@日期", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.ExecuteNonQuery();
                GroupBuyConn.Close();
                Load_today_lunch();
                Send_order_cancel_mail();
                delete_lunch_order_table();
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('本日午餐設定完成!!');", true);
            }
        }

        public Boolean fn_chk_have_store()
        {
            bool chk = false;
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @"SELECT [PK],[店名],[地址],[電話],[good],[ordinary],[bad] FROM Lunch_Store_main where 店名='" + txb_store_name + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                chk = true;
            }
            GroupBuyConn.Close();
            return chk;
        }

        public Boolean fn_chk_today_lunch()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            bool chk = false;
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @" select [PK],[店名],[備註],[日期] FROM Lunch_set_main where 日期='" + today + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                chk = true;
            }
            GroupBuyConn.Close();
            return chk;
        }

        public void Load_today_lunch()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            string sqlstring = @" select [PK],[店名],[備註],[日期] FROM Lunch_set_main where 日期='" + today + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                lab_today_lunch.Text = dr["店名"].ToString();
                txb_lunch_note.Text = dr["備註"].ToString();
            }

            else
            {
                lab_today_lunch.Text = "尚未設定";
                txb_lunch_note.Text = "";
            }
            GroupBuyConn.Close();
        }

        public void delete_today_lunch()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"delete from Lunch_set_main where 日期='" + today + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }

        public void delete_lunch_order_table()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"delete from lunch_order_table where 訂餐日期='" + today + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }

        public void delete_store_meun()
        {
            string store_id = GV_store_main.SelectedDataKey.Value.ToString();
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"delete from Lunch_Store_meun where 店ID='" + store_id + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
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

        public void Send_order_cancel_mail()
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
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string emp_name = Session["User_Name"].ToString();
            string mail_title = "本日「" + today + "」之午餐店家已更換，請有訂餐的同工重新進行訂餐";
            string mail_txt = @"今日午餐店家已更換為「" + lab_today_lunch.Text + "」,請同工重新進行訂餐!!";
            SendMail(mail_title, mail_addr, mail_txt);
        }
    }
}