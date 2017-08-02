using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplication3.Models;

namespace MvcApplication3.Controllers
{
    public class Item
    {
        private Product pr = new Product();

        public Product Pr
        {
            get { return pr; }
            set { pr = value; }
        }
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public Item()
        { }
        public Item(Product pr1 , int quantity1) 
        {
            this.pr = pr1;
            this.quantity = quantity1;
        
        
        }

    }
}