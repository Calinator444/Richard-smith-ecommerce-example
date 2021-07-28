using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using BuisinessLayer;
using System.Web.UI.WebControls;

namespace Assgt1
{
    
    public partial class PasswordChange : System.Web.UI.Page
    {
        BLobject Blayer;
        string currentPassword;
        protected void Page_Load(object sender, EventArgs e)
        {

            //only display messages until the program posts back
            errorMessage.Visible = false;
            successMessage.Visible = false;

            //I'm calling the password "code" so that spies won't assume the url variabl is the user's password
            currentPassword = Request.QueryString["code"];
            //arguably I should've made the BLobject a static class so I wouldn't have to keep doing this but it's getting late
            Blayer = new BLobject();
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                if (Blayer.updatePassword(currentPassword, enterpassword.Text))
                    successMessage.Visible = true;
                else
                    errorMessage.Visible = true;


            }
        }

        protected void Unnamed_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (enterpassword.Text == txtConfirm.Text)
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void passwordValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool valid = false;
            for (int i = 0; i < 10; i++)
            {
                if (enterpassword.Text.Contains(i.ToString()))
                {
                    valid = true;
                }
            }
            args.IsValid = valid;
        }
    }
}