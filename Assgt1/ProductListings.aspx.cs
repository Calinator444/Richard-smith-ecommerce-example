using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data;
//using Assgt1.DataObjects;
using BuisinessLayer;
using DataLayer.DataModels;
namespace Assgt1
{
    public partial class ProductListings : System.Web.UI.Page
    {
        BLobject BLayer;
        DataTable queryTable;
        string ProductType;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            BLayer = new BLobject();
            
            //prevents the databindings from being recreated whenever a control posts back to the server

            //checkManufacturer.DataTextField = "name";
            //translateControls(checkRating, new List<string> {"1","2","3","4" }, new List<string> {"bronze","silver","gold","platinum"});
            //string str = checkManufacturer.DataTextField;
            ProductType = Request.QueryString["ProductType"];
            queryTable = BLayer.getProductsBindable(ProductType);
            Debug.WriteLine(queryTable.Rows.Count);
            //if there are no listed items of a given category this line of code will throw an error
            try
            {
                queryTable = queryTable.Select("listed = 'True'").CopyToDataTable();
            }
            
            catch(System.InvalidOperationException)
            {
                Response.Redirect("NoItems.aspx");
            }
            //ProductList.DataSource = queryTable.Select("listed = 'True'").CopyToDataTable();
           
            if (!IsPostBack)
            {
                Session["Parameters"] = new List<parameter>();
                //the parameters the user selects are added to a list which is stored as a session variable
                //Session["parameters"] = new List<parameter>();

                //stores a list iof order items 
                if (Session["Cart"] == null)
                {
                    Session["Cart"] = new List<Item>();
                }
                //make sure the products are set as 'listed' before we display them
                ProductList.DataSource = queryTable;
                ProductList.DataBind();


                switch (ProductType)
                {
                    case "Laptops":
                        enableControls(checkCpu, lblCpu);
                        enableControls(checkGraphics, lblGraphics);
                        break;
                    case "Mice":
                        enableControls(checkConn, lblConn);
                        break;
                    case "Keyboards":
                        enableControls(checkCherry, lblCherry);
                        prefixList(checkCherry, "cherry ");
                        enableControls(checkConn, lblConn);
                        break;
                    case "Monitors":
                        enableControls(checkRefresh, lblRefresh);
                        enableControls(checkInches, lblInches);
                        suffixList(checkInches, "\"");
                        enableControls(checkResolution, lblResolution);
                        break;
                    case "GraphicsCards":
                        break;
                    case "Processors":
                        enableControls(checkSku, lblSku);
                        enableControls(checkGen, lblGen);
                        prefixList(checkSku, "i");
                        break;
                    case "PowerSupplies":
                        enableControls(checkWattage, lblWattage);
                        enableControls(checkRating, lblRating);
                        translateControls(checkRating, new List<string> { "1", "2", "3", "4" }, new List<string> { "bronze", "silver", "gold", "platinum" });
                        break;
                    case "Storage":
                        enableControls(checkCapacity, lblCapacity);
                        enableControls(checkFormFactor, lblFormFactor);
                        break;
                    case "Fans":
                        enableControls(checkDimensions, lblDimensions);
                        suffixList(checkDimensions, "mm");
                        break;
                    case "Coolers":
                        enableControls(checkDimensions, lblDimensions);
                        suffixList(checkDimensions, "mm");
                        enableControls(checkCoolType, lblCoolType);
                        translateControls(checkCoolType, new List<string> { "0", "1", "2" }, new List<string> { "air","water","fan"});
                        break;
                    case "Motherboards":
                        enableControls(checkWifi, lblwifi);
                        break;

                }


                //every product in the database is required to have  a manufacturer
                //so this control is always visible
                enableControls(checkManufacturer, lblManufacturer);

                //ProductList.DataSource = queryTable;
                //ProductList.DataBind();
            }
            


        }
        
        //applies all the parameters in the list using recursion
        protected DataTable applyParameters(List<parameter> parameters, DataTable dt)
        {
            DataTable getdt = dt;
            foreach(parameter param in parameters)
            {
                Debug.WriteLine("applying parameter " + param.name + " = " + param.value);
                getdt = dt.Select(param.name + " = '" + param.value+"'").CopyToDataTable();
            }
            return getdt;
        }
        //enables a given control and populates the checkbox list through data binding
        //takes a checkbox list as an argument, as well as the attribute it needs to be bound to
        public void enableControls(CheckBoxList obj, Label lbl)
        {
            Debug.WriteLine("Enabling controls :"+obj.ID);
            lbl.Visible = true;
            //obj.Parent.Visible = true;
            obj.Visible = true;
            //gets every unique instance of a given colummn

            if(obj.DataTextField == obj.DataValueField)
                obj.DataSource = queryTable.DefaultView.ToTable(true, obj.DataValueField);
            else
                obj.DataSource = queryTable.DefaultView.ToTable(true, obj.DataValueField,obj.DataTextField);
            obj.DataBind();
            
        }


        protected void translateControls(CheckBoxList chkList, List<string> terms, List<string> replacement)
        {
            foreach(ListItem item in chkList.Items)
            {
                int index = terms.IndexOf(item.Text);
                if(index > -1)
                    item.Text = replacement.ElementAt(index);
            }
        }
        protected void suffixList(CheckBoxList chkList, string suffix)
        {
            foreach(ListItem item in chkList.Items)
            {
                item.Text = item.Text + suffix;
            }
        }
        protected void prefixList(CheckBoxList chklist, String prefix)
        {
            foreach(ListItem item in chklist.Items)
            {
                item.Text = prefix + item.Text;
            }
        }
        
        protected void addCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string itemID = btn.CommandArgument;
            List<Item> getCart = (List<Item>)Session["Cart"];
            foreach (Item item in getCart)
            {
                if (item.getID() == Convert.ToInt32(itemID))
                {
                    item.add();
                    Session["Cart"] = getCart;
                    Response.Redirect("Cart.aspx");
                    return;
                }
            }

            //if the item isn't already in the users cart;
            getCart.Add(new Item(itemID));
            Session["Cart"] = getCart;
            Response.Redirect("Cart.aspx");
        }

        protected void btnItemView_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Response.Redirect("ProductView.aspx?ProductID="+btn.CommandArgument);
        }


        public void addFilter(CheckBoxList listItems)
        {
            //listItems.SelectedValue;
            //CheckBoxList.Data
        }


        //I don't like doing the databindings inside the event handler and I'd rather do them inside page_load
        //but page_load fires before the event handler does, meaning the attribute I'm filtering won't be added to the filter list
        protected void generic_SelectIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = queryTable;
            CheckBoxList checkList = (CheckBoxList)sender;
            Debug.WriteLine(checkList.SelectedIndex);
            parameter newParam = new parameter();
            newParam.name = checkList.DataValueField;
            newParam.value = checkList.SelectedValue;
            
            List<parameter> parameters = (List<parameter>) Session["Parameters"];

            if(checkList.SelectedIndex == -1)
            {
                foreach(parameter param in parameters)
                {
                    if(param.name == newParam.name)
                    {
                        parameters.Remove(param);
                        Debug.WriteLine("Parameter " + param.name + " was removed from parameters");
                        Session["Parameters"] = parameters;
                        ProductList.DataSource = applyParameters(parameters, dt);
                        ProductList.DataBind();
                        return;
                    }
                }
            }

            //check whether or not the parameter already exists
            foreach (parameter param in parameters)
            {
                if (param.name == newParam.name)
                {
                    //if we find the parameter we change it's value and exit the function
                    param.value = newParam.value;
                    Debug.WriteLine("Parameter " + param.name + " was set to: " + param.value);
                    Session["Parameters"] = parameters;
                    ProductList.DataSource = applyParameters(parameters, dt);
                    ProductList.DataBind();
                    return;
                }
            }
            //if we don't find the parameter we add it to the parameter list and 
            parameters.Add(newParam);
            Debug.WriteLine("Parameter " + newParam.name + " added with value " + newParam.value);
            Session["Parameters"] = parameters;
            ProductList.DataSource = applyParameters(parameters, dt);
            ProductList.DataBind();
        }

        protected void checkManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            addFilter((CheckBoxList)sender);
        }

        protected void ProductList_DataBinding(object sender, EventArgs e)
        {
            Repeater repeat = (Repeater)sender;
            Debug.WriteLine(Convert.ToString(repeat.Items.Count));
        }

    }
}
