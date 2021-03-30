using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Models
{
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class Position
    {
        public Position()
        {

        }
        public Position(string rand, string poz, string interval)
        {
            Rand = rand;
            Poz = poz;
            Interval = interval;
        }
        public string Rand { get; set; }
        public string Poz { get; set; }
        public string Interval { get; set; }


        public string PositionString => string.Format("Rand:{0}, Poz:{1}, Int:{2}", Rand, Poz, Interval);

    }
}
