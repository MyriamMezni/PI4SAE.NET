using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webtuto.Models
{
    public class Category_Products
    {


        public long idCategory { get; set; }
        public string nameCategory { get; set; }
        public virtual List<Products> products { get; set; }
    }
}