using System.Threading.Tasks;

namespace DeltaQrCode.Repositories.Guest
{
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;

    public interface IGuestRepository
    {
        Task<Result<GuestInfoDto>> ConfirmAppointmentAsync(string guid);
        Task<Result<CaAppointments>> GetAppointmentByGuid(string guid);
        Task<Result<GuestInfoDto>> GetAppointmentInfoByGuid(string guid);
    }
}
