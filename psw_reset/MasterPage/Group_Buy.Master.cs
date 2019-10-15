using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace psw_reset.MasterPage
{
    public partial class Group_Buy : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lab_name.Text = "歡迎您，同工-" + Session["User_Name"].ToString();
                    if (Session["User_ID"].ToString() == "6475" || Session["User_Dep_ID"].ToString()=="107")
                    {
                        list_lunch_management.Visible = true;
                    }

                    if (Session["User_Dep_ID"].ToString() == "104" ||
        Session["User_Dep_ID"].ToString() == "105" ||
        Session["User_Dep_ID"].ToString() == "106" ||
        Session["User_Dep_ID"].ToString() == "107" ||
        Session["User_Dep_ID"].ToString() == "180" ||
        Session["User_Dep_ID"].ToString() == "351" ||
        Session["User_Dep_ID"].ToString() == "100" ||
        Session["User_Dep_ID"].ToString() == "091" ||
        Session["User_Dep_ID"].ToString() == "092" ||
        Session["User_Dep_ID"].ToString() == "205" ||
        Session["User_Dep_ID"].ToString() == "182" ||
        Session["User_Dep_ID"].ToString() == "137" ||
        Session["User_Dep_ID"].ToString() == "138" ||
        Session["User_Dep_ID"].ToString() == "139" ||
        Session["User_Dep_ID"].ToString() == "333" ||
        Session["User_Dep_ID"].ToString() == "465" ||
        Session["User_Dep_ID"].ToString() == "368" ||
        Session["User_Dep_ID"].ToString() == "408" ||
        Session["User_Dep_ID"].ToString() == "460" ||
        Session["User_Dep_ID"].ToString() == "360" ||
        Session["User_Dep_ID"].ToString() == "183" ||
        Session["User_Dep_ID"].ToString() == "352" ||
        Session["User_Dep_ID"].ToString() == "505" ||
        Session["User_Dep_ID"].ToString() == "504" ||
        Session["User_Dep_ID"].ToString() == "177" ||
        Session["User_Dep_ID"].ToString() == "465" ||
        Session["User_Dep_ID"].ToString() == "366" ||
        Session["User_Dep_ID"].ToString() == "181" ||
        Session["User_Dep_ID"].ToString() == "446" ||
        Session["User_Dep_ID"].ToString() == "393" ||
        Session["User_Dep_ID"].ToString() == "178" ||
        Session["User_Dep_ID"].ToString() == "423" ||
        Session["User_Dep_ID"].ToString() == "424" ||
        Session["User_Dep_ID"].ToString() == "103" ||
        Session["User_Dep_ID"].ToString() == "152" ||
        Session["User_Dep_ID"].ToString() == "400" ||
        Session["User_Dep_ID"].ToString() == "093" ||
        Session["User_Dep_ID"].ToString() == "479")
                        {
                        lunch_list.Visible = true;
                        lunch_list2.Visible = true;
                    }
                }
                catch
                {
                    Response.Redirect("../GroupBuy/login.aspx");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["User_ID"] = null;
            Session["User_Name"] = null;
            Session["User_Mail"] = null;
            Session["User_Dep_ID"] = null;
            Session["User_Dep_Name"] = null;
            Session["Login"] = null;

            Response.Redirect("../GroupBuy/login.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("../GroupBuy/index.aspx");
        }
    }
}