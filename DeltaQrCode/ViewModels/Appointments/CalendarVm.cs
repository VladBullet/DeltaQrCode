using System;
using System.Collections.Generic;

namespace DeltaQrCode.ViewModels.Appointments
{

    public class CalendarVm
    {
        public List<DateTime> AvailableDates { get; set; }

        public DateTime ActiveDate { get; set; }

        public Guid CurrentProfessionalId { get; set; }

        public CalendarVm(string userId)
        {

        }

    }


}