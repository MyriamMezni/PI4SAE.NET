using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PI4SAE.Models
{
    public class User
    {
        public long idUser { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public string name { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        //   [DataType(DataType.Date)]
        //  [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //  public DateTime dateBirth { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Password"), MaxLength(50, ErrorMessage = "max length"), StringLength(25, ErrorMessage = "string length")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Phone Number")]
        public int numTel { get; set; }

        public User()
        {

        }
        public User(long IdUser, string Name, string LastName, string Address, string Email, string Password, int NumTel  )
        {
            idUser = IdUser;
            name = Name;
            lastName = LastName;
            address = Address;
            email = Email;
            password = Password;
            numTel = NumTel;
        }
        public override string ToString()
        {
            return "idUser :" + idUser + "Name : " + name + "lastName: " + lastName + "Addresse :" + address + "Email : " + email+ "password : " + password + "numTel : " + numTel;
        }



    }
}