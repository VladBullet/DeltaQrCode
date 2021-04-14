using System;

namespace DeltaQrCode.Models
{
    public partial class CaAppointments
    {
        public uint Id { get; set; }
        public int? ApptIndex { get; set; }
        public uint? ServiciuId { get; set; }
        public string NumarInmatriculare { get; set; }
        public string NumeClient { get; set; }
        public string NumarTelefon { get; set; }
        public DateTime DataAppointment { get; set; }
        public DateTime DataIntroducere { get; set; }
        public TimeSpan OraInceput { get; set; }
        public bool Deleted { get; set; }
        public string Observatii { get; set; }
        public bool Confirmed { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public string ConfirmedCode { get; set; }
        public DateTime LastModified { get; set; }
        public int RampId { get; set; }
        public string EmailClient { get; set; }
        public int DurataInMinute { get; set; }
    }
}
