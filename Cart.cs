using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webtuto.Models
{
    public class Cart
    {
        public long idCart { get; set; }
        public int quantite { get; set; }
        public double prix { get; set; }
       
        public virtual List<ItemList> ItemLists { get; set; }
        
    





    }
}