
namespace DeltaQrCode.ModelsDto
{
    public class Dimensiuni
    {
        public Dimensiuni()
        {
            
        }
        public Dimensiuni(int diam, int lat, int h, int dot)
        {
            Diam = diam;
            Lat = lat;
            H = h;
            Dot = dot;
        }

        public Dimensiuni(int diam, int lat, int h)
        {
            Diam = diam;
            Lat = lat;
            H = h;
        }


        public int Diam { get; set; }
        public int Lat { get; set; }
        public int H { get; set; }
        public int Dot { get; set; }
    }
}
