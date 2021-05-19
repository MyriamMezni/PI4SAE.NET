using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI4SAE.Models
{
    public class RdvStat
    {
        public int count { get; set; }
        public KinderGarten kinderGarten { get; set; }

        public RdvStat() { }

        /*public RdvStat(long count, KinderGarten kg)
        {
            this.count = count;
            this.kinderGarten = kg;
        }*/
    }
}