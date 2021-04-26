using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webtuto.Models
{
    public class Products
    {


        public long idProd { get; set; }
        public int quantite { get; set; }
       //public int quantity { get; set; }
        public string couleur { get; set; }
        public string description { get; set; }
        public string image { get; set; }

        public string nomProd { get; set; }
        public float prix { get; set; }
        public int quantity { get; set; }

        public virtual Category_Products category { get; set; }
        public virtual OfferProducts offerProducts { get; set; }
        public virtual List<ItemList> ItemLists { get; set; }
    }
}