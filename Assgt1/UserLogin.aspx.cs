using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DataLayer.DataModels;
using System.Web.UI.WebControls;
using System.Diagnostics;
using BuisinessLayer;
using System.IO;
using System.Drawing;
using System.Data;
using DataLayer.DataModels;
namespace Assgt1
{
    public partial class UserLogin : System.Web.UI.Page
    {
        BLobject BLayer = new BLobject();

        protected void accountCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccountCreation.aspx");
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "admin@uon.edu.au";
            txtPassword.Text = "hardPass1";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //DataLayer.

            //whenever users logout they get redirected here, so it's as good a time as any to reset all the session variables
            Session.Clear();
            if (!IsPostBack)
            {
                if (Session["Cart"] == null)
                    Session["Cart"] = new List<Item>();
                if (Request.QueryString["AccountCreated"] != null)
                    accountSuccess.Visible = Convert.ToBoolean(Request.QueryString["AccountCreated"]);

                if(Request.QueryString["activate"] != null)
                {
                    BLayer.activateAccount(Convert.ToInt32(Request.QueryString["activate"]));
                    accountActivateSuccess.Visible = true;
                }
                //Session.Clear();
                
            }
            if(IsPostBack)
            {
                //I want these controls to be visible only until the page refreshes
                accountSuccess.Visible = false;
                accountActivateSuccess.Visible = false;
            }


        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            
            if (Page.IsValid)
            {
                //Account current = (Account)Session["CurrentUser"];

                //make sure the account is listed as active
                int usrID = (int)Session["CurrentUser"];
                if (BLayer.checkAdmin(usrID))
                    Response.Redirect("AdminSite/AccountManagement.aspx");
                else
                    Response.Redirect("Home.aspx"); 
            }
        }


        protected void LoginValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int checkUsr = BLayer.CheckUser(txtUsername.Text, txtPassword.Text);
            if (checkUsr > 1)
            {
                //stores the AccountID as a session variable
                Session["CurrentUser"] = checkUsr;
                args.IsValid = true;
                
                
            }
            else
            {
                switch (checkUsr)
                {
                    //user not found
                    case 0:
                        {
                            LoginValidate.ErrorMessage = "Username was not found";
                            args.IsValid = false;
                            
                            break;
                        }
                    case 1:
                        {
                            LoginValidate.ErrorMessage = "Password was incorrect";
                            args.IsValid = false;
                            break;
                        }
                    default:
                        {
                            //this statement is just here so the code compiles
                            return;
                        }
                }
            }
        }

        protected void resetPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("PasswordReset.aspx");

        }


    }
}