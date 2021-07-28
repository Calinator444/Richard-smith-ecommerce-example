using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net.Mail;

using BuisinessLayer;
using System.Diagnostics;

namespace Assgt1
{
    public partial class AccountCreation : System.Web.UI.Page
    {
        BLobject BLayer;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            BLayer = new BLobject();
        }


        protected bool passwordsMatch()
        {

            return txtPassword.Text == txtPasswordConfirm.Text;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("Webform1.aspx");
        }




        protected bool passwordValid()
        {
            string pass = txtPassword.Text;
            //PassEnterErr.Visible = true;
            if (pass=="")
            {
                passValidate.Text = "You must enter a password";
                return false;
            }
            else if (!containsNumber(pass))
            {
                passValidate.Text = "Passwords must contain at least 1 number";
                return false;
            }
            else
                passValidate.Visible = false;
            return true;
        }




        //>mfw typing out like 5 if statements and calling it a program https://i.kym-cdn.com/photos/images/newsfeed/001/515/694/3b5.jpg



        protected bool validEmail()
        {
            //Author a.garg
            //Date accessed: 26/02/2021
            //Purpose: Email validator
            //source: https://www.netdeft.com/2015/09/custom-validator-server-side-validation.html

            //txtEmail.Text;
            string strRegex = "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}" + "\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\" + ".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$";
            Regex re = new Regex(strRegex);
            return re.IsMatch(txtEmail.Text);
            //e.IsValid = re.IsMatch(txtEmail.Text);
            // source.
        }

        protected bool containsNumber(string tststring)
        {
            bool hasNumber = false;
            for (int i = 0; i < 10; i++)
            {
                if (tststring.Contains(i.ToString()))
                    hasNumber = true;
            }
            return hasNumber;
        }


        //These validators are invisible because error messages are displayed in real time, making these messages redundant

        protected void passValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = passwordValid();
        }

        protected void emailValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtEmail.Text == "")
            {
                emailValidator.Text = "this field must not be blank";
                args.IsValid = false;
            }
            else
            {
                if (!validEmail())
                {
                    emailValidator.Text = "Please enter a valid email";
                    args.IsValid = validEmail();
                }
                else
                    args.IsValid = true;
            }

        }

        protected void passConfirmValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = passwordsMatch();
            //passErr.Text = passwordsMatch();
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //try
                //{

                //MailAddress usraddress = new MailAddress(txtEmail.Text);
                //MailAddress usrAddress = new MailAddress("caleb.williams5247@gmail.com");

                //MailAddress to = new MailAddress("caleb.williams5247@gmail.com");
                //MailAddress from = new MailAddress("richardsmithelectronics@gmail.com");
                //MailAddress copy = new MailAddress("Patrick.Wells@newcastle.edu.au");
                //MailMessage message = new MailMessage(from, to);

                ////MailAddress corporateAddress = new MailAddress("RichardSmithElectronics@gmail.com");
                //message.Subject = "Account Successfully Created";

                ////un-comment later on, probably, if I get around to it
                ////message.CC.Add("Patrick.Wells@newcastle.edu.au");

                ////a little letter of discouragement
                ////you know for security and stuff
                ////success.Body = "Congratulations! Your account was created successfully, \r\nUnless you're a robot, in which case you can p**s off";
                //message.Body = "You should really forward this to Caleb, he seems like a cool guy in my totally unbiased opinion.\r\n he can be reached at 'C3299204@uon.edu.au'";
                //SmtpClient tempClient = new SmtpClient("smtp.gmail.com", 587);
                //tempClient.EnableSsl = true;
                //tempClient.UseDefaultCredentials = false;
                //tempClient.Credentials = new System.Net.NetworkCredential("richardsmithelectronics@gmail.com","hardPass1");
                //tempClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //tempClient.Send(message);
                int accID = BLayer.addAccount(txtEmail.Text, txtPassword.Text);
                string body = "" +
                    "Congratulations! You're account was successfully created \r\n" +
                    "but you'll need to activate it before you can log in\r\n" +
                    "you can do so by visiting the link below: \r\n" +
                    "https://localhost:44396/UserLogin.aspx?activate="+Convert.ToString(accID);
                List<MailAddress> ccs = new List<MailAddress>();
                ccs.Add(new MailAddress("Patrick.Wells@newcastle.edu.au"));
                EmailClient.sendEmail(new MailAddress(txtEmail.Text), "Account activation", body);

                //}
                //catch
                //{
                //    Response.Redirect("ErrorPage.aspx");
                //}
                //int accID = BLayer.addAccount(txtEmail.Text, txtPassword.Text);
                Response.Redirect("UserLogin.aspx?AccountCreated=true");
            }

            }

        protected void checkExists_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (BLayer.checkAnyUserExists(txtEmail.Text))
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    }
}

