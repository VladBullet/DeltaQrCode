﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeltaQrCode.Models;
using DeltaQrCode.ModelsDto;

namespace DeltaQrCode.Services
{
    public interface IAppointmentService
    {
        Task<Result<AppointmentDto>> GetAppointmentByIdAsync(int id);
        Task<Result<AppointmentDto>> AddAppointmentAsync(AppointmentDto appointment);
        Task<Result<AppointmentDto>> UpdateAppointmentAsync(AppointmentDto appointment);
        Result<AppointmentDto> DeleteAppointment(int id);
        Result<AppointmentDto> ConfirmAppointment(int id, bool confirm);
        Task<Result<List<AppointmentDto>>> GetAppointmentsAsync(DateTime date);
        Task<Result<List<CaServicetypes>>> GetServiceTypes();

        Task<Result<AvailableIntervalDto>> DateAndHourIsAvailable(DateTime selectedDate, TimeSpan selectedOraInceput, int selectedDurata, int selectedRampId, int? apptId = null);
    }
}
