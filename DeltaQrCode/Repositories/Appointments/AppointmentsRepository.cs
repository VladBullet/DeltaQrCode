﻿using System;
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

        public async Task<Result<CaAppointments>> GetAppointmentByIdAsync(int id)
        {
            try
            {
                var result = await _db.CaAppointments.FirstAsync(x => x.Id == id && !x.Deleted);
                return Result<CaAppointments>.ResultOk(result);
            }
            catch (Exception e)
            {
                return Result<CaAppointments>.ResultError(e, "Ceva nu a mers bine la gasirea programarii!");
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
                return Result<CaAppointments>.ResultError(null, er, "Ceva nu a mers bine la adaugarea programarii!");
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
                return Result<CaAppointments>.ResultError(null, er, "Ceva nu a mers bine la modificarea programarii!");
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
                return Result<CaAppointments>.ResultError(null, er, "Ceva nu a mers bine la anularea programarii!");
            }
        }

        public async Task<Result<CaAppointments>> ConfirmAppointmentAsync(int id)
        {
            try
            {
                var result = await _db.CaAppointments.FirstAsync(x => x.Id == id);
                result.Confirmed = true;
                result.ConfirmedDate = DateTime.Now;
                await _db.SaveChangesAsync();
                return Result<CaAppointments>.ResultOk(result);
            }
            catch (Exception e)
            {
                return Result<CaAppointments>.ResultError(e, "Ceva nu a mers bine la confirmarea programarii!");
            }
        }

        public async Task<Result<List<CaAppointments>>> GetAppointmentsAsync(DateTime date)
        {
            try
            {
                var result = await _db.CaAppointments.Where(x => x.DataAppointment == date && !x.Deleted).ToListAsync();

                return Result<List<CaAppointments>>.ResultOk(result);
            }
            catch (Exception er)
            {
                return Result<List<CaAppointments>>.ResultError(null, er, "Ceva nu a mers bine la gasirea programarilor pentru data ceruta!");
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
                return Result<CaServicetypes>.ResultError(null, er, "Ceva nu a mers bine la gasirea tipului de serviciu!");
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
                return Result<List<CaServicetypes>>.ResultError(null, er, "Ceva nu a mers bine gasirea tipurilor de servicii!");
            }

        }
    }
}
