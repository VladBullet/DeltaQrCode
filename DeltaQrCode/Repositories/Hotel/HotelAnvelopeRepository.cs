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
                //var value = _db.CaSetAnvelope.Update(setAnv);
                var value = await _db.CaSetAnvelope.FindAsync(setAnv.Id);
                value = setAnv;
                await _db.SaveChangesAsync();

                return Result<CaSetAnvelope>.ResultOk(value);

            }
            catch (Exception er)
            {
                return Result<CaSetAnvelope>.ResultError(null, er, "Ceva nu a mers bine la modificarea setului de anvelope!");
            }
        }

        public async Task<Result<List<Position>>> GetAvailablePositionsAsync(string searchString = null)
        {
            try
            {
                var occupiedPositions = await _db.CaSetAnvelope.Select(x => new Position(x.Rand, x.Pozitie, x.Interval)).ToListAsync();
                var allCombinations = Helpers.GetAllCombinationsRowsAndPositionsAndIntervals();
                var availablePositions = allCombinations.Except(occupiedPositions).ToList();

                if (!string.IsNullOrEmpty(searchString))
                {
                    availablePositions = availablePositions.Where(x => x.PositionString.ToLower().Contains(searchString.ToLower())).ToList();
                }

                return Result<List<Position>>.ResultOk(availablePositions);
            }
            catch (Exception er)
            {
                return Result<List<Position>>.ResultError(null, er, "Ceva nu a mers bine la gasirea pozitiilor libere in raft!");
            }


        }

        public async Task<Result<List<CaSetAnvelope>>> SearchAnvelopeAsync(string searchString, int page = 1, int itemsPerPage = 20)
        {
            try
            {
                var list = await _db.CaSetAnvelope.Where(x => !x.Deleted).ToListAsync();
                if (!string.IsNullOrEmpty(searchString))
                {
                    list = list.Where(x => x.NumeClient.ToLower().Contains(searchString.ToLower()) || x.NumarInmatriculare.ToLower().Contains(searchString.ToLower())).ToList();
                }
                list = list.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
                return Result<List<CaSetAnvelope>>.ResultOk(list);
            }
            catch (Exception e)
            {
                return Result<List<CaSetAnvelope>>.ResultError(null, e, "Ceva nu a mers bine la gasirea anvelopelor!");

            }
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

        public async Task<Result<CaMarca>> GetMarcaByIdAsync(int id)
        {
            try
            {
                var value = await _db.CaMarca.FirstAsync(x => x.Id == id);
                return Result<CaMarca>.ResultOk(value);
            }
            catch (Exception er)
            {
                return Result<CaMarca>.ResultError(null, er, "Ceva nu a mers bine la gasirea setului de anvelope!");
            }

        }


    }
}
