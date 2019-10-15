using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Web.Configuration;
using EDEN_Encode;


namespace psw_reset
{
    public partial class mis_test2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string err = "";
            SqlConnection GroupBuyConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GroupBuy"].ConnectionString);
            GroupBuyConn.Open();
            string sqlstring = "SELECT [id],[psaaword] FROM TEMP";
            SqlCommand cmd = new SqlCommand(sqlstring, GroupBuyConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string id = dr["id"].ToString();
                    string new_psw = "eden@" + covEIPdeCode(dr["psaaword"].ToString());
                    try
                    {
                        updateAD(id, new_psw);
                    }

                    catch
                    {
                        err = err +"<br/>"+ id;   
                    }
                }
                string script = "alert('處理完成')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                GroupBuyConn.Close();
                Label1.Text = err;

                
            }

            else
            {
                string script = "alert('查無資料')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
                GroupBuyConn.Close();
            }
        }

        public void updateAD(string user_ID, string new_psw)
        {
                string QueryString = "LDAP://ad2012.eden.org.tw";
                DirectoryEntry de = new DirectoryEntry(QueryString, "adapi", "Aa8f4w86><.");
                DirectorySearcher ds = new DirectorySearcher(de);
                //設定查詢條件：依姓名及工號查詢
                ds.Filter = @"(samaccountname=" + user_ID + ")";
                //將查詢結果放入Results
                SearchResult result = ds.FindOne();
                DirectoryEntry user = result.GetDirectoryEntry();
                user.Invoke("SetPassword", new_psw);
                de.Close();
        }

        static string covEIPdeCode(string password)
        {

            IntraWare.System ob = new IntraWare.System();
            string decode = ob.Decode(password);
            return decode;
        }

    }
}