using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using DeltaQrCode.Data;
    using DeltaQrCode.Models;
    using Microsoft.EntityFrameworkCore;
    using Serilog;

    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly ApplicationDbContext _db;

        public AppointmentsRepository(ApplicationDbContext db)
        {
            _db = db;
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
                Log.Error(er, "Ceva nu a mers bine la gasirea programarii in functie de id in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea programarii in functie de id in repository!", er);
            }
        }

        public Result<CaAppointments> AddAppointment(CaAppointments appointment)
        {
            try
            {
                var value = _db.CaAppointments.Add(appointment);
                _db.SaveChanges();
                return Result<CaAppointments>.ResultOk(value.Entity);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea programarii in repository!");
                throw new Exception("Ceva nu a mers bine la adaugarea programarii in repository!", er);
            }

        }

        public Result<CaAppointments> UpdateAppointment(CaAppointments appointment)
        {
            try
            {
                var value = _db.CaAppointments.Update(appointment);
                _db.SaveChanges();

                return Result<CaAppointments>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la editarea programarii in repository!");
                throw new Exception("Ceva nu a mers bine la editarea programarii in repository!", er);
            }
        }

        public Result<CaAppointments> DeleteAppointment(int id)
        {
            try
            {
                var result = _db.CaAppointments.First(x => x.Id == id);
                result.Deleted = true;
                var value = _db.CaAppointments.Update(result);
                _db.SaveChanges();

                return Result<CaAppointments>.ResultOk(value.Entity);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la stergerea programarii in repository!");
                throw new Exception("Ceva nu a mers bine la stergerea programarii in repository!", er);
            }
        }

        public Result<CaAppointments> ConfirmAppointment(int id, bool confirm)
        {
            try
            {
                var result = _db.CaAppointments.FirstOrDefault(x => x.Id == id);
                result.Confirmed = confirm;
                result.ConfirmedDate = DateTime.Now;
                _db.SaveChanges();
                return Result<CaAppointments>.ResultOk(result);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la confirmarea programarii in repository!");
                throw new Exception("Ceva nu a mers bine la confirmarea programarii in repository!", er);
            }
        }

        public async Task<Result<List<CaAppointments>>> GetAppointmentsAsync(DateTime date, int? rampId = null)
        {
            try
            {
                var rampValue = 0;
                if (rampId != null && rampId != 0)
                {
                    rampValue = rampId.Value;
                }

                var result = await _db.CaAppointments.Where(x => x.DataAppointment.ToShortDateString() == date.ToShortDateString() && !x.Deleted).ToListAsync();
                if (rampValue != 0)
                {
                    result = result.Where(x => x.RampId == rampValue).ToList();
                }

                return Result<List<CaAppointments>>.ResultOk(result);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea programarii in functie de data in repository!");
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
                Log.Error(er, "Ceva nu a mers bine la gasirea tipului de serviciu in functie de id in repository!");
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
                Log.Error(er, "Ceva nu a mers bine la gasirea tipului de serviciu in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea tipului de serviciu in repository!", er);
            }

        }

        public Result<CaServicetypes> AddServiceType(CaServicetypes serviciu)
        {
            try
            {
                var value = _db.CaServicetypes.Add(serviciu);
                _db.SaveChanges();
                return Result<CaServicetypes>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea tipului de serviciu in repository!");
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
                Log.Error(er, "Ceva nu a mers bine la gasirea tipului de serviciu in functie de label in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea tipului de serviciu in functie de label in repository!", er);
            }

        }
    }
}
