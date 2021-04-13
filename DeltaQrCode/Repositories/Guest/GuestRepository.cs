using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories.Guest
{
    using DeltaQrCode.Data;
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;

    using Microsoft.EntityFrameworkCore;

    using Serilog;

    public class GuestRepository : IGuestRepository
    {
        private ApplicationDbContext _db;

        public GuestRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Result<EmptyDto>> ConfirmAppointmentAsync(string guid)
        {
            try
            {
                var result = await _db.CaAppointments.FirstOrDefaultAsync(x => x.ConfirmedCode == guid);
                result.Confirmed = !result.Confirmed;
                result.ConfirmedDate = DateTime.Now;
                await _db.SaveChangesAsync();

                return Result<EmptyDto>.ResultOk(null);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la confirmarea programarii in repository!");
                throw new Exception("Ceva nu a mers bine la confirmarea programarii in repository!", er);
            }
        }

    }
}
