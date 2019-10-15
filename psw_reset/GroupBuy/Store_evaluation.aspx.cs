using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace psw_reset.GroupBuy
{
    public partial class Store_evaluation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GridView1.Rows.Count == 0)
                {
                    Label1.Visible = true;
                }
            }
        }
    }
}