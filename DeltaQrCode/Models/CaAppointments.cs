using System;
using System.Collections.Generic;

namespace DeltaQrCode.Models
{
    public partial class CaAppointments
    {
        public uint Id { get; set; }
        public int? ApptIndex { get; set; }
        public string Serviciu { get; set; }
        public string NrMasina { get; set; }
        public string NumeClient { get; set; }
        public string NrTelefon { get; set; }
        public DateTime DataAppointment { get; set; }
        public string DataIntroducere { get; set; }
        public TimeSpan OraInceput { get; set; }
    }
}
