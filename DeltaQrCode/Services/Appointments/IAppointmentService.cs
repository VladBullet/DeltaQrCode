﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.Models;
using DeltaQrCode.ModelsDto;
using DeltaQrCode.ViewModels.Appointments;

namespace DeltaQrCode.Services
{
    public interface IAppointmentService
    {
        Task<Result<AppointmentVM>> GetAppointmentByIdAsync(int id);
        Task<Result<AppointmentVM>> AddAppointmentAsync(AppointmentDto appointment);
        Task<Result<AppointmentVM>> UpdateAppointmentAsync(AppointmentDto appointment);
        Task<Result<AppointmentVM>> DeleteAppointmentAsync(int id);
        Task<Result<AppointmentVM>> ConfirmAppointmentAsync(int id);
        Task<Result<List<AppointmentVM>>> GetAppointmentsAsync(DateTime date);
        Task<Result<List<CaServicetypes>>> GetServiceTypes();
    }
}
