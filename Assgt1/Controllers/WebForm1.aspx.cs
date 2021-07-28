using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Assgt1.Controllers
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //DataAccess da = new DataAccess("C:/Users/caleb/source/repos/Assgt1/Assgt1/Controllers/Processors.csv");
            

            //creates a  table and binds it to the FormView object
            //IDataSource myFile = new IDataSource();
            //IDataSource myDataSource;
            //Reads the CSV file and outputs it to a table 

            

            

            //ProcessorsPage.DataSource = da.queryTable("Manufacturer = 'AMD'");
            //ProcessorsPage.DataSource = dt.Select("Manufacturer = 'Intel'");
            ProcessorsPage.DataBind();
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            Debug.WriteLine("Data binding was set up");
            /*
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Console.Write(dt.Columns[i].ColumnName + " ");
                    Console.WriteLine(dt.Rows[j].ItemArray[i]);
                }
            }*/
            //Debug.WriteLine
        }
        


        

        protected void Manufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(Convert.ToString(Manufacturer.SelectedIndex));
            switch (Manufacturer.SelectedIndex)
            {
                case -1:
                    IntelGeneration.Visible = AMDGeneration.Visible = false;
                    break;
                case 0:
                    
                    IntelGeneration.Visible = true;
                    AMDGeneration.Visible = false;
                    break;
                
                case 1:
                    IntelGeneration.Visible = false;
                    AMDGeneration.Visible = true;
                    break;
            }
                    

            
            
        }

        protected void Unnamed_CheckedChanged(object sender, EventArgs e)
        {
            Debug.Write("checkbox was changed");
 
        }
        
    }
}