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
    public partial class add_group_store : System.Web.UI.Page
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
        }

        protected void btn_add_group_store_Click(object sender, EventArgs e)
        {
            String savePath = Request.PhysicalApplicationPath + "menu\\";

            if (txb_group_store_name.Text != "" && txb_group_store_tel.Text!="" && Dlist_store_type.Text!="請選擇")
            {
                bool group_name_chk = fn_chk_group_store_name();
                if (group_name_chk == false)
                {
                    if (FileUpload2.HasFile || txb_group_store_web_addr.Text != "")
                    {
                        try
                        {
                            bool file_check = true;
                            string meun_name = "";
                            if (FileUpload2.HasFile)
                            {
                                string ext = System.IO.Path.GetExtension(FileUpload2.FileName);
                                if (ext == ".jpg" || ext == ".png" || ext == ".JPG" || ext == ".PNG")
                                {
                                    fn_del_group_store_meun_table(txb_group_store_name.Text);
                                    string year = DateTime.Now.Year.ToString();
                                    string month = DateTime.Now.Month.ToString();
                                    string day = DateTime.Now.Day.ToString();
                                    string hh = DateTime.Now.Hour.ToString();
                                    string mm = DateTime.Now.Minute.ToString();
                                    string ss = DateTime.Now.Second.ToString();

                                    foreach (HttpPostedFile postedFile in FileUpload2.PostedFiles)
                                    {
                                        meun_name = year + month + day + hh + mm + ss + Path.GetFileName(postedFile.FileName);
                                        postedFile.SaveAs(savePath + meun_name);
                                        fn_ins_group_store_meun_table(txb_group_store_name.Text, meun_name);
                                    }
                                }
                                else
                                {
                                    file_check = false;
                                    string script = "alert('圖片格式錯誤，請用JPG或PNG格式上傳圖片')";
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);    
                                }
                            }

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
                                        string script2 = "alert('查無部門，請確認格式或部門編號正確')";
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
                                        string script2 = "alert('查無同工，請確認格式或同工編號正確')";
                                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
                                    }
                                }

                                if (check == true && check2 == true && file_check==true)
                                {
                                    fn_ins_group_store_table(meun_name);
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
                        string script = "alert('菜單圖片或店家介紹網址不可都為空')";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                    }
                }

                else
                {
                    string script = "alert('已有此店家名稱')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                }
     
            }

            else
            {
                string script = "alert('請輸入店家名稱及店家電話，及選擇店家類型')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
            }
        }

        public void fn_ins_group_store_table(string meun_name)
        {
            //SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            //GroupBuyConn.Open();
            //string sqlstring = @"INSERT INTO Group_store_table
            //                                       (店名
            //                                       ,類型
            //                                       ,連絡電話
            //                                       ,店家地址
            //                                       ,店家網址
            //                                       ,店家備註
            //                                       ,可使用同工
            //                                       ,可使用部門
            //                                       ,最後更新時間
            //                                       ,最後修改人
            //                                       ,全會可用
            //                                       ,啟用狀態
            //                                       ,擁有者
            //                                       ,菜單檔名)
            //                                 VALUES
            //                                       (@店名
            //                                       ,@類型
            //                                       ,@連絡電話
            //                                       ,@店家地址
            //                                       ,@店家網址
            //                                       ,@店家備註
            //                                       ,@可使用同工
            //                                       ,@可使用部門
            //                                       ,@最後更新時間
            //                                       ,@最後修改人
            //                                       ,@全會可用
            //                                       ,'1'
            //                                       ,@擁有者
            //                                       ,@菜單檔名)";
            //SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            //cmd.Parameters.AddWithValue("@店名", txb_group_store_name.Text.Trim());
            //cmd.Parameters.AddWithValue("@類型", Dlist_store_type.Text);
            //cmd.Parameters.AddWithValue("@連絡電話", txb_group_store_tel.Text.Trim());
            //cmd.Parameters.AddWithValue("@店家地址", txb_group_store_addr.Text.Trim());
            //cmd.Parameters.AddWithValue("@店家備註", txb_group_store_note.Text.Trim());
            //cmd.Parameters.AddWithValue("@店家網址", txb_group_store_web_addr.Text.Trim());
            //cmd.Parameters.AddWithValue("@可使用同工", txb_emp_Notice.Text.Trim());
            //cmd.Parameters.AddWithValue("@可使用部門", txb_dep_Notice.Text.Trim());
            //cmd.Parameters.AddWithValue("@最後修改人", Session["User_ID"].ToString());
            //cmd.Parameters.AddWithValue("@擁有者", Session["User_ID"].ToString());
            //cmd.Parameters.AddWithValue("@菜單檔名", meun_name);
            //cmd.Parameters.AddWithValue("@最後更新時間", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            //bool chk_all = false;
            //if (rdo_all.Checked == true)
            //{
            //    chk_all = true;
            //}
            //cmd.Parameters.AddWithValue("@全會可用", chk_all.ToString());
            //cmd.ExecuteNonQuery();
            //GroupBuyConn.Close();
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"INSERT INTO Group_store_table
                                                   (店名
                                                   ,類型
                                                   ,連絡電話
                                                   ,店家地址
                                                   ,店家網址
                                                   ,店家備註
                                                   ,可使用同工
                                                   ,可使用部門
                                                   ,最後更新時間
                                                   ,最後修改人
                                                   ,全會可用
                                                   ,啟用狀態
                                                   ,擁有者)
                                             VALUES
                                                   (@店名
                                                   ,@類型
                                                   ,@連絡電話
                                                   ,@店家地址
                                                   ,@店家網址
                                                   ,@店家備註
                                                   ,@可使用同工
                                                   ,@可使用部門
                                                   ,@最後更新時間
                                                   ,@最後修改人
                                                   ,@全會可用
                                                   ,'1'
                                                   ,@擁有者)";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@店名", txb_group_store_name.Text.Trim());
            cmd.Parameters.AddWithValue("@類型", Dlist_store_type.Text);
            cmd.Parameters.AddWithValue("@連絡電話", txb_group_store_tel.Text.Trim());
            cmd.Parameters.AddWithValue("@店家地址", txb_group_store_addr.Text.Trim());
            cmd.Parameters.AddWithValue("@店家備註", txb_group_store_note.Text.Trim());
            cmd.Parameters.AddWithValue("@店家網址", txb_group_store_web_addr.Text.Trim());
            cmd.Parameters.AddWithValue("@可使用同工", txb_emp_Notice.Text.Trim());
            cmd.Parameters.AddWithValue("@可使用部門", txb_dep_Notice.Text.Trim());
            cmd.Parameters.AddWithValue("@最後修改人", Session["User_ID"].ToString());
            cmd.Parameters.AddWithValue("@擁有者", Session["User_ID"].ToString());
            cmd.Parameters.AddWithValue("@最後更新時間", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            bool chk_all = false;
            if (rdo_all.Checked == true)
            {
                chk_all = true;
            }
            cmd.Parameters.AddWithValue("@全會可用", chk_all.ToString());
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }

        public void fn_ins_group_store_meun_table(string store_name,string meun_name)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"INSERT INTO Group_store_meun_table
                                                   (store_name
                                                   ,file_name)
                                             VALUES
                                                   (@store_name
                                                   ,@file_name)";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@store_name", store_name);
            cmd.Parameters.AddWithValue("@file_name", meun_name);
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }

        public void fn_del_group_store_meun_table(string store_name)
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"DELETE FROM Group_store_meun_table WHERE store_name=@store_name";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@store_name", store_name);
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }

        public void fn_ins_follow_meun(string savePath)
        {
            SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            Conn.Open();
            String fileName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + FileUpload2.FileName;
            savePath = savePath + fileName;
            FileUpload2.SaveAs(savePath);

            XSSFWorkbook workbook = new XSSFWorkbook(FileUpload2.FileContent);
            XSSFSheet u_sheet = (XSSFSheet)workbook.GetSheetAt(0);

            int i = 0;
            for (i = u_sheet.FirstRowNum + 1; i <= u_sheet.LastRowNum; i++)
            {
                XSSFRow row = (XSSFRow)u_sheet.GetRow(i);
                string sqlstring4 = @"INSERT INTO follow_meun ([店名]
                                                  ,[品名]
                                                  ,[價格]) 
                                                  VALUES (
                                                  @0
                                                  ,@1
                                                  ,@2)";
                SqlCommand cmd = new SqlCommand(sqlstring4, Conn);
                cmd.Parameters.AddWithValue("@0", txb_group_store_name.Text.Trim());
                cmd.Parameters.AddWithValue("@1", row.GetCell(0).ToString());
                cmd.Parameters.AddWithValue("@2", row.GetCell(1).ToString());
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
            workbook = null;
            u_sheet = null;
            Conn.Close();
        }

        public bool fn_chk_group_store_name()
        {
            bool chk = false;
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
                                                                  ,[啟用狀態]
                                                              FROM [Group_store_table] where 店名='" + txb_group_store_name.Text.Trim() + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                chk = true;
            }
            return chk;
        }
    }
}