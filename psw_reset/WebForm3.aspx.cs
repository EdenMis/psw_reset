using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace psw_reset
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String userName = "npoistest@eden.org.tw";
            String password = "25852106";
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress("<eden1321@eden.org.tw>,<eden6475@eden.org.tw>"));
            msg.From = new MailAddress(userName);
            msg.Subject = "SMTP測試";
            msg.Body = "SMTP測試";
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.office365.com";
            client.Credentials = new System.Net.NetworkCredential(userName, password);
            client.Port = 587;
            client.EnableSsl = true;
            client.Send(msg);
        }
    }
}