using System.Collections.Generic;

namespace DeltaQrCode.ModelsDto
{
    public class SetDto
    {
        public MasinaDto masinaDto { get; set; }
        public ClientHotelDto clientDto { get; set; }
        public List<AnvelopaDto> anvelopeList { get; set; }
    }
}
