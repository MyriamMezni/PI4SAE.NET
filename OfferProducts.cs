using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webtuto.Models
{
    public class OfferProducts
    {
        public long idOffer { get; set; }
        public int valeur { get; set; }
        public virtual List<Products> products { get; set; }
    }
}