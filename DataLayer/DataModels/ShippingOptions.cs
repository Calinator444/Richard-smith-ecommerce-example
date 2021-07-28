using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModels
{
    public class ShippingOptions
    {
        public ShippingOptions()
        { 

        }

        public string description { get; set; }
        public string name { get; set; }
        public double cost  { get; set; }
        public int days { get; set; }
    }
}
