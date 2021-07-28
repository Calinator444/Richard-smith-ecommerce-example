using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer.DataModels;
using BuisinessLayer;
using System.Net.Mail;

namespace Assgt1
{
    public partial class SuccessPage : System.Web.UI.Page
    {
        string orderID;
        BLobject BLayer;
        DataLayer.DataModels.ShippingOptions so;



        //the user is redirected here upon placing a successful order
        protected void Page_Load(object sender, EventArgs e)
        {
            BLayer = new BLobject();
            orderID = Request.QueryString["orderID"];

            so = BLayer.getShippingOption(Convert.ToInt32(orderID));
            BLayer = new BLobject();
            if (!IsPostBack)
            {
                //so can only be referenced until the page posts back, but we only need it for the invoice
                so = BLayer.getShippingOption(Convert.ToInt32(orderID));
                sendEmail();
                
            }



        }

        protected void sendEmail()
        {
            //MailAddress usrAddress = new MailAddress("caleb.williams5247@gmail.com");
            
            string emailString;
            if (Request.QueryString["email"] == null)
            {
                Account acc = BLayer.getAccountObject((int)Session["currentuser"]);
                emailString = acc.emailAddress;
                
            }
            else
                emailString = Request.QueryString["email"];
            MailAddress to = new MailAddress(emailString);


            EmailClient.sendEmail(to, "receipt for" + orderID, returnInvoice());

            //message.Body = returnInvoice();
 
        }
        protected string returnInvoice()
        {
            string outString = "" +
            "===============================================================\r\n" +
            "   INVOICE FOR ORDER #" +orderID+"\r\n"+
            "===============================================================\r\n";
            double total = 0;
            double totalItems = 0;
            foreach (OrderItem oi in BLayer.getOrderItems(Convert.ToInt32(orderID)))
            {
                totalItems = oi.quantity * oi.price;
                total += totalItems;
                outString +=
            "   " + oi.modelName + " x " + Convert.ToString(oi.quantity) +" $" + Convert.ToString(totalItems) + "\r\n";
            }
            int ordership = BLayer.getOrderShipping(Convert.ToInt32(orderID));
            DataLayer.DataModels.ShippingOptions so = BLayer.getShippingOption(ordership);

            total += so.cost;
            outString+= "" +
            "   shipping: $" + Convert.ToString(so.cost) + "\r\n" +
            "   total: $" + Convert.ToString(total);

            //outString += "  total: $" + Convert.ToString(total);
            return outString;
        }
    }
}

