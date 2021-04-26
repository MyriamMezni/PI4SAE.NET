using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webtuto.Models
{
    public class ChargeRequest
    {
        public string carta { get; set; }
        public int expMonth { get; set; }
        public int expYear { get; set; }
        public string cvc { get; set; }
  
        
    }
}