using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services.Guest
{
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.Repositories.Guest;

    using Serilog;

    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepo;

        public GuestService(IGuestRepository guestRepo)
        {
            _guestRepo = guestRepo;
        }

        public async Task<Result<EmptyDto>> ConfirmAppointmentAsync(string guid)
        {
            try
            {
                var value = await _guestRepo.ConfirmAppointmentAsync(guid);
                return value;
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la confirmarea programarii in servicii!");
                throw new Exception("Ceva nu a mers bine la confirmarea programarii in servicii!", er);
            }
        }
    }
}
