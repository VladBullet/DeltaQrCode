using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaQrCode.ViewModels.Scheduler
{
    using AppointmentsDb.ModelsDto;

    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.Services;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AppointmentModalVm
    {

        /// <summary>
        /// This is a list of other professionals this professional can view the calendar for.
        /// This professional can also set appointments for them.
        /// </summary>

        /// <summary>
        /// The list of ProfessionalsAccessableEmployees in a format MVC can use for selection.
        /// </summary>
        public IEnumerable<SelectListItem> ProfessionalsAccessableEmployeesDropDownModal;


        public DateTime ActiveDate { get; set; }


        public AppointmentForProUiDto Appointment { get; set; }

        public AppointmentModalVm()
        {
            Appointment = new AppointmentForProUiDto();
        }
        public AppointmentModalVm(string userId, AppointmentForProUiDto appointment)
        {
            Appointment = appointment;
        }
    }
}