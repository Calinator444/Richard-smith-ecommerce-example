using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data;
using DataLayer.DataModels;
using BuisinessLayer;

namespace Assgt1
{
    public partial class AccountManagement : System.Web.UI.Page
    {
        //DataAccess daUsers;
        //DataAccess daOrders;
        //DataAccess daOrderItems;
        BLobject BLayer;
        protected void Page_Load(object sender, EventArgs e)
        {

            txtSuccess.Visible = false;

            //Page_Load is called whenever a control (i.e. the dropdown list) posts back to the server
            //this check ensures that databindings are not reset when the control updates
            //Response.Write();

            
            BLayer = new BLobject();
            if (!IsPostBack)
            {

                //showAccountDetails();
                drpUsers.DataSource = BLayer.getBindable("Accounts");
                drpUsers.DataTextField = "emailAddress";
                //drpUsers.SelectedValue = "select user";
                //knowing this field existed would've saved me a lot of hassle for part 1
                drpUsers.DataValueField = "accountID";
                drpUsers.DataBind();
                drpUsers.Items.Add("Select User");
                drpUsers.SelectedValue = "Select User";
                
                //showAccountDetails();
                //showAddress();
                //drpUsers.Items.Add("Select User");
                //drpUsers.SelectedValue("Select User");
            }
            //showAccountDetails();
            //showAddress();
        }


        protected void dataBindControls()
        {
            //DataTable tempBindings = daUsers.queryTable("emailaddress = " + "'" + allUsers.Text + "'");
            //bindotherstuff.DataSource = tempBindings;
            //bindotherstuff.DataBind();
        }
        protected void allUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(drpUsers.SelectedValue == "Select User"))
            {
                DropDownList getSender = (DropDownList)sender;
                showAccountDetails();
                showAddress();

                OrderList.DataSource = BLayer.getUserOrders(Convert.ToInt32(getSender.SelectedValue));
                OrderList.DataBind();
            }

        }

        public void showAccountDetails()
        {

            //DropDownList dl = (DropDownList)sender;
            int ID = Convert.ToInt32(drpUsers.SelectedValue);
            Account acc = BLayer.getAccountObject(ID);
            txtEmail.Text = acc.emailAddress;
            txtPass.Text = acc.password;
            chkAdmin.Checked = acc.adminPriveleges;
            chckActive.Checked = acc.active;
        }


        public Address getShippingAddress()
        {
            Address shippingAddress = new Address();
            shippingAddress.postCode = Convert.ToInt32(ShipPostCode.Text);
            shippingAddress.country = ShipCountry.Text;
            shippingAddress.street = ShipStreet.Text;
            shippingAddress.suburb = ShipSuburb.Text;
            shippingAddress.unit = Convert.ToInt32(ShipNo.Text);
            shippingAddress.billingAddress = false;
            return shippingAddress;
        }



        public void showAddress()
        {
            int accountID = Convert.ToInt32(drpUsers.SelectedValue);
            List<Address> myAddresses = BLayer.getAddressByUser(accountID);
            if (myAddresses.Count < 1)
            {
                BillPostCode.Text = "";

                BillCountry.Text = "";
                BillStreet.Text = "";
                BillSuburb.Text = "";
                BillNo.Text = "";

                ShipPostCode.Text = "";
                ShipCountry.Text = "";
                ShipStreet.Text = "";
                ShipSuburb.Text = "";
                ShipNo.Text = "";


                //indicates that no adddress has been set up yet
                Session["BillingID"] = 0;
                Session["AddressID"] = 0;
                updateAddresses.Text = "Add Address";
            }
            else
            {
                updateAddresses.Text = "Update Address";
            }

            foreach (Address add in myAddresses)
            {
                if (add.billingAddress)
                {
                    BillPostCode.Text = add.postCode.ToString();
                    BillCountry.Text = add.country;
                    BillStreet.Text = add.street;
                    BillSuburb.Text = add.suburb;
                    BillNo.Text = add.unit.ToString();

                    Session["BillingID"] = add.addressID;
                    //BillPostCode.
                }
                //do stuff
                else
                {
                    ShipPostCode.Text = add.postCode.ToString();
                    ShipCountry.Text = add.country;
                    ShipStreet.Text = add.street;
                    ShipSuburb.Text = add.suburb;
                    ShipNo.Text = add.unit.ToString();
                    Session["AddressID"] = add.addressID;


                }

            }
            Debug.Write(Convert.ToString(Session["ShippingID"]));
        }

        protected void viewObject_Click(object sender, EventArgs e)
        {
            Button sendButton = (Button)sender;
            DataTable dt = BLayer.getOrderItemsBindable(Convert.ToInt32(sendButton.CommandArgument));
            orderitems.DataSource = dt;
            orderitems.DataBind();
         
        }

        protected void updateAccount_Click(object sender, EventArgs e)
        {
            if (drpUsers.SelectedValue == "Select User")
                return;
            int ID = Convert.ToInt32(drpUsers.SelectedValue);
            int active = Convert.ToInt32(chckActive.Checked);
            int admin = Convert.ToInt32(chkAdmin.Checked);
            txtSuccess.Visible = BLayer.alterAccount(ID, txtPass.Text, txtEmail.Text, active, admin);


        }

        protected void chkUseAsBilling_CheckedChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("use as billing was fired");
            CheckBox check = (CheckBox)sender;
            bool bChecked = !check.Checked;

            BillCountry.Enabled = bChecked;
            billCountryValid.Enabled = bChecked;

            BillNo.Enabled = bChecked;
            billNoValid.Enabled = bChecked;

            BillPostCode.Enabled = bChecked;
            BillPostCodeValid.Enabled = bChecked;

            BillStreet.Enabled = bChecked;
            billStreetValid.Enabled = bChecked;

            BillSuburb.Enabled = bChecked;
            billSuburbValid.Enabled = bChecked;


        }

        public Address getBillingAddress()
        {
            Address billingAddress = new Address();
            billingAddress.postCode = Convert.ToInt32(BillPostCode.Text);
            billingAddress.country = BillCountry.Text;
            billingAddress.street = BillStreet.Text;
            billingAddress.suburb = BillSuburb.Text;
            billingAddress.unit = Convert.ToInt32(BillNo.Text);
            billingAddress.billingAddress = true;
            return billingAddress;
        }

        public void insertAddresses()
        {
            int usrID = Convert.ToInt32(drpUsers.SelectedValue);
            Address shippingAddress = getShippingAddress();
            if (chkUseAsBilling.Checked)
                BLayer.addAddresses(shippingAddress, usrID);
            else
            {

                Address billingAddress = getBillingAddress();
                BLayer.addAddresses(billingAddress, shippingAddress, usrID);
            }
        }


        protected void updateAddresses_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                int BillingID = (int) Session["BillingID"];
                int ShippingID = (int) Session["AddressID"];

                //address session variables are both set to zero when the page first loads
                //if any address records are found when the database is queried with the user's id, these names are changed
                if (BillingID == 0 && ShippingID == 0)
                    insertAddresses();
                else
                    BLayer.updateAddresses(ShippingID, getShippingAddress(), BillingID, getBillingAddress());
            }
        }


    }
}

