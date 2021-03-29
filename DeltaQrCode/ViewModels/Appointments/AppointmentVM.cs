using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ViewModels.Appointments
{
    public class AppointmentVM
    {
        public uint Id { get; set; }
        public int? ApptIndex { get; set; }
        public string Serviciu { get; set; }
        public string NrMasina { get; set; }
        public string NumeClient { get; set; }
        public string EmailClient { get; set; }
        public string NrTelefon { get; set; }
        public DateTime DataAppointment { get; set; }
        public string DataIntroducere { get; set; }
        public TimeSpan OraInceput { get; set; }
        public int DurationInMinutes { get; set; }
        public bool Confirmed { get; set; }
        public bool Canceled { get; set; }
        public DateTime? CanceledDate { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public string Notes { get; set; }
        public int RampId { get; set; }
    }
}
