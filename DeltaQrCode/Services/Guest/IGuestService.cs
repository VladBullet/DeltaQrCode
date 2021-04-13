using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services.Guest
{
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;

    public interface IGuestService
    {
        Task<Result<EmptyDto>> ConfirmAppointmentAsync(string guid);
    }
}
