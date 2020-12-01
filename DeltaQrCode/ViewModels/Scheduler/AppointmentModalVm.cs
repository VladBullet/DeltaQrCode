﻿using System;
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