using System;
using System.Collections.Generic;

namespace DeltaQrCode.Models
{
    public partial class CaSetAnvelope
    {
        public uint Id { get; set; }
        public string NumeClient { get; set; }
        public string NumarTelefon { get; set; }
        public string NumarInmatriculare { get; set; }
        public string Rand { get; set; }
        public string Pozitie { get; set; }
        public string Interval { get; set; }
        public uint? MarcaId { get; set; }
        public string NumeSet { get; set; }
        public int NrBucati { get; set; }
        public string Dimensiuni { get; set; }
        public string Uzura { get; set; }
        public string TipSezon { get; set; }
        public string Observatii { get; set; }
        public string StatusCurent { get; set; }
        public DateTime DataUltimaModificare { get; set; }
        public bool Deleted { get; set; }
    }
}
