using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace psw_reset.GroupBuy
{
    public partial class Group_store_management : System.Web.UI.Page
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

            GridView1.DataBind();
            GridView2.DataBind();
            if (sender.GetType() != typeof(BulletedList) && TextBox1.Text != "0")
            {
                TextBox1.Text = TextBox1.Text == "" ? "0" : TextBox1.Text;
                tabchange(int.Parse(TextBox1.Text));
            }
        }

        protected void BulletedList1_Click(object sender, BulletedListEventArgs e)
        {
            TextBox1.Text = e.Index.ToString();
            tabchange(e.Index);
        }

        public void tabchange(int index)
        {
            BulletedList2.Items[index].Attributes.Add("class", "active");
            List<Panel> panel = new List<Panel>()
            {
                 tabe1,
                 tabe2,
            };
            List<ListItem> bl_item = new List<ListItem>()
            {
                BulletedList2.Items[0],
                BulletedList2.Items[1]
            };

            bl_item.Remove(BulletedList2.Items[index]);
            switch (index)
            {
                case 0:
                    tabe1.CssClass = "tab-pane fade active in";
                    panel.Remove(tabe1);
                    GridView1.DataBind();
                    break;
                case 1:
                    tabe2.CssClass = "tab-pane fade active in";
                    panel.Remove(tabe2);
                    GridView2.DataBind();
                    break;
            }

            for (int i = 0; i < panel.Count; i++)
            {
                panel[i].CssClass = "tab-pane fade";
                bl_item[i].Attributes.Add("class", "");
            }
        }
    }
}