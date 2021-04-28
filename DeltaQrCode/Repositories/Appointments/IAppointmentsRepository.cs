using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using DeltaQrCode.Models;

    public interface IAppointmentsRepository
    {
        Task<Result<CaAppointments>> GetAppointmentByIdAsync(int id);
        Result<CaAppointments> AddAppointment(CaAppointments appointment);
        Result<CaAppointments> UpdateAppointment(CaAppointments appointment);
        Result<CaAppointments> DeleteAppointment(int id);
        Result<CaAppointments> ConfirmAppointment(int id, bool confirm);
        Task<Result<List<CaAppointments>>> GetAppointmentsAsync(DateTime date, int? rampId = null);

        Task<Result<CaServicetypes>> GetServiceTypeByIdAsync(uint id);
        Task<Result<List<CaServicetypes>>> GetServiceTypesAsync();
        Result<CaServicetypes> AddServiceType(CaServicetypes serviciu);
        Task<Result<CaServicetypes>> GetServiceTypeByLableAsync(string label);

    }
}
