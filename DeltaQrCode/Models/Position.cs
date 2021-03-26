using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Models
{
    public class Position
    {
        public Position()
        {

        }
        public Position(string rand, string poz)
        {
            Rand = rand;
            Poz = poz;
        }
        public string Rand { get; set; }
        public string Poz { get; set; }


    }
}
