using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using DeltaQrCode.Models;

    public interface IAppointmentsRepository
    {
        Task<Result<CaAppointments>> GetAppointmentByIdAsync(int id);
        Task<Result<CaAppointments>> AddAppointmentAsync(CaAppointments appointment);
        Task<Result<CaAppointments>> UpdateAppointmentAsync(CaAppointments appointment);
        Task<Result<CaAppointments>> DeleteAppointmentAsync(int id);
        Task<Result<CaAppointments>> ConfirmAppointmentAsync(int id);
        Task<Result<List<CaAppointments>>> GetAppointmentsAsync(DateTime date);


    }
}
