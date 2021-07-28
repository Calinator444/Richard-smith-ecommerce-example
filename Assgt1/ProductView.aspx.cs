using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;

using DataLayer.DataModels;
using BuisinessLayer;

namespace Assgt1
{
    public partial class ProductView : System.Web.UI.Page
    {
        BLobject BLayer;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string productId = Request.QueryString["ProductID"];
                BLayer = new BLobject();
                //frm.DataSource = BLayer.getSingleProduct(productId);
                //DataTable dt = BLayer.getProductsBindable("Mice")
                //int ind = 5;
                //foreach (DataRow row in dt.Rows)
                //{
                    
                //    Debug.WriteLine(Convert.ToString(row["description"]));
                //    ind++;
                //}
                
                
                form.DataSource = BLayer.getSingleProduct(productId);
                form.DataBind();
            }
        }

        protected void cartview_Click(object sender, EventArgs e)
        {

            List < Item > items = (List < Item >) Session["Cart"];
            Response.Redirect("Cart.aspx");
            
        }


        protected void addtocart()
        {
           
            ////addtocart.co
            //List<Item> items = (List<Item>)Session["Cart"];
            //foreach (Item item in items)
            //{
                
            //    if (item.getID == btnAddToCart.CommandArgument)
            //        item.add();
            //        return;
            //}

        }
        protected void stuffstuff_DataBinding(object sender, EventArgs e)
        {
            
            FormView send = (FormView)sender;
            DataTable dt = (DataTable)send.DataSource;
            //Debug.WriteLine(Convert.ToString(send.Items.Count));
            Debug.WriteLine("stuffstuff was databound");
            foreach (DataRow row in dt.Rows)
            {

                Debug.WriteLine(Convert.ToString(row["description"]));
            }

        }
    }
}