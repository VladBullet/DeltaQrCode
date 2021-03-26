using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using DeltaQrCode.Data;
    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;

    using Microsoft.EntityFrameworkCore;

    public class HotelAnvelopeRepository : IHotelAnvelopeRepository
    {
        private ApplicationDbContext _db;

        public HotelAnvelopeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Result<CaSetAnvelope>> GetSetAnvelopeByIdAsync(int id)
        {
            try
            {
                var value = await _db.CaSetAnvelope.FirstAsync(x => x.Id == id && !x.Deleted);
                return Result<CaSetAnvelope>.ResultOk(value);
            }
            catch (Exception er)
            {
                return Result<CaSetAnvelope>.ResultError(null, er, "Ceva nu a mers bine la gasirea setului de anvelope!");
            }
        }

        public async Task<Result<CaSetAnvelope>> AddSetAnvelopeAsync(CaSetAnvelope setAnv)
        {
            try
            {
                var value = await _db.CaSetAnvelope.AddAsync(setAnv);
                await _db.SaveChangesAsync();
                return Result<CaSetAnvelope>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                return Result<CaSetAnvelope>.ResultError(null, er, "Ceva nu a mers bine la adaugarea setului de anvelope!");
            }
        }

        public async Task<Result<CaSetAnvelope>> UpdateSetAnvelopeAsync(CaSetAnvelope setAnv)
        {
            try
            {
                var value = _db.CaSetAnvelope.Update(setAnv);
                await _db.SaveChangesAsync();

                return Result<CaSetAnvelope>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                return Result<CaSetAnvelope>.ResultError(null, er, "Ceva nu a mers bine la modificarea setului de anvelope!");
            }
        }

        public async Task<Result<List<Position>>> GetAvailablePositionsAsync()
        {
            try
            {
                var occupiedPositions = await _db.CaSetAnvelope.Select(x => new Position(x.Rand, x.Pozitie)).ToListAsync();
                var allCombinations = Helpers.GetAllCombinationsRowsAndPositions();
                var availablePositions = allCombinations.Except(occupiedPositions).ToList();

                return Result<List<Position>>.ResultOk(availablePositions);
            }
            catch (Exception er)
            {
                return Result<List<Position>>.ResultError(null, er, "Ceva nu a mers bine la gasirea pozitiilor libere in raft!");
            }


        }

        public async Task<Result<List<CaSetAnvelope>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage)
        {
            throw new NotImplementedException();
            // should return first ItemsPerPage for page 1 if everything is null. I mean if search string is null or empty, should still return data.
            // make sure you also filter them to have Deleted == FALSE
        }

        public async Task<Result<CaSetAnvelope>> DeleteSetAnvelopeAsync(int id)
        {
            try
            {
                var entity = await _db.CaSetAnvelope.FindAsync(id);
                entity.Deleted = true;
                var value = _db.CaSetAnvelope.Update(entity);
                await _db.SaveChangesAsync();

                return Result<CaSetAnvelope>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                return Result<CaSetAnvelope>.ResultError(null, er, "Ceva nu a mers bine la stergerea setului de anvelope!");
            }
        }


    }
}
