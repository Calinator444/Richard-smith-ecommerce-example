using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.DataModels
{
    public class Item
    {
        private int quantity { get; set; }
        private string ID { get; set; }
        public Item(string ID)
        {
            this.ID = ID;
            quantity = 1;
        }

        //adds another one of these items to the user's cart
        public void add()
        {
            quantity++;
        }

        public void remove()
        {
            quantity--;
        }
        public void setQuantity(int qty)
        {
            quantity = qty;
        }
        public int getQuantity()
        {
            return quantity;
        }
        public int getID()
        {
            return Convert.ToInt32(ID);
        }

    }
}