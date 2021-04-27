using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PI4SAE.Models
{
    public class Parent : User
    {
        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "number of childes")]
        public int nbChild { get; set; }
    }
}