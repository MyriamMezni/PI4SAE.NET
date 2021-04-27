using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PI4SAE.Models
{
    public class KinderGartenAdmin : User
    {
        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "training")]
        public string formation { get; set; }

    }
}