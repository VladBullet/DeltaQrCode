using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using DeltaQrCode.Data;
    using DeltaQrCode.Models;

    using Microsoft.EntityFrameworkCore;

    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly ApplicationDbContext _db;

        public AppointmentsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Result<CaAppointment>> GetAppointmentByIdAsync(int id)
        {
            try
            {
                var result = await _db.CaAppointments.FirstAsync(x => x.Id == id && !x.Canceled);
                return Result<CaAppointment>.ResultOk(result);
            }
            catch (Exception e)
            {
                return Result<CaAppointment>.ResultError(e, "Ceva nu a mers bine la gasirea programarii!");
            }
        }

        public async Task<Result<CaAppointment>> AddAppointmentAsync(CaAppointment appointment)
        {
            try
            {
                var value = await _db.CaAppointments.AddAsync(appointment);
                await _db.SaveChangesAsync();
                return Result<CaAppointment>.ResultOk(value.Entity);
            }
            catch (Exception er)
            {
                return Result<CaAppointment>.ResultError(er, "Ceva nu a mers bine la adaugarea programarii!");
            }

        }

        /// <inheritdoc />
        public async Task<Result<CaAppointment>> UpdateAppointmentAsync(CaAppointment appointment)
        {
            try
            {
                var value = _db.CaAppointments.Update(appointment);
                await _db.SaveChangesAsync();

                return Result<CaAppointment>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                return Result<CaAppointment>.ResultError(null, er, "Ceva nu a mers bine la modificarea programarii!");
            }
        }

        public async Task<Result<CaAppointment>> CancelAppointmentAsync(int id)
        {
            try
            {
                var result = await _db.CaAppointments.FindAsync(id);
                result.Canceled = true;
                result.CanceledDate = DateTime.Now;
                await _db.SaveChangesAsync();
                return Result<CaAppointment>.ResultOk(result);
            }
            catch (Exception e)
            {
                return Result<CaAppointment>.ResultError(e, "Ceva nu a mers bine la anularea programarii!");
            }
        }

        public async Task<Result<CaAppointment>> ConfirmAppointmentAsync(int id)
        {
            try
            {
                var result = await _db.CaAppointments.FirstAsync(x => x.Id == id);
                result.Confirmed = true;
                result.ConfirmedDate = DateTime.Now;
                await _db.SaveChangesAsync();
                return Result<CaAppointment>.ResultOk(result);
            }
            catch (Exception e)
            {
                return Result<CaAppointment>.ResultError(e, "Ceva nu a mers bine la confirmarea programarii!");
            }
        }

        public async Task<Result<List<CaAppointment>>> GetAppointmentsAsync(DateTime date)
        {
            try
            {
                var result = await _db.CaAppointments.Where(x => x.DataAppointment == date && !x.Canceled).ToListAsync();

                return Result<List<CaAppointment>>.ResultOk(result);
            }
            catch (Exception e)
            {
                return Result<List<CaAppointment>>.ResultError(e, "Ceva nu a mers bine la gasirea programarilor pentru data ceruta!");
            }
        }
    }
}
