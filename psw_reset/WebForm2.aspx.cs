using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace psw_reset
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool add_chk=false;
            string user_list = "";
            String o = txb_o.Text.Trim();
            String add = txb_add.Text.Trim();
            String less = txb_less.Text.Trim();
            String[] o_substrings = o.Split(',');
            String[] add_substrings = add.Split(',');
            String[] less_substrings = less.Split(',');
            int o_leg = o_substrings.Length;

           for (int i=0;i <add_substrings.Length;i++)
            {
               for(int j=0;j<o_leg;j++)
                {
                    if (add_substrings[i] == o_substrings[j])
                    {
                        add_chk = true;
                        break;
                    }
                    else
                    {
                        add_chk = false;
                    }
                }

                if (add_chk == false)
                {
                    System.Array.Resize(ref o_substrings, o_substrings.Length + 1);
                    o_substrings[o_substrings.Length - 1] = add_substrings[i];
                }
            }
            for (int i = 0; i < less_substrings.Length; i++)
            {
                o_substrings = o_substrings.Where(val => val != less_substrings[i]).ToArray();
            }

            for (int i = 0; i < o_substrings.Length; i++)
            {
                if (user_list == "")
                {
                    user_list = o_substrings[i];
                }

                else
                {
                    user_list=user_list+","+ o_substrings[i];
                }
            }
            Response.Write(user_list);
        }
    }
}