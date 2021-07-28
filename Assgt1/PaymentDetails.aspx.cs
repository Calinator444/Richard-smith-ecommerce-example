using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Helper;
using System.Web.UI;
using BuisinessLayer;
using DataLayer.DataModels;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Diagnostics;
namespace Assgt1
{
    public partial class PaymentDetails : System.Web.UI.Page
    {
        BLobject BLayer;

       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
                Response.Redirect("PaymentDetails.aspx");
            BLayer = new BLobject();
            
            

                
            
            
                if (!IsPostBack)
                {

                //we put the binding here so that the list doesn't bind every time the page reloads
                shipOptions.DataSource = BLayer.getShippingOptions();
                shipOptions.DataBind();
                //checks if the user is logged in
                if (Session["CurrentUser"] != null)
                {
                    List<Address> addresses = BLayer.getAddressByUser((int)Session["CurrentUser"]);

                    foreach (Address address in addresses)
                    {
                        if (address.billingAddress)
                        {
                            billCode.Text = Convert.ToString(address.postCode);
                            billStreet.Text = address.street;
                            billStreetNo.Text = Convert.ToString(address.unit);
                            billCountry.Text = address.country;
                            billSuburb.Text = address.suburb;
                        }
                        else
                        {
                            shipCode.Text = Convert.ToString(address.postCode);
                            shipStreet.Text = address.street;
                            shipStreetno.Text = Convert.ToString(address.unit);
                            shipCountry.Text = address.country;
                            shipSuburb.Text = address.suburb;
                        }
                    }
                    saveShipping.Visible = true;
                }
                else
                    {
                        errorpreamble.Visible = true;
                        //saveShipping.Visible = true;
                    }
                }
                //bool blogged = (bool) Session["logStatus"];
                //errorpreamble.Visible = !blogged;
                //saveShipping.Enabled = blogged;
        

        }

        protected void useAsBilling_CheckedChanged(object sender, EventArgs e)
        {
            billStreet.Enabled = !useAsBilling.Checked;
            billStreetNo.Enabled = !useAsBilling.Checked;
            billCode.Enabled = !useAsBilling.Checked;
            billCountry.Enabled = !useAsBilling.Checked;
            billSuburb.Enabled = !useAsBilling.Checked;
        }



        //we need the country to query the API
        protected bool PostCodeValid(TextBox txtCode, TextBox txtCountry)
        {
            AddressValidator addValid = new AddressValidator();
            return addValid.queryPostCode(txtCode.Text, txtCountry.Text);


        }
        protected void shipAddressValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool countryExists = countryValid(shipCountry.Text);
            bool validPostCode = PostCodeValid(shipCode, shipCountry);
            bool streetfilled = shipStreet.Text.Length > 0;
            bool streetNoFilled = shipStreetno.Text.Length > 0;

            if(countryExists && validPostCode && streetfilled && streetNoFilled)
            {
                args.IsValid = true;
            }
                else
                { 

                args.IsValid = countryValid(shipCountry.Text);
                if (!countryExists)
                {
                    shipAddressValid.Text = "Please enter a valid country";
                    args.IsValid = false;
                }
                else if(!validPostCode)
                {
                    shipAddressValid.Text = "The post code you entered was not valid for your region";
                    args.IsValid = false;
                }
                if(!streetNoFilled)
                {
                    shipAddressValid.Text = "Please enter your street number";
                args.IsValid = false;
                }

            if (!streetfilled)
            {
                    shipAddressValid.Text = "Please enter a valid street number";
                args.IsValid = false;
            }
            }

        }


        
        
        protected bool countryValid(string countryName)
        {

            //using a class here for validation so I don't need to copy and paste the country list every time I need to check if a country is valid
            CountryValidator tempValid = new CountryValidator();
            return tempValid.checkCountry(countryName);
        }

        protected void billAddressValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (useAsBilling.Checked)
                args.IsValid = true;
            else
            { 
            bool countryExists = countryValid(billCountry.Text);
            bool validPostCode = PostCodeValid(billCode, shipCountry);
            bool streetfilled = shipStreet.Text.Length > 0;
            bool streetNoFilled = shipStreetno.Text.Length > 0;

            if (countryExists && validPostCode && streetfilled && streetNoFilled)
            {
                args.IsValid = true;
            }
            else
            {

                args.IsValid = countryValid(shipCountry.Text);
                if (!countryExists)
                {
                    billAddressValid.Text = "Please enter a valid country";
                    args.IsValid = false;
                }
                else if (!validPostCode)
                {
                    billAddressValid.Text = "The post code you entered was not valid for your region";
                    args.IsValid = false;
                }
                if (!streetNoFilled)
                {
                        billAddressValid.Text = "Please enter your street number";
                    args.IsValid = false;
                }

                if (!streetfilled)
                {
                        billAddressValid.Text = "Please enter a valid street number";
                    args.IsValid = false;
                }
            }

            }
        }

        protected void CreditCardValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool valNumber = CreditCardNo.Text.Length > 13 && CreditCardNo.Text.Length < 17;

            bool valccv = txtCCV.Text.Length > 2 && txtCCV.Text.Length < 5;
            bool valDate = txtExpiry.Text.Length == 4;

            if (valNumber && valccv && valDate)
            {
                string tempstring = txtExpiry.Text.Substring(0, 2);
                int test = Convert.ToInt32(tempstring);
                if (test < 13 && test > 0)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = false;
            }
            
        }

        protected Address createBillingAddress()
        {
            Address billingAddress = new Address();
            billingAddress.billingAddress = true;
            billingAddress.country = billCountry.Text;
            billingAddress.postCode = Convert.ToInt32(shipCode.Text);
            billingAddress.suburb = billSuburb.Text;
            billingAddress.unit = Convert.ToInt32(billStreetNo.Text);
            billingAddress.street = billStreet.Text;
            return billingAddress;
        }
        protected Address createShippingAddress()
        {
            Address shippingAddress = new Address();
            shippingAddress.billingAddress = false;
            shippingAddress.country = shipCountry.Text;
            shippingAddress.postCode = Convert.ToInt32(shipCode.Text);
            shippingAddress.suburb = shipSuburb.Text;
            shippingAddress.unit = Convert.ToInt32(shipStreetno.Text);
            shippingAddress.street = shipStreet.Text;
            return shippingAddress;
        }

        protected void confirmOrder_Click(object sender, EventArgs e)
        {
            //if (BLayer.getAddressByUser((int)Session["CurrentUser"]).Count == 0)
            //    Debug.WriteLine("");

            if(Page.IsValid)
            {
                //user can only check save shipping if they're logged in, otherwise the control isn't visible\
                
                if (saveShipping.Checked)
                {
                    //check whether or not the user has addressses first
                    List<Address> Addresses = BLayer.getAddressByUser((int)Session["CurrentUser"]);
                    int usr = (int)Session["CurrentUser"];
                    if (Addresses.Count == 0)
                    {
                        Address shippingAddress = createShippingAddress();
                        if (useAsBilling.Checked)
                            BLayer.addAddresses(shippingAddress, usr);
                        else
                        {
                            Address billingAddress = createBillingAddress();
                            BLayer.addAddresses(billingAddress, shippingAddress, usr);
                        }
                    }
                    else
                    {
                        int billingID = 0;
                        int shippingID = 0;
                        foreach(Address address in Addresses)
                        {
                            if (address.billingAddress)
                                billingID = address.addressID;
                            else
                                shippingID = address.addressID;
                        }
                        BLayer.updateAddresses(shippingID, createShippingAddress(), billingID, createBillingAddress());
                    }
                }

                Response.Redirect("OrderConfirmation.aspx?shippingOption=" + shipOptions.SelectedValue);

            }
        }

        protected void shipOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList getSender = (DropDownList)sender;

            Debug.WriteLine(getSender.SelectedValue);
        }
    }
}