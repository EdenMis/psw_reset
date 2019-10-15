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
    public partial class Open_buy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Login"].ToString() == null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('停留過久沒動作或沒經過驗證進來本頁，以上行為都需重新登入');location.href='../login.aspx'", true);
                }
            }

            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('停留過久沒動作或沒經過驗證進來本頁，以上行為都需重新登入');location.href='../login.aspx'", true);
            }
        }

        protected void btn_add_group_Click(object sender, EventArgs e)
        {
            
            string emp_name_dep = "";
            string mail_title_dep = "";
            string mail_txt_dep = "";
            string mail_addr_dep = "";
            string emp_name_emp = "";
            string mail_title_emp = "";
            string mail_txt_emp = "";
            string mail_addr_emp = "";

            if (txb_group_name.Text != "" && Dlist_store.Text!="" && txb_end_time.Text!="")
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
                                            if (dr2["EMPLOYEE_EMAIL_1"].ToString() != "")
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
                                    }
                                    HrsConn.Close();
                                }
                                emp_name_dep = Session["User_Name"].ToString();
                                mail_title_dep = txb_group_name.Text.Trim() + "開團了";
                                mail_txt_dep = "團名：" + mail_title_dep + "<br/>開團人：" + emp_name_dep + "<br/>結單時間：" + txb_end_time.Text + "<br/>開團內容：" + txb_group_note.Text + "<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統跟團</a>，帳號密碼為EIP密碼喔!!";
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
                                string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'";
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
                                    string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'";
                                    SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
                                    SqlDataReader dr2 = cmd2.ExecuteReader();
                                    if (dr2.HasRows)
                                    {
                                        while (dr2.Read())
                                        {
                                            if (dr2["EMPLOYEE_EMAIL_1"].ToString() != "")
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
                                        }
                                        HrsConn.Close();
                                    }
                                }
                                emp_name_emp = Session["User_Name"].ToString();
                                mail_title_emp = txb_group_name.Text.Trim() + "開團了";
                                mail_txt_emp = "團名：" + mail_title_emp + "<br/>開團人：" + emp_name_emp + "<br/>結單時間：" + txb_end_time.Text + "<br/>開團內容：" + txb_group_note.Text + "<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統跟團</a>，帳號密碼為EIP密碼喔!!";
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
                            SendMail(mail_title_dep, mail_addr_dep, mail_txt_dep);
                            SendMail(mail_title_emp, mail_addr_emp, mail_txt_emp);
                            ins_Group_Buy_Main();
                            string script = "window.parent.opener.location.href='../GroupBuy/index.aspx';";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "reload", script, true);
                            string script2 = "window.close();";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "close", script2, true);
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
                string script = "alert('請輸入團名，選擇店家，及設定結單時間!!')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
            }
        }

        //protected void Button1_Click1(object sender, EventArgs e)
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
        //        if (FileUpload1.HasFile || txb_meun_addr.Text != "")
        //        {
        //            string filename = "";
        //            if (FileUpload1.HasFile)
        //            {
        //                string year = DateTime.Now.Year.ToString();
        //                string month = DateTime.Now.Month.ToString();
        //                string day = DateTime.Now.Day.ToString();
        //                string hh = DateTime.Now.Hour.ToString();
        //                string mm = DateTime.Now.Minute.ToString();
        //                string ss = DateTime.Now.Second.ToString();

        //                filename = year + month + day + hh + mm + ss + FileUpload1.FileName;
        //                savePath = savePath + filename;
        //                FileUpload1.SaveAs(savePath);
        //            }

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
        //                        mail_title_dep = txb_group_name.Text.Trim() + "開團了";
        //                        mail_txt_dep = "團名：" + mail_title_dep + "<br/>開團人：" + emp_name_dep + "<br/>結單時間：" + txb_end_time.Text + "<br/>開團內容：" + txb_group_note.Text + "<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統跟團</a>，帳號密碼為EIP密碼喔!!";
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
        //                        mail_title_emp = txb_group_name.Text.Trim() + "開團了";
        //                        mail_txt_emp = "團名：" + mail_title_emp + "<br/>開團人：" + emp_name_emp + "<br/>結單時間：" + txb_end_time.Text + "<br/>開團內容：" + txb_group_note.Text + "<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統跟團</a>，帳號密碼為EIP密碼喔!!";
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
        //                    SendMail(mail_title_dep, mail_addr_dep, mail_txt_dep);
        //                    SendMail(mail_title_emp, mail_addr_emp, mail_txt_emp);
        //                    ins_Group_Buy_Main(filename, "");

        //                    string script = "window.parent.opener.location.href='../GroupBuy/index.aspx';";
        //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "reload", script, true);
        //                    string script2 = "window.close();";
        //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "close", script2, true);
        //                }
        //            }
        //        }

        //        else
        //        {
        //            string script = "alert('未上傳菜單或未填寫菜單網址')";
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //        }
        //    }

        //    else
        //    {
        //        string script = "alert('請輸入開團標題')";
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //    }
        //}

        protected void Dlsit_store_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dlist_store.Items.Clear();
            Dlist_store.Items.Add("");
            lab_store_name.Text = "";
            lab_store_type.Text = "";
            lab_store_addr.Text = "";
            lab_store_tel.Text = "";
            txb_group_store_note.Text = "";
            hyp_store_web_addr.Visible = false;
     

            if (Dlsit_store_type.Text == "請選擇")
            {
                Dlist_store.Visible = false;
                SqlDataSource1.SelectCommand = @"SELECT [PK], [店名],類型 FROM [Group_store_table] where 類型=''";
            }

            else
            {
                Dlist_store.Visible = true;
                SqlDataSource1.SelectCommand = @"SELECT [PK], [店名],類型 FROM [Group_store_table] where 類型='" + Dlsit_store_type.Text+ "' and (可使用部門 like '%" + Session["User_Dep_ID"].ToString() + "%' or 可使用同工 like '%"+Session["User_ID"].ToString()+ "%' or 全會可用='true' or 擁有者='" + Session["User_ID"].ToString() + "') and 啟用狀態='true'";
            }
        }

        protected void Dlist_store_SelectedIndexChanged(object sender, EventArgs e)
        {
            string store_name = Dlist_store.Text;
            load_store_info(store_name);
            fn_load_Group_store_meun_table(store_name);
        }

        public void ins_Group_Buy_Main()
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"INSERT INTO Group_main_table 
                                                                                (開團人部門
                                                                                ,開團人
                                                                                ,開團標題
                                                                                ,菜單類型
                                                                                ,店家名稱
                                                                                ,開團備註
                                                                                ,通知部門
                                                                                ,通知同工
                                                                                ,狀態
                                                                                ,開團時間
                                                                                ,全開放
                                                                                ,結單時間) 
                                                                                VALUES 
                                                                                (@開團人部門
                                                                                ,@開團人
                                                                                ,@開團標題,
                                                                                '固定式菜單'
                                                                                ,@店家名稱
                                                                                ,@開團備註
                                                                                ,@通知部門
                                                                                ,@通知同工
                                                                                ,'開團中'
                                                                                ,@開團時間
                                                                                ,@全開放
                                                                                ,@結單時間);";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@開團人部門", Session["User_Dep_Name"].ToString());
            cmd.Parameters.AddWithValue("@開團人", Session["User_Name"].ToString());
            cmd.Parameters.AddWithValue("@開團標題", txb_group_name.Text.Trim());
            cmd.Parameters.AddWithValue("@開團備註", txb_group_note.Text.Trim());
            cmd.Parameters.AddWithValue("@通知部門", txb_dep_Notice.Text.Trim());
            cmd.Parameters.AddWithValue("@通知同工", txb_emp_Notice.Text.Trim());
            cmd.Parameters.AddWithValue("@店家名稱", Dlist_store.Text);
            cmd.Parameters.AddWithValue("@開團時間", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@結單時間", txb_end_time.Text.Trim());
            bool chk_all = false;
            if (rdo_all.Checked == true)
            {
                chk_all = true;
            }
            cmd.Parameters.AddWithValue("@全開放", chk_all.ToString());
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

        public void load_store_info(string store_name)
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
                                              ,[店家網址]
                                              ,菜單檔名
                                          FROM [Group_store_table] where 店名='" + store_name+"'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                lab_store_name.Text = dr["店名"].ToString();
                lab_store_type.Text= dr["類型"].ToString();
                lab_store_addr.Text = dr["店家地址"].ToString();
                lab_store_tel.Text = dr["連絡電話"].ToString();
                txb_group_store_note.Text = dr["店家備註"].ToString();
                if (dr["店家網址"].ToString() != "")
                {
                    hyp_store_web_addr.Visible = true;
                    hyp_store_web_addr.Text = "店家連結網址";
                    hyp_store_web_addr.NavigateUrl = dr["店家網址"].ToString();
                }

                if (dr["店家網址"].ToString() == "" )
                {
                    hyp_store_web_addr.Visible = false;
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
                    HyperLink aa = new HyperLink();
                    aa.Text = dr["file_name"].ToString();
                    aa.ImageUrl = "~/menu/" + dr["file_name"].ToString();
                    aa.NavigateUrl = "~/menu/" + dr["file_name"].ToString();
                    aa.CssClass = "img_group";
                    img_panel.Controls.Add(aa);
                }
            }
            GroupBuyConn.Close();
        }

        protected void dlist_user_group_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlist_user_group_list.Text != "")
            {
                using (SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString))
                {
                    string sqlstring = @"SELECT [PK]
                                                          ,[owner]
                                                          ,[user_group_name]
                                                          ,[dep]
                                                          ,[emp]
                                                      FROM [GroupBuy].[dbo].[user_group_list] where user_group_name='" + dlist_user_group_list.Text + "'";
                    GroupBuyConn.Open();
                    SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        txb_dep_Notice.Text= dr["dep"].ToString();
                        txb_emp_Notice.Text=dr["emp"].ToString();
                    }
                    GroupBuyConn.Close();
                }
            }

            else
            {
                txb_dep_Notice.Text = "";
                txb_emp_Notice.Text = "";
            }
        }

        //==停止使用==

        //protected void Button2_Click(object sender, EventArgs e)
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
        //                            string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where DEPARTMENT_CODE = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'  and EMPLOYEE_NO<>'1551'";
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
        //                                string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where DEPARTMENT_CODE = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'  and EMPLOYEE_NO<>'1551'";
        //                                SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                                SqlDataReader dr2 = cmd2.ExecuteReader();
        //                                if (dr2.HasRows)
        //                                {
        //                                    while (dr2.Read())
        //                                    {
        //                                        if (dr2["EMPLOYEE_EMAIL_1"].ToString() != "")
        //                                        {
        //                                            if (mail_addr_dep == "")
        //                                            {
        //                                                mail_addr_dep = dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                            }
        //                                            else
        //                                            {
        //                                                mail_addr_dep = mail_addr_dep + "," + dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                                HrsConn.Close();
        //                            }
        //                            emp_name_dep = Session["User_Name"].ToString();
        //                            mail_title_dep = txb_group_name.Text.Trim() + "開團了";
        //                            mail_txt_dep = "團名：" + mail_title_dep + "<br/>開團人：" + emp_name_dep + "<br/>結單時間：" + txb_end_time.Text + "<br/>開團內容：" + txb_group_note.Text + "<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統跟團</a>，帳號密碼為EIP密碼喔!!";
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
        //                            string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'";
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
        //                                string sqlstring2 = @"SELECT [EMPLOYEE_NO],[EMPLOYEE_CNAME],[EMPLOYEE_EMAIL_1],[DEPARTMENT_CODE],[DEPARTMENT_CNAME] FROM [vwZZ_EMPLOYEE] where EMPLOYEE_NO = '" + substring + "' and [EMPLOYEE_WORK_STATUS]='1'";
        //                                SqlCommand cmd2 = new SqlCommand(sqlstring2, HrsConn);
        //                                SqlDataReader dr2 = cmd2.ExecuteReader();
        //                                if (dr2.HasRows)
        //                                {
        //                                    while (dr2.Read())
        //                                    {
        //                                        if (dr2["EMPLOYEE_EMAIL_1"].ToString() != "")
        //                                        {
        //                                            if (mail_addr_emp == "")
        //                                            {
        //                                                mail_addr_emp = dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                            }
        //                                            else
        //                                            {
        //                                                mail_addr_emp = mail_addr_emp + "," + dr2["EMPLOYEE_EMAIL_1"].ToString();
        //                                            }
        //                                        }
        //                                    }
        //                                    HrsConn.Close();
        //                                }
        //                            }
        //                            emp_name_emp = Session["User_Name"].ToString();
        //                            mail_title_emp = txb_group_name.Text.Trim() + "開團了";
        //                            mail_txt_emp = "團名：" + mail_title_emp + "<br/>開團人：" + emp_name_emp + "<br/>結單時間：" + txb_end_time.Text + "<br/>開團內容：" + txb_group_note.Text + "<br/><a href=http://eip.eden.org.tw/E-Form/Form/MIS_DOC005/Step1_MIS_1.asp> 點此進入系統跟團</a>，帳號密碼為EIP密碼喔!!";
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
        //                        string year = DateTime.Now.Year.ToString();
        //                        string month = DateTime.Now.Month.ToString();
        //                        string day = DateTime.Now.Day.ToString();
        //                        string hh = DateTime.Now.Hour.ToString();
        //                        string mm = DateTime.Now.Minute.ToString();
        //                        string ss = DateTime.Now.Second.ToString();
        //                        string meun_id = year + month + day + hh + mm + ss;

        //                        SendMail(mail_title_dep, mail_addr_dep, mail_txt_dep);
        //                        SendMail(mail_title_emp, mail_addr_emp, mail_txt_emp);

        //                        ins_follow_meun(savePath, meun_id);
        //                        ins_Group_Buy_Main("", meun_id);

        //                        string script = "window.parent.opener.location.href='../GroupBuy/index.aspx';";
        //                        ClientScript.RegisterClientScriptBlock(this.GetType(), "reload", script, true);
        //                        string script2 = "window.close();";
        //                        ClientScript.RegisterClientScriptBlock(this.GetType(), "close", script2, true);
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
        //            string script = "alert('請選擇菜單檔案')";
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //        }
        //    }

        //    else
        //    {
        //        string script = "alert('請輸入開團標題')";
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //    }
        //}



        //protected void rbo_open_meun_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbo_open_meun.Checked == true)
        //    {
        //        table_open_meun_1.Visible = true;
        //        table_open_meun_2.Visible = true;
        //        table_fixed_meun.Visible = false;
        //        table_fixed_meun_1.Visible = false;
        //    }
        //}

        //protected void rbo_fixed_meun_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbo_fixed_meun.Checked == true)
        //    {
        //        table_open_meun_1.Visible = false;
        //        table_open_meun_2.Visible = false;
        //        table_fixed_meun.Visible = true;
        //        table_fixed_meun_1.Visible = true;
        //    }
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

        //public void ins_Group_Buy_Main(string filename, string meun_id)
        //{
        //    SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
        //    GroupBuyConn.Open();
        //    string sqlstring = @"INSERT INTO Group_main_table (開團人部門,開團人,開團標題,菜單類型,菜單編號,菜單網址,菜單檔名,開團備註,通知部門,通知同工,狀態,開團時間,全開放,結單時間) VALUES (@開團人部門,@開團人,@開團標題,@菜單類型,@菜單編號,@菜單網址,@菜單檔名,@開團備註,@通知部門,@通知同工,'開團中',@開團時間,@全開放,@結單時間);";
        //    SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
        //    cmd.Parameters.AddWithValue("@開團人部門", Session["User_Dep_Name"].ToString());
        //    cmd.Parameters.AddWithValue("@開團人", Session["User_Name"].ToString());
        //    cmd.Parameters.AddWithValue("@開團標題", txb_group_name.Text.Trim());
        //    cmd.Parameters.AddWithValue("@開團備註", txb_group_note.Text.Trim());
        //    cmd.Parameters.AddWithValue("@通知部門", txb_dep_Notice.Text.Trim());
        //    cmd.Parameters.AddWithValue("@通知同工", txb_emp_Notice.Text.Trim());
        //    cmd.Parameters.AddWithValue("@菜單類型", meun_type);
        //    cmd.Parameters.AddWithValue("@菜單編號", meun_id);
        //    cmd.Parameters.AddWithValue("@菜單網址", meun_addr);
        //    cmd.Parameters.AddWithValue("@菜單檔名", filename);
        //    cmd.Parameters.AddWithValue("@開團時間", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        //    cmd.Parameters.AddWithValue("@結單時間", txb_end_time.Text.Trim());
        //    bool chk_all = false;
        //    if (rdo_all.Checked == true)
        //    {
        //        chk_all = true;
        //    }
        //    cmd.Parameters.AddWithValue("@全開放", chk_all.ToString());
        //    cmd.ExecuteNonQuery();
        //    GroupBuyConn.Close();
        //}

    }
}
