
namespace DeltaQrCode.Models
{
    public class Position
    {
        public Position()
        {

        }
        public Position(string input) //TODO: remove after avaiable positions drompdown is in place
        {
            Rand = input[0].ToString();
            Poz = input[1].ToString();
            Interval = input[2].ToString();
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
