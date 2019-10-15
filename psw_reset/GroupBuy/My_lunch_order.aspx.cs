using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace psw_reset.GroupBuy
{
    public partial class My_lunch_order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int i;
            for (i = 0; i < GridView2.Rows.Count; i++)
            {
                if (GridView2.Rows[i].Cells[1].Text == "未支付")
                {
                    GridView2.Rows[i].Cells[1].ForeColor = System.Drawing.Color.Red;
                }

                if (GridView2.Rows[i].Cells[1].Text == "已支付")
                {
                    GridView2.Rows[i].Cells[1].ForeColor = System.Drawing.Color.Blue;
                }

                if (GridView2.Rows[i].Cells[8].Text != "尚未結單" || GridView2.Rows[i].Cells[1].Text == "已支付")
                {
                    GridView2.Rows[i].FindControl("btn_delete").Visible = false;
                }
            }
        }
    }
}