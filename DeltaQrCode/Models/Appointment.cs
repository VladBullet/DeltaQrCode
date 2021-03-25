using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Models
{
    using System.ComponentModel.DataAnnotations;

    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.Services;

    public class Appointment
    {

        public Guid AppointmentId { get; set; }
        public int AppointmentIndex { get; set; }

        public string Serviciu { get; set; }
        public string NrMasina { get; set; }
        public string NumeClient { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public DateTime StartTime { get; set; }
        public int StartTime_Hour { get; set; }
        public int StartTime_Minutes { get; set; }
        public int DurationInMinutes { get; set; }

        public string StartTimeString => StartTime_Hour.ToString() + ":" + StartTime_Minutes + ":00";

        public DateTime CreatedTime { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CancelledTime { get; set; }
        public string ProfessionalNotes { get; set; }



    }

}
