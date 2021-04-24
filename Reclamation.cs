using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReclamationPI.Models
{
    public class Reclamation
    {
        public int IdParent { get; set; }
        public int IdReclamation { get; set; }
        public DateTime Date { get; set; }
        public String Subject { get; set; }
        public String Description { get; set; }
        public String TypeReclamation { get; set; }
        public Boolean Statue { get; set; }
    }
}