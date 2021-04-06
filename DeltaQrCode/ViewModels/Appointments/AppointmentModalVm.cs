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

        public AppointmentVM Appointment { get; set; } // TODO:change with apptVM

        public AppointmentModalVm()
        {
            Appointment = new AppointmentVM(); // TODO: change with apptVM
        }
        public AppointmentModalVm(ActionType createOrEdit)
        {
            Appointment = new AppointmentVM();
            CreateOrEdit = createOrEdit;
        }
        public AppointmentModalVm(string userId, AppointmentVM appointment, ActionType? creteOrEdit = null) // TODO: change with apptVM
        {
            Appointment = appointment;
            CreateOrEdit = ActionType.Add;
        }
    }
}