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
using EDEN_Encode;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using IntraWare;

namespace psw_reset
{
    public partial class TEST : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        static string covEIPdeCode(string password)
        {

            IntraWare.System ob = new IntraWare.System();
            string decode = ob.Decode(password);
            return decode;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sql ="SELECT [id],[psaaword] FROM TEMP order by id";
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();

            SqlDataAdapter oda = new SqlDataAdapter(sql, GroupBuyConn);
            DataTable dt = new DataTable();
            oda.Fill(dt);

            //建立Excel 2003檔案
            IWorkbook wb = new HSSFWorkbook();
            HSSFCellStyle style = (HSSFCellStyle)wb.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)wb.CreateDataFormat();
            style.DataFormat = format.GetFormat("###,##0");
            ISheet ws;
            ISheet ws2;

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
                ws = wb.CreateSheet("Sheet1");
                ws2 = wb.CreateSheet("Sheet2");
            }

            ws.CreateRow(0);//第一行為欄位名稱
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ws.GetRow(0).CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);

            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ws.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j == 1)
                    {
                        ws.GetRow(i + 1).CreateCell(j).SetCellValue(covEIPdeCode(dt.Rows[i][j].ToString()));
                    }

                    else
                    {
                        ws.GetRow(i + 1).CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
            }

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ws.AutoSizeColumn(i);
            }

            MemoryStream MS = new MemoryStream();
            wb.Write(MS);
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=\"" + HttpUtility.UrlEncode("公用部門密碼表" + DateTime.Now, System.Text.Encoding.UTF8) + ".xls");
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            System.Web.HttpContext.Current.Response.BinaryWrite(MS.ToArray());
            wb = null;
            MS.Close();
            MS.Dispose();
            GroupBuyConn.Close();
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
            //FileStream file= new FileStream(@"E:\npoi.xls", FileMode.Create);//產生檔案
            //wb.Write(file);
            //file.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string emp = txb_emp.Text;
            byte[] bytes = System.Text.Encoding.GetEncoding("utf-8").GetBytes(emp);
            string emp_base64 = Convert.ToBase64String(bytes);

            SHA512 sha512 = new SHA512CryptoServiceProvider();
            //byte[] source = Encoding.Default.GetBytes(emp_base64+"108"+"v3geQ23Z");
            string ss = emp_base64 + "108" + "v3geQ23Z";
            byte[] source = Encoding.Default.GetBytes(emp_base64 + "108" + "v3geQ23Z");
            byte[] crypto = sha512.ComputeHash(source);//進行SHA512加密
            string hash = BitConverter.ToString(crypto).Replace("-", String.Empty);

            HyperLink1.NavigateUrl = "http://210.61.226.76/SingleSignOnLogin.aspx?token=" + emp_base64 + "&year=108&hash=" + hash;

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            TextBox2.Text=covEIPdeCode(TextBox1.Text);
        }
    }
}