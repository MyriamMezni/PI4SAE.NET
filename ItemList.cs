using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webtuto.Models
{
    public class ItemList
    {
        public long id { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        //public System.DateTime date { get; set; }
        public string status { get; set; }
        public  Products products { get; set; }
        public Cart cart { get; set; }

    }
}