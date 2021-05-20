using System;
using System.Collections.Generic;

namespace DeltaQrCode.Models
{
    public partial class CaSetAnvelope
    {
        public uint Id { get; set; }
        public uint MasinaId { get; set; }
        public uint ClientId { get; set; }
        public string NumeSet { get; set; }
        public int NrBucati { get; set; }
        public bool Deleted { get; set; }

    }
}
