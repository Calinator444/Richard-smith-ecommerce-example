using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;

using System.Timers;

namespace Assgt1.Scripts
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool blogged = (Session["currentuser"] != null);
            loggedInControls.Visible = blogged;
            notlogged.Visible = !blogged;
            //Session["logtime"] = 0;

            //timer with an interval of 1 second
            //var myTime = new System.Timers.Timer(1000);
            //myTime.Elapsed += MyTime_Elapsed;
            //myTime.Start();


        }

        //private void MyTime_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    int timeActive = (int)Session["logtime"];
        //    if (timeActive > 5)
        //        redirect();
        //    else
        //    {
        //        timeActive += 1;
        //        Session["logtime"] = timeActive;
        //    }
        //}

        protected void redirect()
        {
            HttpContext.Current.Response.Redirect("ErrorPage.aspx");
        }
    }
}