using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.Models;
using DeltaQrCode.ViewModels.Appointments;

namespace DeltaQrCode.Services
{
    public interface IAppointmentService
    {
        Task<Result<AppointmentsVM>> GetAppointmentByIdAsync(int id);
        Task<Result<AppointmentsVM>> AddAppointmentAsync(AppointmentsVM appointment);
        Task<Result<AppointmentsVM>> UpdateAppointmentAsync(AppointmentsVM appointment);
        Task<Result<AppointmentsVM>> CancelAppointmentAsync(int id);
        Task<Result<AppointmentsVM>> ConfirmAppointmentAsync(int id);
        Task<Result<List<AppointmentsVM>>> GetAppointmentsAsync(DateTime date);
    }
}
