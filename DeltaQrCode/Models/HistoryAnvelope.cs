using System;

namespace DeltaQrCode.Models
{
    public partial class HistoryAnvelope
    {
        public uint Id { get; set; }
        public int SetAnvelopeId { get; set; }
        public DateTime DataModificare { get; set; }
        public string Changes { get; set; }
    }
}
