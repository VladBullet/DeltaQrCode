using System;
using System.Collections.Generic;
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
        Task<Result<CaAppointments>> ConfirmAppointmentAsync(int id, bool confirm);
        Task<Result<List<CaAppointments>>> GetAppointmentsAsync(DateTime date, int? rampId = null);
        Task<Result<List<CaAppointments>>> GetAppointmentsAsync();
        Task<Result<CaServicetypes>> GetServiceTypeByIdAsync(uint id);
        Task<Result<CaServicetypes>> AddServiceTypeAsync(CaServicetypes serviciu);
        Task<Result<CaServicetypes>> GetServiceTypeByLableAsync(string label);

    }
}
