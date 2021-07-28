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
    public partial class Cart : System.Web.UI.Page
    {
        double total;
        BLobject BLayer = new BLobject();
        protected void Page_Load(object sender, EventArgs e)
        {
            total = 0;
            if (!IsPostBack)
                buildDataSource();
        }

        
        protected void buildDataSource()
        {
            
            List<Item> getItems = (List<Item>)Session["Cart"];
            bool hasItems = getItems.Count > 0;
            if (hasItems)
            {
                CartMenu.Visible = true;
                emptyError.Visible = false;
                DataTable dt = new DataTable();
                dt.Columns.Add("Model");
                dt.Columns.Add("Description");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Image");
                dt.Columns.Add("Price");
                dt.Columns.Add("ID");


                foreach (Item item in getItems)
                {

                    Product p = BLayer.getProduct(item.getID());
                    DataRow dr = dt.NewRow();
                    //y u godda b liek dis
                    dr[0] = p.modelName;
                    dr[1] = p.description;
                    dr[2] = item.getQuantity();
                    dr[3] = p.previewImage;
                    double price = p.price * item.getQuantity();
                    total += price;
                    dr[4] = price;
                    dr[5] = item.getID(); //p.productId;
                    dt.Rows.Add(dr);

                }
                cartItems.DataSource = dt;
                cartItems.DataBind();
                lblTotal.Text = total.ToString();
            }
            else
            {
                emptyError.Visible = true;
                CartMenu.Visible = false;
            }
            

        }
       //protected DataTable convertData 
        protected void purchase_Click(object sender, EventArgs e)
        {
            //don't click this yet

            //it'll break some stuff
            
            Response.Redirect("PaymentDetails.aspx");
            //ConfigurationManager.AppSettings["Securepath"]+"
        }

        protected void cartItems_DataBinding(object sender, EventArgs e)
        {
            Debug.WriteLine("cartItems was databound");
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int ID = Convert.ToInt32(btn.CommandArgument);
            List<Item> cartItems = (List<Item>)Session["Cart"];
            foreach(Item item in cartItems)
            {
                if (item.getID() == ID)
                {
                    
                    cartItems.Remove(item);
                    //prevents recursive function from executing if item is removed
                    Session["Cart"] = cartItems;
                    buildDataSource();
                    return;
                }
            }

        }

        protected void Unnamed_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            TextBox getText = (TextBox)sender;
            int newQuantity = Convert.ToInt32(getText.Text);

            //quantity should be at least 1 or everythinng gets buggered up
            if (Convert.ToInt32(getText.Text) < 1)
                newQuantity = 1;


            //so TextBoxes have no "CommandArgument" attribute for whatever reason
            string ID = getText.Attributes["CommandArgument"].ToString();
            
            //int newQty = Convert.ToInt32(getText.Text);
            List<Item> getItems = (List<Item>)Session["Cart"];
            foreach(Item item in getItems)
            {
                if (item.getID() == Convert.ToInt32(ID))
                {
                    item.setQuantity(newQuantity);
                    Session["Cart"] = getItems;
                    buildDataSource();
                    //return so we arent doing redundant checks of each item in the for loop
                    return;
                }
            }
            

        }
    }
}