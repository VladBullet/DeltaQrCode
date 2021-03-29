using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaQrCode.ViewModels.Appointments
{

    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.Services;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AppointmentModalVm
    {

        public DateTime ActiveDate { get; set; }

        public ActionType CreateOrEdit { get; set; }

        public AppointmentForProUiDto Appointment { get; set; }

        public AppointmentModalVm()
        {
            Appointment = new AppointmentForProUiDto();
        }
        public AppointmentModalVm(ActionType createOrEdit)
        {
            Appointment = new AppointmentForProUiDto();
            CreateOrEdit = createOrEdit;
        }
        public AppointmentModalVm(string userId, AppointmentForProUiDto appointment, ActionType? creteOrEdit = null)
        {
            Appointment = appointment;
            CreateOrEdit = ActionType.Add;
        }
    }
}