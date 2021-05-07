namespace DeltaQrCode.Models
{
    public partial class CaHotelPositions
    {
        public uint Id { get; set; }
        public string Rand { get; set; }
        public string Pozitie { get; set; }
        public string Interval { get; set; }
        public bool Ocupat { get; set; }
        public int? Locuriocupate { get; set; }
    }
}
