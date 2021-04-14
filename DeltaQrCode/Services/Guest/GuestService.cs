using System;
using System.Threading.Tasks;

namespace DeltaQrCode.Services.Guest
{
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.Repositories.Guest;
    using DeltaQrCode.Services.Mail;

    using Serilog;

    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepo;
        private readonly IMailService _mailService;

        public GuestService(IGuestRepository guestRepo, IMailService mailService)
        {
            _guestRepo = guestRepo;
            _mailService = mailService;
        }

        public async Task<Result<GuestInfoDto>> ConfirmAppointmentAsync(string guid)
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
        private async Task<Result<EmptyDto>> SendInfoEmailTocompany(string guid, bool confirmed)
        {
            try
            {
                var appt = await _guestRepo.GetAppointmentByGuid(guid);
                //var sent = await _mailService.SendEmail();
                return Result<EmptyDto>.ResultOk(null);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la confirmarea programarii in servicii!");
                throw new Exception("Ceva nu a mers bine la confirmarea programarii in servicii!", er);
            }
        }
    }
}
