using System;
using System.Collections.Generic;

namespace DeltaQrCode.Models
{
    public partial class CaSetAnvelope
    {
        public uint Id { get; set; }
        public int MasinaId { get; set; }
        public int ClientId { get; set; }
        public string NumeSet { get; set; }
    }
}
