using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
//I misspelt business lol
using BuisinessLayer;
namespace Assgt1
{


    public partial class ShippingOptions : System.Web.UI.Page
    {
        BLobject BLayer;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //ensure the message only displays until the page posts back
            itemadded.Visible = false;


            //shippingItems = new DataAccess("DataObjects/ShippingOptions.csv");
            //shippingTable.DataSource = shippingItems.returnAll();
            BLayer = new BLobject();
            if (!IsPostBack)
            {
                shippingTable.DataSource = BLayer.getShippingOptions();
                shippingTable.DataBind();
            }
        }
        protected void toggleFormMode()
        {
            viewShipOptions.Visible = !viewShipOptions.Visible;
            addItem.Visible = !addItem.Visible;
            submitItem.Visible = !submitItem.Visible;


        }
        protected void cost_TextChanged(object sender, EventArgs e)
        {
            TextBox getBox = (TextBox)sender;

            //only 2 decimal places here cobba >:^(
            double roundedVal = Convert.ToDouble(getBox.Text);
            roundedVal = Math.Round(roundedVal, 2);
            if (roundedVal < 0)
                roundedVal = 0;
            getBox.Text = Convert.ToString(roundedVal);
        }

        protected void addOption_Click(object sender, EventArgs e)
        {
            //
            toggleFormMode();
        }

        protected void period_TextChanged(object sender, EventArgs e)
        {
            TextBox getText = (TextBox)sender;

            //just gonna truncate anything after the decimal place here

            int iOut = (int) Convert.ToInt32(getText.Text);
            if (iOut < 0)
                iOut = 0;
            getText.Text = Convert.ToString(iOut);
            
        }

        protected void cancelAdd_Click(object sender, EventArgs e)
        {
            toggleFormMode();
            txtDescription.Text = "";
            cost.Text = "0";
            period.Text = "0";
        }

        protected void formValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Color validColor = ColorTranslator.FromHtml("#ced4da");
            string errMessage = "";

            if(txtDescription.Text == "" && period.Text == "0" || cost.Text == "0")
            {
                args.IsValid = false;
            if (txtDescription.Text == "")
            { 
                txtDescription.BorderColor = Color.Red;
                errMessage = "Each shipping option requires a description";
                //args.IsValid = false;
            }

            else
                txtDescription.BorderColor = validColor;
            if (period.Text == "0")
            {
                period.BorderColor = Color.Red;
                errMessage = "Please enter a shipping period for the option you've provided";
                //args.IsValid = false;
            }
            else
                period.BorderColor = validColor;

                if (cost.Text == "0")
                {
                    cost.BorderColor = Color.Red;
                    errMessage = "Please enter the cost of your proposed shipping solution";

                }
                else
                    cost.BorderColor = validColor;
                formValidate.Text = errMessage;
                }
                else
                    args.IsValid = true;


        }

        protected void submitItem_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BLayer.addShippingOption(Convert.ToInt32(period.Text), Convert.ToDouble(cost.Text), txtName.Text, txtDescription.Text);
                itemadded.Visible = true;
                shippingTable.DataSource = BLayer.getShippingOptions();
                shippingTable.DataBind();
                toggleFormMode();
                
            }
        }

        
    }
}