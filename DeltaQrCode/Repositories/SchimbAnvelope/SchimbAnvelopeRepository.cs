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
    public class SchimbAnvelopeRepository:ISchimbAnvelopeRepository
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

        public async Task<Result<CaOperatiuneSchimbAnvelope>> SetOperationStep(int pas)
        {
            try
            {
                var pascurent = await _db.CaOperatiuneSchimbAnvelope.FirstAsync(x => x.PasCurentOperatiuneId == pas);
                return Result<CaOperatiuneSchimbAnvelope>.ResultOk(pascurent);
            }

            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la setarea pasului operatiunii in repository!");
                throw new Exception("Ceva nu a mers bine la setarea pasului operatiunii in repository!", er);
            }

        }

        public Task<Result<CaOperatiuneSchimbAnvelope>> SetOperationStep()
        {
            throw new NotImplementedException();
        }
    }
}
