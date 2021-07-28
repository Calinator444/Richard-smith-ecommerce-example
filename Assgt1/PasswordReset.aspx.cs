using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Net.Mail;
using BuisinessLayer;
namespace Assgt1
{


    //the user can request a password reset here.
    public partial class PasswordReset : System.Web.UI.Page
    {
        BLobject BLayer;
        protected void Page_Load(object sender, EventArgs e)
        {
            successMessage.Visible = false;
            errorMessage.Visible = false;
            BLayer = new BLobject();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.UrlReferrer.ToString());
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            string tempPassword = Membership.GeneratePassword(7,0);
            string emailAddress = txtEmail.Text;
            if (BLayer.tempPassword(txtEmail.Text, tempPassword))
            {
                sendRecoveryEmail(tempPassword);
                successMessage.Visible = true;
            }
            else
                errorMessage.Visible = true;
        }
        protected void sendRecoveryEmail(string tempPassword)
        {
            string body = "hello user!\r\n" +
                         "your password has been temporarily changed to " + tempPassword+"\r\n\r\n" +
                         "please follow the link below to create a permanent password for yourself\r\n" +
                         "https://localhost:44396/PasswordChange.aspx?code="+tempPassword;

            EmailClient.sendEmail(new MailAddress(txtEmail.Text), "password reset", body);
        }
    }
}