using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModels
{

    //inherit itemID etc. from product
    public class OrderItem : Product
    {
        public OrderItem()
        {
        }
        //public int orderID;
        public int quantity;
    }
}
