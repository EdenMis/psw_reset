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

namespace psw_reset.GroupBuy
{
    public partial class edit_group_store : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Login"].ToString() == null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('停留過久沒動作或沒經過驗證進來本頁，以上行為都需重新登入');location.href='../Login.aspx'", true);
                }
            }

            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('停留過久沒動作或沒經過驗證進來本頁，以上行為都需重新登入');location.href='../Login.aspx'", true);
            }

            if (!Page.IsPostBack)
            {
                string store_name = fn_get_group_store_name();
                bool chk_owner = fn_chk_owner();
                if (chk_owner == true)
                {
                    fn_load_Group_store_meun_table(store_name);
                    fn_load_Group_store_table();
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('你沒有修改此店家之權限');location.href='../GroupBuy/login.aspx'", true);
                }
            }
        }

        //protected void btn_add_meun_Click(object sender, EventArgs e)
        //{
        //    string store_name = fn_get_group_store_name();
        //    SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
        //    GroupBuyConn.Open();
        //    string sqlstring = @"INSERT INTO follow_meun (店名,品名,價格) VALUES (@店名,@品名,@價格)";
        //    SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
        //    cmd.Parameters.AddWithValue("@店名", store_name);
        //    cmd.Parameters.AddWithValue("@品名", txb_meun_name.Text.Trim());
        //    cmd.Parameters.AddWithValue("@價格", txb_meun_money.Text.Trim());
        //    cmd.ExecuteNonQuery();
        //    GroupBuyConn.Close();
        //}

        protected void btn_update_group_store_Click(object sender, EventArgs e)
        {
            if (rdo_dep_emp.Checked == true && txb_dep_Notice.Text == "" && txb_emp_Notice.Text == "")
            {
                string script2 = "alert('若使用權限為僅被通知部門與同工，則通知部門或通知同工必填')";
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

                if (check == true && check2 == true)
                {
                    fn_Update_group_store_meun();
                    fn_Update_Group_store_table();
                    string store_name = fn_get_group_store_name();
                    fn_load_Group_store_meun_table(store_name);
                    string script2 = "alert('資料修改完成')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string store_name = fn_get_group_store_name();
            String savePath = Request.PhysicalApplicationPath + "menu\\";
            string meun_name = "";
            if (FileUpload1.HasFile)
            {
                string ext = System.IO.Path.GetExtension(FileUpload1.FileName);
                if (ext == ".jpg" || ext == ".png" || ext == ".JPG" || ext == ".PNG")
                {
                    fn_del_group_store_meun_table(txb_group_store_name.Text);
                    string year = DateTime.Now.Year.ToString();
                    string month = DateTime.Now.Month.ToString();
                    string day = DateTime.Now.Day.ToString();
                    string hh = DateTime.Now.Hour.ToString();
                    string mm = DateTime.Now.Minute.ToString();
                    string ss = DateTime.Now.Second.ToString();

                    foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                    {
                        meun_name = year + month + day + hh + mm + ss + Path.GetFileName(postedFile.FileName);
                        postedFile.SaveAs(savePath + meun_name);
                        fn_ins_group_store_meun_table(txb_group_store_name.Text, meun_name);
                    }
                    fn_load_Group_store_meun_table(store_name);
                }
                else
                {
                    string script = "alert('圖片格式錯誤，請用JPG或PNG格式上傳圖片')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                }
            }
            else
            {
                string script2 = "alert('未上傳檔案')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script2, true);
            }
        }

        //protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    string store_name = fn_get_group_store_name();
        //    SqlDataSource1.SelectCommand = @"SELECT [PK], [店名], [品名], [價格] FROM [follow_meun] WHERE [店名] = '" + store_name + "'";
        //    GridView1.DataBind();
        //}

        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    TextBox tb = new TextBox();
        //    tb = (TextBox)GridView1.Rows[e.RowIndex].Cells[1].FindControl("TextBox1");
        //    string money = tb.Text.Trim();
        //    if (!(new System.Text.RegularExpressions.Regex("^[0-9]+$")).IsMatch(tb.Text.Trim()))
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('價格格式錯誤，請輸入正數');", true);
        //        e.Cancel = true;
        //    }
        //}

        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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

        public void fn_delete_follow_meun(string store_name)
        {
            
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"delete from follow_meun where 店名='" + store_name + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }

        public void fn_ins_follow_meun(string store_name)
        {
            String savePath = Request.PhysicalApplicationPath + "NPOI\\";
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
                string sqlstring4 = @"INSERT INTO follow_meun ([店名]
                                                  ,[品名]
                                                  ,[價格]) 
                                                  VALUES (
                                                  @0
                                                  ,@1
                                                  ,@2)";
                SqlCommand cmd = new SqlCommand(sqlstring4, Conn);
                cmd.Parameters.AddWithValue("@0", store_name);
                cmd.Parameters.AddWithValue("@1", row.GetCell(0).ToString());
                cmd.Parameters.AddWithValue("@2", row.GetCell(1).ToString());
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
            workbook = null;
            u_sheet = null;
            Conn.Close();
        }

        public void fn_load_Group_store_table()
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
                                                          FROM Group_store_table where PK='" + Request.QueryString["PK"] + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                txb_old_store_name.Text= dr["店名"].ToString();
                txb_group_store_name.Text = dr["店名"].ToString();
                bool en = Convert.ToBoolean(dr["啟用狀態"]);
                if (en == true)
                {
                    rdo_en.Checked = true;
                }
                else
                {
                    rdo_dis_en.Checked = true;
                }
                bool all_user = Convert.ToBoolean(dr["全會可用"]);
                if (all_user == true)
                {
                    rdo_all.Checked = true;
                }
                else
                {
                    rdo_dep_emp.Checked = true;
                }
                txb_dep_Notice.Text = dr["可使用部門"].ToString();
                txb_emp_Notice.Text = dr["可使用同工"].ToString();
                Dlist_store_type.Text = dr["類型"].ToString();
                txb_group_store_addr.Text = dr["店家地址"].ToString();
                txb_group_store_tel.Text = dr["連絡電話"].ToString();
                txb_group_store_web_addr.Text = dr["店家網址"].ToString();
                txb_group_store_note.Text = dr["店家備註"].ToString();
            }
            GroupBuyConn.Close();
        }

        public void fn_Update_Group_store_table()
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"UPDATE Group_store_table
                                               SET
                                                [店名] = @店名
                                               ,[類型] = @類型
                                               ,[連絡電話] = @連絡電話
                                               ,[店家地址] = @店家地址
                                               ,[店家備註] = @店家備註
                                               ,[可使用同工] = @可使用同工
                                               ,[可使用部門] = @可使用部門
                                               ,[最後更新時間] = @最後更新時間
                                               ,[最後修改人] = @最後修改人
                                               ,[全會可用] = @全會可用
                                               ,[啟用狀態] = @啟用狀態
                                               ,[店家網址] = @店家網址
                                                WHERE PK='" + Request.QueryString["PK"] + "';";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@店名", txb_group_store_name.Text.Trim());
            cmd.Parameters.AddWithValue("@類型", Dlist_store_type.Text);
            cmd.Parameters.AddWithValue("@連絡電話", txb_group_store_tel.Text.Trim());
            cmd.Parameters.AddWithValue("@店家地址", txb_group_store_addr.Text.Trim());
            cmd.Parameters.AddWithValue("@可使用同工", txb_emp_Notice.Text.Trim());
            cmd.Parameters.AddWithValue("@可使用部門", txb_dep_Notice.Text.Trim());
            cmd.Parameters.AddWithValue("@店家備註", txb_group_store_note.Text.Trim());
            cmd.Parameters.AddWithValue("@最後更新時間", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@最後修改人", Session["User_ID"].ToString());
            bool chk_all = false;
            if (rdo_all.Checked == true)
            {
                chk_all = true;
            }
            cmd.Parameters.AddWithValue("@全會可用", chk_all.ToString());
            bool chk_en = false;
            if (rdo_en.Checked == true)
            {
                chk_en = true;
            }
            cmd.Parameters.AddWithValue("@啟用狀態", chk_en.ToString());
            cmd.Parameters.AddWithValue("@店家網址", txb_group_store_web_addr.Text.Trim());
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }

        public void fn_Update_group_store_meun()
        {
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = @"UPDATE Group_store_meun_table
                                               SET                                  
                                                store_name=@new_store_name
                                                WHERE store_name=@old_store_name;";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            cmd.Parameters.AddWithValue("@new_store_name", txb_group_store_name.Text);
            cmd.Parameters.AddWithValue("@old_store_name",txb_old_store_name.Text);
            cmd.ExecuteNonQuery();
            GroupBuyConn.Close();
        }

        public string fn_get_group_store_name()
        {
            string store_name = "";
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
                                                  ,[店家網址] FROM Group_store_table where PK='" + Request.QueryString["PK"] + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                store_name = dr["店名"].ToString();
            }
            GroupBuyConn.Close();
            return store_name;
        }

        public bool fn_chk_owner()
        {
            bool chk_owner =false;
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
                                                  ,[擁有者] FROM Group_store_table where PK='" + Request.QueryString["PK"] + "'";
            GroupBuyConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                string owner = dr["擁有者"].ToString();
                string all_user = dr["全會可用"].ToString();
                if (owner == Session["User_ID"].ToString() || all_user=="True")
                {
                    chk_owner = true;
                }  
            }
            GroupBuyConn.Close();
            return chk_owner;
        }


        public void fn_ins_group_store_meun_table(string store_name, string meun_name)
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
    }
}