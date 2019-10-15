using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace psw_reset.GroupBuy
{
    public partial class My_Follow_Group : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    int i;
        //    for (i = 0; i < GridView1.Rows.Count; i++)
        //    {
        //        if (GridView1.Rows[i].Cells[1].Text == "未支付")
        //        {
        //            GridView1.Rows[i].Cells[1].ForeColor = System.Drawing.Color.Red;
        //        }

        //        if (GridView1.Rows[i].Cells[1].Text == "已支付")
        //        {
        //            GridView1.Rows[i].Cells[1].ForeColor = System.Drawing.Color.Blue;
        //        }

        //        if (GridView1.Rows[i].Cells[6].Text != "開團中")
        //        {
        //            GridView1.Rows[i].FindControl("btn_edit").Visible = false;
        //            GridView1.Rows[i].FindControl("btn_delete").Visible = false;
        //        }
        //    }
        //}

        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    TextBox tb = new TextBox();
        //    tb = (TextBox)GridView1.Rows[e.RowIndex].Cells[4].FindControl("TextBox1");
        //    string money = tb.Text.Trim();
        //    if (!(new System.Text.RegularExpressions.Regex("^[0-9]+$")).IsMatch(tb.Text.Trim()))
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('金額格式錯誤，請輸入正數');", true);
        //        e.Cancel = true;
        //    }
        //}

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int i;
            for (i = 0; i < GridView2.Rows.Count; i++)
            {
                //if (GridView2.Rows[i].Cells[1].Text == "未支付")
                //{
                //    GridView2.Rows[i].Cells[1].ForeColor = System.Drawing.Color.Red;
                //}

                //if (GridView2.Rows[i].Cells[1].Text == "已支付")
                //{
                //    GridView2.Rows[i].Cells[1].ForeColor = System.Drawing.Color.Blue;
                //}

                if (GridView2.Rows[i].Cells[8].Text != "開團中")
                {
                    GridView2.Rows[i].FindControl("btn_edit").Visible = false;
                    GridView2.Rows[i].FindControl("btn_delete").Visible = false;
                }
            }
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox tb = new TextBox();
            tb = (TextBox)GridView2.Rows[e.RowIndex].Cells[4].FindControl("TextBox1");
            if (!(new System.Text.RegularExpressions.Regex("^[0-9]+$")).IsMatch(tb.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('數量格式錯誤，請輸入正數');", true);
                e.Cancel = true;
            }

            TextBox tb2 = new TextBox();
            tb2 = (TextBox)GridView2.Rows[e.RowIndex].Cells[4].FindControl("TextBox2");
            if (!(new System.Text.RegularExpressions.Regex("^[0-9]+$")).IsMatch(tb2.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(GetType(), "GetOut", "alert('價格格式錯誤，請輸入正數');", true);
                e.Cancel = true;
            }
        }
    }
}