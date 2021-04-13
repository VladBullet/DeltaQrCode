using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories.Guest
{
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;

    public interface IGuestRepository
    {
        Task<Result<EmptyDto>> ConfirmAppointmentAsync(string guid);
    }
}
