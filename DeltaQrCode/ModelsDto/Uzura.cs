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

        public Uzura(int stF, int? stS, int? drF, int? drS)
        {
            StF = stF;
            StS = stS;
            DrF = drF;
            DrS = drS;
        }
        public int StF { get; set; }
        public int? StS { get; set; }
        public int? DrF { get; set; }
        public int? DrS { get; set; }
    }
}
