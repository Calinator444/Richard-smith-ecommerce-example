using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using BuisinessLayer;
using System.Windows;

namespace Assgt1
{
    public partial class StockLevels : System.Web.UI.Page
    {
        BLobject BLayer;

        //List<DropDownList> checkableItems = new List<DropDownList>();
        List<DropDownList> StorageControls = new List<DropDownList>();
        List<DropDownList> ProcessorControls = new List<DropDownList>();
        List<DropDownList> PowerControls = new List<DropDownList>();
        List<DropDownList> MonitorControls = new List<DropDownList>();
        List<DropDownList> MiceControls = new List<DropDownList>();
        List<DropDownList> KeyboardControls = new List<DropDownList>();
        List<DropDownList> GraphicsControls = new List<DropDownList>();
        List<DropDownList> ComputerControls = new List<DropDownList>();
        List<DropDownList> CoolerControls = new List<DropDownList>();
        List<DropDownList> fanControls = new List<DropDownList>();
        //

        List<DropDownList> DefaultControls = new List<DropDownList>();


        List<DropDownList> hardCoded = new List<DropDownList>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //DateTime.Now
            Debug.WriteLine("Page reloaded at "+DateTime.Now.ToString("hhmmss"));
            BLayer = new BLobject();
            //if (!IsPostBack)
                //catergoryFillDetails();
            if(!IsPostBack)
            {
                //I only want this message to be displayed until the page reloads
                //insertSuccess.Visible = false;
                
                drpManufacturer.DataSource = BLayer.getManufacturers();
                drpManufacturer.DataBind();
                drpCategory.Items.Add("Select Category");
                drpCategory.SelectedValue = "Select Category";
            }

            insertSuccess.Visible = false;



            //this is gonna make stuff easier in the long run I swear
            ProcessorControls.Add(drpcpuGen);
            ProcessorControls.Add(drpcpuSku);

            StorageControls.Add(drpCapacity);
            StorageControls.Add(drpFormFactor);
            StorageControls.Add(drpTechnology);

            PowerControls.Add(drpRating);
            PowerControls.Add(drpWattage);

            MonitorControls.Add(drpRefresh);
            MonitorControls.Add(drpSize);
            MonitorControls.Add(drpRes);

            KeyboardControls.Add(drpKeyCaps);
            KeyboardControls.Add(drpConnection);

            MiceControls.Add(drpConnection);
            MiceControls.Add(drpOptical);

            GraphicsControls.Add(drpChipManufacturer);
            GraphicsControls.Add(drpVram);

            CoolerControls.Add(drpCoolType);
            CoolerControls.Add(drpDimensions);

            ComputerControls.Add(drpRam);
            ComputerControls.Add(drpGraphics);
            ComputerControls.Add(drpProcessor);
            fanControls.Add(drpDimensions);

            DefaultControls.Add(drpManufacturer);
            //DefaultControls.Add(drpDevice);
            //translateDropdown(drpConnection, new List<string> {"False","True"},new List<string> {"wired","wireless"});


            //these controls will not have their values changed when the dropdownlists are databound
            hardCoded.Add(drpRating);
            hardCoded.Add(drpConnection);
            hardCoded.Add(drpOptical);
            hardCoded.Add(drpCoolType);
            hardCoded.Add(drpGraphics);
            hardCoded.Add(drpProcessor);
            hardCoded.Add(drpManufacturer);
        }


        protected bool dropdownsValid(List<DropDownList> ddowns)
        {
            bool val = true;
            //Manufacturer isn't part of any of the lists by default because all products have a manufacturer
            ddowns.Add(drpManufacturer);
            foreach (DropDownList item in ddowns)
            {
                //"Select Value" is the default value for each dropdown list

                //if the statement evaluates false it means the user hasn't selected an attribute
                if (item.SelectedValue == "Select Value")
                {
                    item.BorderColor = Color.Red;
                    val = false;
                }
                else
                {
                    //Color custom = ColorConverter.Convert
                    //ColorTranslator.FromHtml()
                    Color custom = (Color)ColorTranslator.FromHtml("#ced4da");
                    item.BorderColor = custom;
                }

            }
            return val;
        }

        protected void generic_SelectIndexChanged(object sender, EventArgs e)
        {
            DropDownList dlist = (DropDownList)sender;
            Debug.Write(dlist.SelectedValue);
            if (dlist.SelectedValue == "Add new value")
            {
                addPanel.Visible = true;
                Debug.WriteLine("Select index generic was called");
                Debug.WriteLine(dlist.DataTextField);
                InsertCustom.Text = "Enter a custom " + dlist.DataTextField;

                //not registering for whatever reason
                btnAddItem.CommandArgument = dlist.ID;
            }

        }



        protected void catergoryFillDetails()
        {
            string selectedValue = drpCategory.SelectedValue;
            //DataAccess da = new DataAccess("DataObjects/" + selectedValue + ".csv");
            DataTable dt = BLayer.getProductsBindable(selectedValue);

            if (Session["lastValue"] != null)
            {
                hideControls(false, Session["lastValue"].ToString());
                string sessionOut = (string)Session["lastValue"];
            }
            bindAndCreate(DefaultControls, dt);
            //drpManufacturer.DataSource = da.returnAll();
            drpDevice.DataSource = dt;//da.returnAll();
            drpDevice.DataBind();
            //txtPrice.Text = Convert.ToString(dt["txtPrice"]);
            hideControls(true, drpCategory.SelectedValue, dt);
            Session["lastValue"] = selectedValue;
            changeConfirm.CommandArgument = drpDevice.SelectedValue;
            updateFormDetails();
        }

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            /*
            string selectedValue = drpCategory.SelectedValue;
            //DataAccess da = new DataAccess("DataObjects/" + selectedValue + ".csv");
            DataTable dt = BLayer.getProductsBindable(selectedValue);

            if (Session["lastValue"] != null)
            {
                hideControls(false, Session["lastValue"].ToString());
                string sessionOut = (string)Session["lastValue"];
                //Debug.WriteLine("Attempting to hide control with value: " + sessionOut);
            }
            //drpDevice.DataSource = da.returnAll();
            //drpDevice.DataBind();
            bindAndCreate(DefaultControls, dt);
            //drpManufacturer.DataSource = da.returnAll();
            drpDevice.DataSource = dt;//da.returnAll();
            drpDevice.DataBind();
            //txtPrice.Text = Convert.ToString(dt["txtPrice"]);
            hideControls(true, drpCategory.SelectedValue, dt);
            Session["lastValue"] = selectedValue;
            changeConfirm.CommandArgument = drpDevice.SelectedValue;
            updateFormDetails();*/
            if(!(drpCategory.SelectedValue == "Select Category"))
                catergoryFillDetails();

        }

        /*protected DataTable returnAppropriateDatasource(string productCategory)
        {
            switch (productCategory)
            {

            }
            return new DataTable();
        }*/

        protected void translateDropdown(DropDownList dlist, List<string> terms, List<string> replacement)
        {
            foreach(ListItem item in dlist.Items)
            {

                
                int getTerm = terms.IndexOf(item.Text);
                if(getTerm > -1)
                    item.Text = replacement.ElementAt(getTerm);
            }
        }


        //if you don't pass a datatable as an argument every control in the list supplied will have it's datasource be set to null
        protected void hideControls(bool visibleAction, string selectedValue, DataTable dt = null)
        {
            

            bindAndCreate(translateControls(selectedValue), dt);
            DropDownList dSpy = translateControls(selectedValue).First();
            dSpy.Parent.Visible = visibleAction;
            dSpy = translateControls(selectedValue).Last<DropDownList>();
            //sometimes some of our controls are countained in a separate asp:Panel
            //so we need to set our last value's parent to visible to get that panel
            dSpy.Parent.Visible = visibleAction;
            Debug.WriteLine("Identified object of ID " + dSpy.Parent.ID + " visible set to" + Convert.ToString(visibleAction));
            if (drpCategory.Text == "Laptops")
            {
                
                drpGraphics.DataSource = BLayer.getLaptopGraphics();
                drpGraphics.DataBind();
                drpProcessor.DataSource = BLayer.getLaptopProcessor();
                drpProcessor.DataBind();
            }
            
        }



        //returns a list of all the controls that need to be validated based on the product category
        //i.e. When entering a new Monitor into the system the program will need to check that the user has selected a resolution
        protected List<DropDownList> translateControls(string controlVal)
        {
            //passes a default value of "null" if none of the values match
            List<DropDownList> returnControl;
            //intellisense doesn't consider instantiation inside of a switch statement valid
            //this one's just for the compiler
            returnControl = null;



            switch (controlVal)
            {
                //What have I done to deserve this torment?
                case "Processors":
                    returnControl = ProcessorControls;
                    break;
                case "Storage":
                    returnControl = StorageControls;
                    break;
                case "PowerSupplies":
                    returnControl = PowerControls;
                    break;
                case "Monitors":
                    returnControl = MonitorControls;
                    break;
                case "Mice":
                    returnControl = MiceControls;
                    break;
                case "Laptops":
                    returnControl = ComputerControls;
                    break;
                case "Keyboards":
                    returnControl = KeyboardControls;
                    break;
                case "GraphicsCards":
                    returnControl = GraphicsControls;
                    break;
                case "Fans":
                    returnControl = fanControls;
                    break;
                case "Coolers":
                    returnControl = CoolerControls;
                    break;
                case "Branded":
                    returnControl = ComputerControls;
                    break;
            }
            return returnControl;
        }



        //searches for a dropdownlist with a given ID (out of all the drop down lists for the selected product category)
        //and adds a new value to the list
        protected void addListItem(string searchId, string value, string label)
        { 
            foreach (DropDownList dlist in translateControls(drpCategory.Text))
            {
                if (dlist.ID == searchId)
                {
                    ListItem li = new ListItem(label,value);
                    //var ListItem = dlist.Items.Add("");
                    dlist.Items.Add(li);
                    return;
                }

            }

        }
        //databinds the form and creates a default value
        protected void bindAndCreate(List<DropDownList> objList, DataTable data)
        {

            foreach (DropDownList obj in objList)
            {
                //we don't want hard coded controls to have their values replaced
                if (!hardCoded.Contains(obj))
                {
                    obj.DataSource = data;
                    obj.DataBind();
                    //obj.Items.Add("Select Value");
                    obj.Items.Add("Add new value");
                }
            }

            Session["checkableItems"] = objList;

            //switch(drpCategory.SelectedValue)
            //{
            //    case "Keyboards":
            //    {
            //            translateDropdown(drpConnection, new List<string> {"False", "True"}, new List<string> {"wired", "wireless" });
            //            break;
            //    }
            //}
        }

        //when the user changes which model they're looking at for a given item category this 
        //function updates the form data to match that model
        protected void updateFormDetails()
        {
            string deviceCategory = drpCategory.SelectedValue;
            string deviceID = drpDevice.SelectedValue;
            alterModelNo.Text = drpDevice.SelectedItem.Text;
            //DataAccess da = new DataAccess("DataObjects/" + deviceCategory + ".csv");
            DataTable dt = BLayer.getProductsBindable(deviceCategory);


            //laying out the attributes of the selected item
            DataRow getDR = dt.Rows[0];
            txtPrice.Text = Convert.ToString(getDR["price"]);
            txtImage.Text = Convert.ToString(getDR["previewImage"]);
            txtDescription.Text = Convert.ToString(getDR["description"]);

            DataRow[] dr = dt.Select("productID = '" + deviceID + "'");
            DataRow getdr = dr[0];
            drpManufacturer.SelectedValue = (string)getdr["manufacturerID"];
            foreach (DropDownList ctrl in translateControls(deviceCategory))
            {
                
                //Debug.WriteLine((string)getdr[ctrl.DataTextField]);
                ctrl.SelectedValue = (string)getdr[ctrl.DataValueField];//da.returnSingleValue("Model", deviceName, ctrl.DataTextField);
            }
            changeConfirm.CommandArgument = deviceID;
            //translateControls()
        }
        protected void drpDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateFormDetails();
        }


        protected void defaultDropLists(List<DropDownList> givenLists)
        {
            foreach (DropDownList list in givenLists)
            {
                list.SelectedValue = "Select Value";
            }
        }

        protected void addItem_Click(object sender, EventArgs e)
        {
            txtPrice.Text = "0";
            alterModelNo.Text = "";
            insertText.Text = "Add a new product to '" + drpCategory.Text+"'";
            toggleSubmit();
            //addItem.Visible = false;
            //submitProduct.Visible = true;
            //cancelProduct.Visible = true;
            //revertChange.Visible = false;
            //changeConfirm.Visible = false;
            //MainItems.Visible = false;
            foreach (DropDownList dlist in translateControls(drpCategory.Text))
            {
                if(!listContains(dlist, "Select Value"))
                    dlist.Items.Add("Select Value");
            }

            foreach (DropDownList dlist in DefaultControls)
            {
                if (!listContains(dlist, "Select Value"))
                    dlist.Items.Add("Select Value");
            }
            defaultDropLists(DefaultControls);
            defaultDropLists(translateControls(drpCategory.SelectedValue));
        }

        protected bool listContains(DropDownList dlist, string searchTerm)
        {
            foreach(ListItem item in dlist.Items)
            {
                if (item.Text == searchTerm)
                    return true;
            }
            return false;
        }

        protected void addItemValidation_ServerValidate(object source, ServerValidateEventArgs args)

        {
            if (!dropdownsValid(translateControls(drpCategory.SelectedValue)))
            {
                errPanel.Visible = true;
                //Validatoroutput.Visible = true;
                Validatoroutput.Text = "Please fill out all the mandatory criteria for that item";
                args.IsValid = false;
            }
            else
            {
                Validatoroutput.Visible = false;
                args.IsValid = true;

            }
                
        }

        protected void HideManufacturer_Click(object sender, EventArgs e)
        {
            drpManufacturer.Parent.Visible = false;

        }

        protected void txtPrice_TextChanged(object sender, EventArgs e)
        {
            TextBox txtin = (TextBox)sender;
            double val = Convert.ToDouble(txtin.Text);
            val = Math.Round(val, 2);
            txtin.Text = Convert.ToString(val);

        }
        //hides (or shows) all of the form fields necessary to add a new item to the database
        protected void toggleSubmit()
        {
            submitProduct.Visible = !submitProduct.Visible;
            errPanel.Visible = !errPanel.Visible;
            cancelProduct.Visible = !cancelProduct.Visible;
            addItem.Visible = !addItem.Visible;
            revertChange.Visible = !revertChange.Visible;
            changeConfirm.Visible = !changeConfirm.Visible;
            MainItems.Visible = !MainItems.Visible;
            insertHeader.Visible = !insertHeader.Visible;
        }

        protected void cancelProduct_Click(object sender, EventArgs e)
        {
            toggleSubmit();
            //submitProduct.Visible = false;
            //errPanel.Visible = false;
            //cancelProduct.Visible = false;
            //addItem.Visible = true;
            //revertChange.Visible = true;
            //changeConfirm.Visible = true;
            updateFormDetails();
        }

        protected void changeConfirm_Click(object sender, EventArgs e)
        {
            Button getSender = (Button)sender;
            int listed = Convert.ToInt32(chckListed.Checked);
            string modelNo = alterModelNo.Text;
            string description = txtDescription.Text;
            double price = Convert.ToDouble(txtPrice.Text);
            int productID = Convert.ToInt32(getSender.CommandArgument);
            int manufacturerID = Convert.ToInt32(drpManufacturer.SelectedValue);

            string previewImage = txtImage.Text;


            bool bOptical;
            bool bWireless;
            int iWireless, iOptical;

            switch (drpCategory.SelectedValue)
            {
                case "Processors":
                    BLayer.alterProcessor(productID, listed, modelNo, description, price, previewImage, manufacturerID, 
                        Convert.ToInt32(drpcpuGen.SelectedValue), Convert.ToInt32(chckIGPU.Checked), Convert.ToInt32(drpcpuSku.SelectedValue));
                    break;
                case "Storage":
                    BLayer.alterStorage(productID, listed, modelNo, description, price, previewImage, manufacturerID, Convert.ToInt32(drpCapacity.SelectedValue),
                        Convert.ToDouble(drpFormFactor.Text), drpTechnology.Text);
                    break;
                case "PowerSupplies":
                    BLayer.alterPowerSupply(productID, listed, modelNo, description, price, previewImage, manufacturerID, Convert.ToInt32(drpRating.SelectedValue)
                    , Convert.ToInt32(drpWattage.SelectedValue));
                    break;
                //why are we still here?
                case "Monitors":
                    BLayer.alterMonitor(productID, listed, modelNo, description, price, previewImage, manufacturerID, Convert.ToInt32(drpRefresh.SelectedValue), Convert.ToDouble(drpSize.SelectedValue), drpRes.SelectedValue);
                    break;
                case "Mice":

                    bWireless = Convert.ToBoolean(drpConnection.SelectedValue);
                    iWireless = Convert.ToInt32(bWireless);
                    bOptical = Convert.ToBoolean(drpOptical.SelectedValue);
                    iOptical = Convert.ToInt32(bOptical);
                    BLayer.alterMouse(productID, listed, modelNo, description, price, previewImage, manufacturerID, iWireless, iOptical);
                    break;
                case "Laptops":
                    BLayer.alterComputer(productID, listed, modelNo, description, price, previewImage, manufacturerID, Convert.ToInt32(drpRam.SelectedValue) , 1, Convert.ToInt32(drpGraphics.SelectedValue), Convert.ToInt32(drpProcessor.SelectedValue));
                    break;
                case "Keyboards":
                    bWireless = Convert.ToBoolean(drpConnection.SelectedValue);
                    iWireless = Convert.ToInt32(bWireless);
                    BLayer.alterKeyboard(productID, listed, modelNo, description, price, previewImage, manufacturerID, iWireless, drpKeyCaps.SelectedValue);
                    break;
                case "GraphicsCards":
                    BLayer.alterGraphicsCard(productID, listed, modelNo, description, price, previewImage, manufacturerID, Convert.ToInt32(drpVram.Text), Convert.ToInt32(drpChipManufacturer.SelectedValue)); 
                    break;
                case "Fans":
                    BLayer.alterCooler(productID, listed, modelNo, description, price, previewImage, manufacturerID, 2, Convert.ToInt32(drpDimensions.SelectedValue));
                    break;
                case "Coolers":
                    BLayer.alterCooler(productID, listed, modelNo, description, price, previewImage, manufacturerID, Convert.ToInt32(drpCoolType.Text), Convert.ToInt32(drpDimensions.SelectedValue));
                    break;
            }
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            addPanel.Visible = false;
            if (txtAddValue.Text == "")
                addListItem(btn.CommandArgument, txtAddItem.Text, txtAddItem.Text);
            else
                addListItem(btn.CommandArgument, txtAddValue.Text, txtAddItem.Text);
            updateFormDetails();
        }

        protected void submitProduct_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                toggleSubmit();
                insertSuccess.Visible = true;
                
                Button getSender = (Button)sender;
                int listed = Convert.ToInt32(chckListed.Checked);
                string modelNo = alterModelNo.Text;
                string description = txtDescription.Text;
                double price = Convert.ToDouble(txtPrice.Text);
                //int productID = Convert.ToInt32(getSender.CommandArgument);
                int manufacturerID = Convert.ToInt32(drpManufacturer.SelectedValue);

                string previewImage = txtImage.Text;


                bool bOptical;
                bool bWireless;
                int iWireless, iOptical;

                switch (drpCategory.SelectedValue)
                {
                    case "Processors":
                        BLayer.addProcessor(listed, modelNo, description, price, previewImage, manufacturerID,
                            Convert.ToInt32(drpcpuGen.SelectedValue), Convert.ToInt32(chckIGPU.Checked), Convert.ToInt32(drpcpuSku.SelectedValue));
                        break;
                    case "Storage":
                        BLayer.addStorage(listed, modelNo, description, price, previewImage, manufacturerID, Convert.ToInt32(drpCapacity.SelectedValue),
                            Convert.ToDouble(drpFormFactor.Text), drpTechnology.Text);
                        break;
                    case "PowerSupplies":
                        BLayer.addPowerSupply(listed, modelNo, description, price, previewImage, manufacturerID, Convert.ToInt32(drpRating.SelectedValue)
                        , Convert.ToInt32(drpWattage.SelectedValue));
                        break;
                    //why are we still here?
                    case "Monitors":
                        BLayer.addMonitor(listed, modelNo, description, price, previewImage, manufacturerID, Convert.ToInt32(drpRefresh.SelectedValue), Convert.ToDouble(drpSize.SelectedValue), drpRes.SelectedValue);
                        break;
                    case "Mice":

                        bWireless = Convert.ToBoolean(drpConnection.SelectedValue);
                        iWireless = Convert.ToInt32(bWireless);
                        bOptical = Convert.ToBoolean(drpOptical.SelectedValue);
                        iOptical = Convert.ToInt32(bOptical);
                        BLayer.addMouse(listed, modelNo, description, price, previewImage, manufacturerID, iWireless, iOptical);
                        break;
                    case "Laptops":
                        BLayer.addComputer(listed, modelNo, description, price, previewImage, manufacturerID, Convert.ToInt32(drpRam.SelectedValue), 1, Convert.ToInt32(drpGraphics.SelectedValue), Convert.ToInt32(drpProcessor.SelectedValue));
                        break;
                    case "Keyboards":
                        bWireless = Convert.ToBoolean(drpConnection.SelectedValue);
                        iWireless = Convert.ToInt32(bWireless);
                        BLayer.addKeyboard(listed, modelNo, description, price, previewImage, manufacturerID, iWireless, drpKeyCaps.SelectedValue);
                        break;
                    case "GraphicsCards":
                        BLayer.addGraphicsCard(listed, modelNo, description, price, previewImage, manufacturerID, Convert.ToInt32(drpVram.Text), Convert.ToInt32(drpChipManufacturer.SelectedValue));
                        break;
                    case "Fans":
                        BLayer.addCooler(listed, modelNo, description, price, previewImage, manufacturerID, 2, Convert.ToInt32(drpDimensions.SelectedValue));
                        break;
                    case "Coolers":
                        BLayer.addCooler(listed, modelNo, description, price, previewImage, manufacturerID, Convert.ToInt32(drpCoolType.Text), Convert.ToInt32(drpDimensions.SelectedValue));
                        break;
                    
                }
                updateFormDetails();
            }
        }
    }
}