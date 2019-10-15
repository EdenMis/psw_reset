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
    public partial class TEST : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int tag_strat =1 - 1;
            int i = 0;
            int j = 0;
            int k = 0;
            string tag = "";
            string word = "";
            string templatePath = Request.PhysicalApplicationPath + "NPOI\\tag33.docx";

            // 讀入該範本檔
            DocX document = DocX.Load(templatePath);

            for (k = 0; k < tag_strat; k++)
            {
                tag = "#A" + k.ToString() + "#";
                document.ReplaceText(tag, "");
            }

            for (i = 0; i < GridView1.Rows.Count; i++)
            {
                word = GridView1.Rows[i].Cells[0].Text.ToString() + "–" + GridView1.Rows[i].Cells[1].Text.ToString() + "\n同工姓名：" + GridView1.Rows[i].Cells[2].Text.ToString();
                tag = "#A" + (i + tag_strat).ToString() + "#";
                document.ReplaceText(tag, word);
            }

            for (j = (i + tag_strat); j < 66; j++)
            {
                tag = "#A" + j.ToString() + "#";
                document.ReplaceText(tag, "");
            }
            // 指定成品儲存路徑及檔名並執行儲存
            string savePath = Request.PhysicalApplicationPath + "NPOI\\";
            string fileName = string.Format("標籤檔_3.docx", DateTime.Now.ToString("yyyyMMddHHmmss"));
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