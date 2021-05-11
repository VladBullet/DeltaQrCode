using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ModelsDto
{
    public class SetDto
    {
        public MasinaDto masinaDto { get; set; }
        public ClientHotelDto clientDto { get; set; }
        public List<AnvelopaDto> anvelopeList { get; set; }
    }
}
