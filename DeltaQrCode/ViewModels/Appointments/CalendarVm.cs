using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaQrCode.ViewModels.Appointments
{
    using AppointmentsDb.ModelsDto.Custom;

    using DeltaQrCode.Services;

    using Microsoft.AspNetCore.Mvc.Rendering;

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