
namespace DeltaQrCode.ModelsDto
{
    public class Uzura
    {
        public Uzura()
        {

        }

        public Uzura(Uzura u)
        {
            StF = u.StF;
            StS = u.StS;
            DrF = u.DrF;
            DrS = u.DrS;

        }
        public Uzura(int stF, int? stS, int? drF, int? drS, int? opt1, int? opt2)
        {
            StF = stF;
            StS = stS;
            DrF = drF;
            DrS = drS;
            Opt1 = opt1;
            Opt2 = opt2;
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
        public int? Opt1 { get; set; }
        public int? Opt2 { get; set; }

    }
   
}
