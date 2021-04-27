using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PI4SAE.Models
{
    public class Kid
    {
        public long idKid { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Name Kid")]
        public string nameKid { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Last Name Kid")]
        public string lastNameKid { get; set; }
        // public int dateBirthKid { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Address Kid")]
        public string addressKid { get; set; }

        public Parent Parent { get; set; }
        public long? idUser { get; set; }




        public Kid()
        {

        }

        public Kid(long IdKid, string NameKid, string LastNameKid, string AddressKid)
        {
            idKid = IdKid;
            nameKid = NameKid;
            lastNameKid = LastNameKid;
            addressKid = AddressKid;
        }
    }
}