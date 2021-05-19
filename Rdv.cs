using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI4SAE.Models
{
    public class Rdv
    {
        public int idRdv { get; set; }
        public string description { get; set; }
        public DateTime dateRdv { get; set; }
        public KinderGarten kinderGarten { get; set; }
        public User parent { get; set; }

        public string etat { get; set; }


        public int idkg { get; set; }

        public Rdv() { }

        public Rdv(int id)
        {
            this.idRdv = id;
        }
    }
}