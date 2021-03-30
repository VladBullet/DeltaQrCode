using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ModelsDto
{
    public class Dimensiuni
    {
        public Dimensiuni()
        {
            
        }
        public Dimensiuni(string diam, string lat, string h)
        {
            Diam = diam;
            Lat = lat;
            H = h;
        }
        public string Diam { get; set; }
        public string Lat { get; set; }
        public string H { get; set; }
    }
}
