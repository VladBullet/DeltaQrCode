using System;
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
        public async Task<Result<GuestInfoDto>> ConfirmAppointmentAsync(string guid)
        {
            try
            {
                var result = await _db.CaAppointments.FirstOrDefaultAsync(x => x.ConfirmedCode == guid);
                if (!result.Deleted)
                {
                    result.Confirmed = !result.Confirmed;
                    result.ConfirmedDate = DateTime.Now;
                    result.ChangedByClient = true;
                    await _db.SaveChangesAsync();
                }
                var model = new GuestInfoDto(result.ConfirmedCode, result.Confirmed, result.DataAppointment, result.OraInceput, result.DurataInMinute, !result.Deleted);
                return Result<GuestInfoDto>.ResultOk(model);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la confirmarea programarii in repository!");
                throw new Exception("Ceva nu a mers bine la confirmarea programarii in repository!", er);
            }
        }

        /// <inheritdoc />
        public async Task<Result<CaAppointments>> GetAppointmentByGuid(string guid)
        {
            try
            {
                var result = await _db.CaAppointments.FirstOrDefaultAsync(x => x.ConfirmedCode == guid);
                var model = new GuestInfoDto(result.ConfirmedCode, result.Confirmed, result.DataAppointment, result.OraInceput, result.DurataInMinute, !result.Deleted);
                return Result<CaAppointments>.ResultOk(result);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la confirmarea programarii in repository!");
                throw new Exception("Ceva nu a mers bine la confirmarea programarii in repository!", er);
            }
        }
        public async Task<Result<GuestInfoDto>> GetAppointmentInfoByGuid(string guid)
        {
            try
            {
                var result = await _db.CaAppointments.FirstOrDefaultAsync(x => x.ConfirmedCode == guid);
                var model = new GuestInfoDto(result.ConfirmedCode, result.Confirmed, result.DataAppointment, result.OraInceput, result.DurataInMinute, !result.Deleted);
                return Result<GuestInfoDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la confirmarea programarii in repository!");
                throw new Exception("Ceva nu a mers bine la confirmarea programarii in repository!", er);
            }
        }
    }
}
