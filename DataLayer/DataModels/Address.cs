using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModels
{
    public class Address
    {
        public Address()
        {

        }

        public int addressID { get; set; }

        public int postCode { get; set; }

        public string country { get; set; }

        public string street { get; set; }

        public string suburb { get; set; }

        public int unit { get; set; }

        public bool billingAddress { get; set; }

    }
}
