using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using DeltaQrCode.Models;

    public interface IAppointmentsRepository
    {
        Task<Result<CaAppointment>> GetAppointmentByIdAsync(int id);
        Task<Result<CaAppointment>> AddAppointmentAsync(CaAppointment appointment);
        Task<Result<CaAppointment>> UpdateAppointmentAsync(CaAppointment appointment);
        Task<Result<CaAppointment>> CancelAppointmentAsync(int id);
        Task<Result<CaAppointment>> ConfirmAppointmentAsync(int id);
        Task<Result<List<CaAppointment>>> GetAppointmentsAsync(DateTime date);


    }
}
