using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using BuisinessLayer;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace Assgt1
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        BLobject BLayer;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLayer = new BLobject();
            if(!IsPostBack)
            {
                orders.DataSource = BLayer.getUserOrders((int)Session["CurrentUser"]);
                orders.DataBind();
                Debug.WriteLine("ORDER COUNT" + Convert.ToString(orders.Items.Count));

                //display the error message if the user hasn't made any purchases
           
            }
        }

        protected void viewOrder_Click(object sender, EventArgs e)
        {
            Button send = (Button)sender;
            
            orderItems.DataSource = BLayer.getOrderItemsBindable(Convert.ToInt32(send.CommandArgument));
            orderItems.DataBind();
        }
    }
}