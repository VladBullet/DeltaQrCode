using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ViewModels.HotelAnvelope
{
    public class HotelPositionsVM
    {
        public uint Id { get; set; }
        public string Rand { get; set; }
        public string Pozitie { get; set; }
        public string Interval { get; set; }
        public bool Ocupat { get; set; }
        public int? Locuriocupate { get; set; }
    }
}
