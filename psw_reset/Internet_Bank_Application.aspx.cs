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
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using Xceed.Words.NET;
using System.Text.RegularExpressions;
using System.Globalization;

namespace psw_reset
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "update")
            {
                string errmsg = fn_chk_text();
                if (errmsg != "")
                {
                    string script = "alert('" + errmsg + "')";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                }

                else
                {
                    if (FileUpload1.HasFile == true)
                    {
                        string ext = System.IO.Path.GetExtension(FileUpload1.FileName);
                        if (ext == ".jpg" || ext == ".png" || ext == ".JPG" || ext == ".PNG" || ext == "tif" || ext == ".TIF" || ext == ".pdf" || ext == ".PDF")
                        {
                            string filename = FileUpload1.FileName;
                            string filename2 = txb_id.Text.Trim() + ext;
                            String serverDir = Request.PhysicalApplicationPath + "application\\";
                            if (Directory.Exists(serverDir) == false)
                            {
                                Directory.CreateDirectory(serverDir);
                            }
                            string serverFilePath = Path.Combine(serverDir, filename2);
                            FileUpload1.SaveAs(serverFilePath);
                            fn_ins_company_list("1");
                            string script = "alert('線上申請作業完成')";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                        }

                        else
                        {
                            string script = "alert('上傳檔案格式只支援jpg、png、tif、pdf')";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                        }
                    }

                    else
                    {
                        fn_ins_company_list("0");
                        string script = "alert('線上申請作業完成')";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                    }
                }
            }
        }


        //protected void btn_send_Click(object sender, EventArgs e)
        //{
        //    string errmsg = fn_chk_text();
        //    if (errmsg != "")
        //    {
        //        string script = "alert('" + errmsg + "')";
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //    }

        //    else
        //    {
        //        if (FileUpload1.HasFile == true)
        //        {
        //            string ext = System.IO.Path.GetExtension(FileUpload1.FileName);
        //            if (ext == ".jpg" || ext == ".png" || ext == ".JPG" || ext == ".PNG" || ext == "tif" || ext == ".TIF" || ext == ".pdf" || ext == ".PDF")
        //            {
        //                string filename = FileUpload1.FileName;
        //                string filename2 = txb_id.Text.Trim() + ext;
        //                String serverDir = Request.PhysicalApplicationPath + "application\\";
        //                if (Directory.Exists(serverDir) == false)
        //                {
        //                    Directory.CreateDirectory(serverDir);
        //                }
        //                string serverFilePath = Path.Combine(serverDir, filename2);
        //                FileUpload1.SaveAs(serverFilePath);
        //                fn_ins_company_list("1");
        //                string script = "alert('線上申請作業完成')";
        //                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //            }

        //            else
        //            {
        //                string script = "alert('上傳檔案格式只支援jpg、png、tif、pdf')";
        //                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //            }
        //        }

        //        else
        //        {
        //            fn_ins_company_list("0");
        //            string script = "alert('線上申請作業完成')";
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
        //        }
        //    }
        //}

        //protected void rdo_type_company_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdo_type_company.Checked == true)
        //    {
        //        lab_id_title.Text = "統一編號";
        //        lab_name_title.Text = "公司全銜";
        //        btn_same.Text = "同公司全銜";
        //    }
        //}

        //protected void rdo_type_person_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdo_type_person.Checked == true)
        //    {
        //        lab_id_title.Text = "身分證字號";
        //        lab_name_title.Text = "個人戶/姓名";
        //        btn_same.Text = "同個人戶/姓名";
        //    }
        //}

        protected void Dlist_bank_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection ErpConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ERP"].ConnectionString);
            string sqlstring = @"SELECT ID_BANK, NM_BANK,NM_BANK_FULL FROM FCM2BANK where ID_BANK='" + Dlist_bank_id.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlstring, ErpConn);
            try
            {
                ErpConn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    lab_bank_id.Text = dr["ID_BANK"].ToString();
                    lab_bank_name.Text = dr["NM_BANK"].ToString();
                    lab_bank_name_full.Text = dr["NM_BANK_FULL"].ToString();
                    ErpConn.Close();
                }
                else
                {
                    ErpConn.Close();
                }
            }
            catch
            {
                ErpConn.Close();
            }

            finally
            {
                ErpConn.Close();
            }
        }


        public void fn_ins_company_list(string upload_flag)
        {
            string type = lab_type.Text.Trim();          

            SqlConnection IBConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Internet_Bank"].ConnectionString);
            IBConn.Open();
            string sqlstring = @"INSERT INTO company_list
                                                   ([type]
      ,[id]
      ,[name]
      ,[Contact_person]
      ,[tel]
      ,[fax]
      ,[mail]
      ,[account_name]
      ,[bank_id]
      ,[bank_name]
      ,[bank_name_full]
      ,[account]
      ,[create_time]
,upload_flag)
                                             VALUES
                                                   (@type
                                                   ,@id
                                                   ,@name
                                                   ,@Contact_person
                                                   ,@tel
                                                   ,@fax
                                                   ,@mail
                                                   ,@account_name
                                                   ,@bank_id
                                                   ,@bank_name
                                                   ,@bank_name_full
                                                   ,@account
                                                   ,@create_time,@upload_flag)";
            SqlCommand cmd = new SqlCommand(sqlstring, IBConn);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@id", txb_id.Text.Trim());
            cmd.Parameters.AddWithValue("@name", txb_name.Text.Trim());
            cmd.Parameters.AddWithValue("@Contact_person", txb_Contact_person.Text.Trim());
            cmd.Parameters.AddWithValue("@tel", txb_tel.Text.Trim());
            cmd.Parameters.AddWithValue("@fax", txb_fax.Text.Trim());
            cmd.Parameters.AddWithValue("@mail", txb_mail.Text.Trim());
            cmd.Parameters.AddWithValue("@account_name", txb_account_name.Text.Trim());
            cmd.Parameters.AddWithValue("@bank_id", lab_bank_id.Text.Trim());
            cmd.Parameters.AddWithValue("@bank_name", lab_bank_name.Text.Trim());
            cmd.Parameters.AddWithValue("@bank_name_full", lab_bank_name_full.Text.Trim());
            cmd.Parameters.AddWithValue("@account", txb_account.Text.Trim());
            cmd.Parameters.AddWithValue("@create_time", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@upload_flag",upload_flag);
            cmd.ExecuteNonQuery();
            IBConn.Close();
        }

        public string fn_chk_text()
        {
            string errmsg = "";
            if (txb_id.Text == "")
            {
                if (errmsg == "")
                {
                    errmsg = "統一編號/身分證字未填";
                }
            }

            if (txb_id.Text != "")
            {
                bool chk = fn_id_chk();
                if (chk == false)
                {
                    if (errmsg == "")
                    {
                        errmsg = "系統已有此統一編號/身分證字號";
                    }
                    else
                    {
                        errmsg = errmsg + "，系統已有此統一編號/身分證字號";
                    }
                }
            }

            if (txb_name.Text == "")
            {
                if (errmsg == "")
                {
                    errmsg = "公司全銜/個人戶姓名未填";
                }
                else
                {
                    errmsg = errmsg + "，公司全銜/個人戶姓名未填";
                }
            }

            if (txb_Contact_person.Text == "")
            {
                if (errmsg == "")
                {
                    errmsg = "聯絡人未填";
                }
                else
                {
                    errmsg = errmsg + "，聯絡人未填";
                }
            }

            if (txb_tel.Text == "")
            {
                if (errmsg == "")
                {
                    errmsg = "電話未填";
                }
                else
                {
                    errmsg = errmsg + "，電話未填";
                }
            }

            if (txb_fax.Text == "" && txb_mail.Text == "")
            {
                if (errmsg == "")
                {
                    errmsg = "傳真或是E-Mail未填";
                }
                else
                {
                    errmsg = errmsg + "，傳真或是E-Mail未填";
                }
            }

            if (txb_mail.Text.Trim() != "")
            {
                bool mail_chk = IsValidEmail(txb_mail.Text.Trim());
                if (mail_chk == false)
                {
                    if (errmsg == "")
                    {
                        errmsg = "E-Mail格式錯誤";
                    }
                    else
                    {
                        errmsg = errmsg + "，E-Mail格式錯誤";
                    }
                }
            }

            if (txb_account_name.Text == "")
            {
                if (errmsg == "")
                {
                    errmsg = "銀行戶名未填";
                }
                else
                {
                    errmsg = errmsg + "，銀行戶名未填";
                }
            }

            if (txb_account.Text == "")
            {
                if (errmsg == "")
                {
                    errmsg = "帳號未填";
                }
                else
                {
                    errmsg = errmsg + "，帳號未填";
                }
            }

            if (chk_yes.Checked != true)
            {
                if (errmsg == "")
                {
                    errmsg = "確認事項未勾選";
                }
                else
                {
                    errmsg = errmsg + "，確認事項未勾選";
                }
            }

            if (Dlist_bank_id.Text == "")
            {
                if (errmsg == "")
                {
                    errmsg = "銀行代號未選";
                }
                else
                {
                    errmsg = errmsg + "，銀行代號未選";
                }
            }

            //if (FileUpload1.HasFile == false)
            //{
            //    if (errmsg == "")
            //    {
            //        errmsg = "用印網路銀行匯款申請書文件未上傳";
            //    }
            //    else
            //    {
            //        errmsg = errmsg + "，用印網路銀行匯款申請書文件未上傳";
            //    }
            //}
            return errmsg;
        }

        public bool fn_id_chk()
        {
            bool chk = true;
            SqlConnection IBConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Internet_Bank"].ConnectionString);
            string sqlstring = @"SELECT id FROM company_list where id=@id";
            SqlCommand cmd = new SqlCommand(sqlstring, IBConn);
            cmd.Parameters.AddWithValue("@id", txb_id.Text.Trim());
            IBConn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                chk = false;
                IBConn.Close();
            }
            else
            {
                IBConn.Close();
            }

            return chk;
        }

        public bool fn_IsNatural_English(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]{1}.*$");
            return reg1.IsMatch(str);
        }

        protected void btn_same_Click(object sender, EventArgs e)
        {
            txb_account_name.Text = txb_name.Text.Trim();
        }

        protected void btn_word_Click(object sender, EventArgs e)
        {
            string templatePath = "";
            if (lab_type.Text == "公司戶")
            {
                templatePath = Request.PhysicalApplicationPath + "NPOI\\company.docx";
            }

            if (lab_type.Text == "個人戶")
            {
                templatePath = Request.PhysicalApplicationPath + "NPOI\\person.docx";
            }

            // 讀入該範本檔
            DocX document = DocX.Load(templatePath);

            document.ReplaceText("#A0#", txb_id.Text.Trim());
            document.ReplaceText("#A1#", txb_name.Text.Trim());
            document.ReplaceText("#A2#", txb_Contact_person.Text.Trim());
            document.ReplaceText("#A3#", txb_tel.Text.Trim());
            document.ReplaceText("#A4#", txb_fax.Text.Trim());
            document.ReplaceText("#A5#", txb_mail.Text.Trim());
            document.ReplaceText("#A6#", txb_account_name.Text.Trim());
            document.ReplaceText("#A7#", lab_bank_name.Text.Trim());
            document.ReplaceText("#A8#", lab_bank_id.Text.Trim());
            document.ReplaceText("#A9#", txb_account.Text.Trim());
            document.ReplaceText("#A11#", DateTime.Now.ToString("yyyy-MM-dd"));

            // 指定成品儲存路徑及檔名並執行儲存
            string savePath = Request.PhysicalApplicationPath + "NPOI\\";
            string fileName = string.Format(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "網路銀行匯款同意書.docx", DateTime.Now.ToString("yyyyMMddHHmmss"));
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

        public bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$");
        }

        protected void txb_id_TextChanged(object sender, EventArgs e)
        {
            bool chk = fn_IsNatural_English(txb_id.Text);

            if (chk == true)
                lab_type.Text = "個人戶";
            else
                lab_type.Text = "公司戶";
        }
    }
}