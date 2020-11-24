using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentsMvc.ViewModels
{
    using AppointmentsDb.ModelsDto.Custom;

    using DeltaQrCode.Services;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CalendarVm
    {
        /// <summary>
        /// The list of ProfessionalsAccessableEmployees in a format MVC can use for selection.
        /// </summary>
        public List<DateTime> AvailableDates { get; set; }

        public DateTime ActiveDate { get; set; }

        public Guid CurrentProfessionalId { get; set; }

        public CalendarVm(string userId)
        {

        }

    }


}