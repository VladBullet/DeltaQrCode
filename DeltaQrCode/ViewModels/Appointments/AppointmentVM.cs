using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.HelpersAndExtensions;

namespace DeltaQrCode.ViewModels.Appointments
{
    public class AppointmentVM
    {
        public AppointmentVM()
        {

        }
        public AppointmentVM(DateTime appointmentStart)
        {
            DataAppointment = appointmentStart;
            OraInceput = DataAppointment.TimeOfDay;
            StartTime_Hour = OraInceput.Hours;
            StartTime_Minutes = OraInceput.Minutes;

        }

        public int Id { get; set; }
        public int? ApptIndex { get; set; }
        public int ServiciuId { get; set; }
        public string Serviciu { get; set; }
        public int RampId { get; set; }
        public string NumarInmatriculare { get; set; }
        public string NumeClient { get; set; }
        public string EmailClient { get; set; }
        public string NumarTelefon { get; set; }
        public TimeSpan OraInceput { get; set; }
        public DateTime DataAppointment { get; set; }
        public int DurataInMinute { get; set; }
        public TimeSpan OraSfarsit => OraInceput.Add(new TimeSpan(0, 0, DurataInMinute, 0));
        public DateTime DataIntroducere { get; set; }
        public bool Confirmed { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public string Observatii { get; set; }

        public int StartTime_Hour { get; set; }

        public int StartTime_Minutes { get; set; }

    }
}
