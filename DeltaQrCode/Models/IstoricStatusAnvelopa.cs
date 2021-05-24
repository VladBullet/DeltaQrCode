using System;

namespace DeltaQrCode.Models
{
    public partial class IstoricStatusAnvelopa
    {
        public uint Id { get; set; }
        public int AnvelopaId { get; set; }
        public DateTime LastModified { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
    }
}
