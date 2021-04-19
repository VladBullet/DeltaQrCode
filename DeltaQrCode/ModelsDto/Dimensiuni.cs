
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
        public int Diam { get; set; }
        public int Lat { get; set; }
        public int H { get; set; }
        public int Dot { get; set; }
    }
}
