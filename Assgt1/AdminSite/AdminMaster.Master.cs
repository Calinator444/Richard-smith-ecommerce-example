using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BuisinessLayer;

namespace Assgt1
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        BLobject BLayer;
        protected void Page_Load(object sender, EventArgs e)
        {

            int usrID = (int) Session["CurrentUser"];
            BLayer = new BLobject();
            BLayer.checkAdmin(usrID);
            //redirects the user if they aren't signed in to an admin account
            if (!BLayer.checkAdmin(usrID))
                Response.Redirect("ErrorPage.aspx");
        }
    }
}