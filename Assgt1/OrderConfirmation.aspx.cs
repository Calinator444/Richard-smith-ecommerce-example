using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BuisinessLayer;
using System.Diagnostics;
using System.Configuration;
using DataLayer.DataModels;
//using Assgt1.DataObjects;
namespace Assgt1
{
    public partial class OrderConfirmation : System.Web.UI.Page
    {
        
        BLobject BLayer = new BLobject();
        string shipOption;
        //DataLayer.DataModels.ShippingOptions so;
        protected void Page_Load(object sender, EventArgs e)
        {
            shipOption = Request.QueryString["shippingOption"];
            //total = 0;
            if (!IsPostBack)
            {
                if (Session["currentuser"] == null)
                    errorMessage.Visible = true;
                //string shipOption = Request.QueryString["shippingOption"];
                //not sure why "shipping options" is an ambiguous reference but ok
                //so = BLayer.getShippingOption(Convert.ToInt32(shipOption));
                buildDataSource();
            }
        }

        protected void btnPlace_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                double total = 0;
                //BLayer.getProduct
                foreach (Item item in (List<Item>)Session["Cart"])
                {
                    Product product = BLayer.getProduct(item.getID());
                    total += item.getQuantity() * product.price;
                }
                        //check the user's actually logged in, they can still place orders if they aren't though, they'll just have their accountID set to null
                int orderID;
                int getShipOption = Convert.ToInt32(shipOption);
                DataLayer.DataModels.ShippingOptions getShipping = BLayer.getShippingOption(getShipOption);
                total += getShipping.cost;
                string email = "";
                if (Session["CurrentUser"] != null)
                    orderID = BLayer.placeOrder(getShipOption, (int)Session["CurrentUser"], total);
                else
                {
                    orderID = BLayer.placeOrder(getShipOption, total);
                    email = "&email=" + txtEmail.Text;
                }
                BLayer.addOrderItems((List<Item>)Session["Cart"], orderID);
                //string redirect = "SuccessPage.aspx?orderID=" + orderID
                //if()
                Response.Redirect("SuccessPage.aspx?orderID=" + orderID + email);
            }
        }

        protected void buildDataSource()
        {
                double total = 0;
                List<Item> getItems = (List<Item>)Session["Cart"];
                //bool hasItems = getItems.Count > 0;

                DataTable dt = new DataTable();
                dt.Columns.Add("Model");
                //dt.Columns.Add("Description");
                dt.Columns.Add("Quantity");
                //dt.Columns.Add("Image");
                dt.Columns.Add("Price");
                dt.Columns.Add("ID");


                foreach (Item item in getItems)
                {

                    Product p = BLayer.getProduct(item.getID());
                    DataRow dr = dt.NewRow();
                    //y u godda b liek dis
                    dr[0] = p.modelName;
                    //dr[1] = p.description;
                    dr[1] = item.getQuantity();
                    //dr[3] = p.previewImage;
                    double price = p.price * item.getQuantity();
                    total += price;
                    dr[2] = price;
                    dr[3] = item.getID(); //p.productId;
                    dt.Rows.Add(dr);

                }
                DataLayer.DataModels.ShippingOptions so = BLayer.getShippingOption(Convert.ToInt32(shipOption));
                orderItems.DataSource = dt;
                orderItems.DataBind();
                total += so.cost;
                shipCost.Text = "Shipping :$"+Convert.ToString(so.cost);
                totalCost.Text = "Total :$"+ Convert.ToString(total);
                //lblTotal.Text = total.ToString();
            }

        protected void Unnamed_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Session["currentuser"] != null)
                args.IsValid = true;
            else
                args.IsValid = EmailValidator.CheckValid(txtEmail.Text);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            Button sendbtn = (Button)sender;
            List<Item> items = (List<Item>)Session["Cart"];
            foreach(Item item in items)
            {
                if (item.getID() == Convert.ToInt32(sendbtn.CommandArgument))
                    items.Remove(item);
                    Session["Cart"] = items;
                    return;
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            List<Item> items = (List<Item>)Session["Cart"];
            string commandarg = tb.Attributes["CommandArgument"].ToString();
            int icommandarg = Convert.ToInt32(commandarg);
            foreach (Item item in items)
            {
                tb.Attributes["CommandArgument"].ToString();
                if (item.getID() == icommandarg)
                    item.setQuantity(Convert.ToInt32(tb.Text));
                    Session["cart"] = items;
            }
        }
        //else
        //{
        //    emptyError.Visible = true;
        //    CartMenu.Visible = false;
        //}


    }
    //protected DataTable convertData 
    //protected void purchase_Click(object sender, EventArgs e)
    //{
    //    //don't click this yet

    //    //it'll break some stuff

    //    Response.Redirect("PaymentDetails.aspx");
    //    //ConfigurationManager.AppSettings["Securepath"]+"
    //}

    //protected void cartItems_DataBinding(object sender, EventArgs e)
    //{
    //    Debug.WriteLine("cartItems was databound");
    //}

    //protected void Unnamed_Click(object sender, EventArgs e)
    //{
    //    Button btn = (Button)sender;
    //    int ID = Convert.ToInt32(btn.CommandArgument);
    //    List<Item> cartItems = (List<Item>)Session["Cart"];
    //    foreach(Item item in cartItems)
    //    {
    //        if (item.getID() == ID)
    //        {

    //            cartItems.Remove(item);
    //            //prevents recursive function from executing if item is removed
    //            Session["Cart"] = cartItems;
    //            buildDataSource();
    //            return;
    //        }
    //    }
    //}

    //protected void txtQty_TextChanged(object sender, EventArgs e)
    //{
    //    TextBox getText = (TextBox)sender;
    //    int newQuantity = Convert.ToInt32(getText.Text);

    //    //quantity should be at least 1 or everythinng gets buggered up
    //    if (Convert.ToInt32(getText.Text) < 1)
    //        newQuantity = 1;


    //    //so TextBoxes have no "CommandArgument" attribute for whatever reason
    //    string ID = getText.Attributes["CommandArgument"].ToString();

    //    //int newQty = Convert.ToInt32(getText.Text);
    //    List<Item> getItems = (List<Item>)Session["Cart"];
    //    foreach(Item item in getItems)
    //    {
    //        if (item.getID() == Convert.ToInt32(ID))
    //        {
    //            item.setQuantity(newQuantity);
    //            Session["Cart"] = getItems;
    //            buildDataSource();
    //            //return so we arent doing redundant checks of each item in the for loop
    //            return;
    //        }
    //    }


    //}

    
    //}

}