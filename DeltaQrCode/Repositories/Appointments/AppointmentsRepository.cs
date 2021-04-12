using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using System.Security.Policy;

    using DeltaQrCode.Data;
    using DeltaQrCode.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger _logger;

        public AppointmentsRepository(ApplicationDbContext db, ILogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Result<CaAppointments>> GetAppointmentByIdAsync(int id)
        {
            try
            {
                var result = await _db.CaAppointments.FirstAsync(x => x.Id == id && !x.Deleted);
                return Result<CaAppointments>.ResultOk(result);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la gasirea programarii in functie de id in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea programarii in functie de id in repository!", er);
            }
        }

        public async Task<Result<CaAppointments>> AddAppointmentAsync(CaAppointments appointment)
        {
            try
            {
                var value = await _db.CaAppointments.AddAsync(appointment);
                await _db.SaveChangesAsync();
                return Result<CaAppointments>.ResultOk(value.Entity);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la adaugarea programarii in repository!");
                throw new Exception("Ceva nu a mers bine la adaugarea programarii in repository!", er);
            }

        }

        /// <inheritdoc />
        public async Task<Result<CaAppointments>> UpdateAppointmentAsync(CaAppointments appointment)
        {
            try
            {
                var value = _db.CaAppointments.Update(appointment);
                await _db.SaveChangesAsync();

                return Result<CaAppointments>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la editarea programarii in repository!");
                throw new Exception("Ceva nu a mers bine la editarea programarii in repository!", er);
            }
        }

        public async Task<Result<CaAppointments>> DeleteAppointmentAsync(int id)
        {
            try
            {
                var result = await _db.CaAppointments.FirstAsync(x => x.Id == id);
                result.Deleted = true;
                var value = _db.CaAppointments.Update(result);
                await _db.SaveChangesAsync();

                return Result<CaAppointments>.ResultOk(value.Entity);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la stergerea programarii in repository!");
                throw new Exception("Ceva nu a mers bine la stergerea programarii in repository!", er);
            }
        }

        public async Task<Result<CaAppointments>> ConfirmAppointmentAsync(int id, bool confirm)
        {
            try
            {
                var result = await _db.CaAppointments.FirstOrDefaultAsync(x => x.Id == id);
                result.Confirmed = confirm;
                result.ConfirmedDate = DateTime.Now;
                await _db.SaveChangesAsync();
                return Result<CaAppointments>.ResultOk(result);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la confirmarea programarii in repository!");
                throw new Exception("Ceva nu a mers bine la confirmarea programarii in repository!", er);
            }
        }

        public async Task<Result<List<CaAppointments>>> GetAppointmentsAsync(DateTime date)
        {
            try
            {
                var result = await _db.CaAppointments.Where(x => x.DataAppointment.ToShortDateString() == date.ToShortDateString() && !x.Deleted).ToListAsync();

                return Result<List<CaAppointments>>.ResultOk(result);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la gasirea programarii in functie de data in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea programarii in functie de data in repository!", er);
            }
        }

        public async Task<Result<CaServicetypes>> GetServiceTypeByIdAsync(uint id)
        {
            try
            {
                var value = await _db.CaServicetypes.FirstAsync(x => x.Id == id);
                return Result<CaServicetypes>.ResultOk(value);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la gasirea tipului de serviciu in functie de id in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea tipului de serviciu in functie de id in repository!", er);
            }

        }

        public async Task<Result<List<CaServicetypes>>> GetServiceTypesAsync()
        {
            try
            {
                var value = await _db.CaServicetypes.ToListAsync();
                return Result<List<CaServicetypes>>.ResultOk(value);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la gasirea tipului de serviciu in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea tipului de serviciu in repository!", er);
            }

        }

        public async Task<Result<CaServicetypes>> AddServiceTypeAsync(CaServicetypes serviciu)
        {
            try
            {
                var value = await _db.CaServicetypes.AddAsync(serviciu);
                await _db.SaveChangesAsync();
                return Result<CaServicetypes>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la adaugarea tipului de serviciu in repository!");
                throw new Exception("Ceva nu a mers bine la adaugarea tipului de serviciu in repository!", er);
            }
        }

        public async Task<Result<CaServicetypes>> GetServiceTypeByLableAsync(string label)
        {
            try
            {
                label = string.IsNullOrEmpty(label) ? string.Empty : label.Trim();
                var value = await _db.CaServicetypes.FirstOrDefaultAsync(x => x.Label.ToLower().Contains(label.ToLower()));
                return Result<CaServicetypes>.ResultOk(value);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la gasirea tipului de serviciu in functie de label in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea tipului de serviciu in functie de label in repository!", er);
            }

        }
    }
}
