using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ModelsDto
{
    public class Uzura
    {
        public Uzura()
        {

        }

        public Uzura(string stF, string stS, string drF, string drS)
        {
            StF = stF;
            StS = stS;
            DrF = drF;
            DrS = drS;
        }
        public string StF { get; set; }
        public string StS { get; set; }
        public string DrF { get; set; }
        public string DrS { get; set; }
    }
}
