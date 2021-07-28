using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.DataModels
{
    public class Product
    {
        public Product()
        {
            //empty constructor
        }
        //this class is never actually created
        //it just serves as a list of base attributes for every other product
        public int productId;
        public bool listed;
        public string description;
        public string previewImage;
        public string modelName;
        public double price;



    }
}