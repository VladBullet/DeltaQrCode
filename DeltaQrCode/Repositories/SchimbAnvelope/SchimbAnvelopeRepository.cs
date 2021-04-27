using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.Data;
using DeltaQrCode.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DeltaQrCode.Repositories.SchimbAnvelope
{
    using Microsoft.EntityFrameworkCore.Migrations.Operations;

    public class SchimbAnvelopeRepository : ISchimbAnvelopeRepository
    {
        private ApplicationDbContext _db;

        public SchimbAnvelopeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Result<CaOperatiuneSchimbAnvelope>> GetOperatiuneByIdAsync(int id)
        {
            try
            {
                var value = await _db.CaOperatiuneSchimbAnvelope.FirstAsync(x => x.Id == id);
                return Result<CaOperatiuneSchimbAnvelope>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea operatiunii in functie de id in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea operatiunii in functie de id in repository!", er);
            }
        }

        public async Task<Result<CaOperatiuneSchimbAnvelope>> FinalizareOperatiuneAsync(CaOperatiuneSchimbAnvelope schimb)
        {
            try
            {
                var value = await _db.CaOperatiuneSchimbAnvelope.AddAsync(schimb);
                await _db.SaveChangesAsync();
                return Result<CaOperatiuneSchimbAnvelope>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea operatiunii in repository!");
                throw new Exception("Ceva nu a mers bine la adaugarea operatiunii in repository!", er);
            }
        }

        public async Task<Result<CaOperatiuneSchimbAnvelope>> SetCurrentOperationStep(int operatiuneId, int pasId)
        {
            try
            {
                var operatiuneCurenta = await _db.CaOperatiuneSchimbAnvelope.FirstOrDefaultAsync(x => x.Id == operatiuneId);
                operatiuneCurenta.PasCurentOperatiuneId = pasId;
                await _db.SaveChangesAsync();
                return Result<CaOperatiuneSchimbAnvelope>.ResultOk(operatiuneCurenta);
            }

            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la setarea pasului operatiunii in repository!");
                throw new Exception("Ceva nu a mers bine la setarea pasului operatiunii in repository!", er);
            }

        }

        public async Task<Result<PasOperatiune>> PseudoSaveOperationStep(int userId, int operatiuneId, int step, string dataToSave)
        {
            try
            {
                PasOperatiune stepToInsert = new PasOperatiune();

                stepToInsert.UserId = userId;
                stepToInsert.InsertedDate = DateTime.Now;
                stepToInsert.OperatiuneId = operatiuneId;
                stepToInsert.Pas = step;
                stepToInsert.SavedData = dataToSave;

                var added = await _db.PasOperatiune.AddAsync(stepToInsert);
                await _db.SaveChangesAsync();
                return Result<PasOperatiune>.ResultOk(stepToInsert);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la pseudoSave step in repository!");
                throw new Exception("Ceva nu a mers bine la pseudoSave step in repository!", er);
            }
        }

        public async Task<Result<CaOperatiuneSchimbAnvelope>> GetOperationStep(int pas)
        {
            try
            {

                var value = await _db.CaOperatiuneSchimbAnvelope.OrderByDescending(x => x.OraInceput).FirstOrDefaultAsync(x => x.PasCurentOperatiuneId == pas && !x.OperatiuneFinalizata);
                return Result<CaOperatiuneSchimbAnvelope>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea operatiunii in functie de id in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea operatiunii in functie de id in repository!", er);
            }
        }
    }
}
