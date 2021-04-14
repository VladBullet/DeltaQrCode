
namespace DeltaQrCode.ModelsDto
{
    public class Dimensiuni
    {
        public Dimensiuni()
        {
            
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
    }
}
