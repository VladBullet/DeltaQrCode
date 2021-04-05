﻿using System;
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

        public async Task<Result<List<CaSetAnvelope>>> GetAllSetAnvelopeAsync()
        {
            try
            {
                var list = await _db.CaSetAnvelope.Where(x => !x.Deleted).OrderBy(s => s.NumarInmatriculare).ThenBy(x => x.DataUltimaModificare).ToListAsync();
                return Result<List<CaSetAnvelope>>.ResultOk(list);
            }
            catch (Exception e)
            {
                return Result<List<CaSetAnvelope>>.ResultError(null, e, "Ceva nu a mers bine la gasirea anvelopelor!");

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



        public async Task<Result<List<CaSetAnvelope>>> SearchAnvelopeAsync(string searchString, int page = 1, int itemsPerPage = 20)
        {
            try
            {
                var list = await _db.CaSetAnvelope.Where(x => !x.Deleted).ToListAsync();
                if (!string.IsNullOrEmpty(searchString))
                {
                    list = list.Where(x => x.NumeClient.ToLower().Contains(searchString.ToLower()) || x.NumarInmatriculare.ToLower().Contains(searchString.ToLower())).ToList();
                }
                return Result<List<CaSetAnvelope>>.ResultOk(list);
            }
            catch (Exception e)
            {
                return Result<List<CaSetAnvelope>>.ResultError(null, e, "Ceva nu a mers bine la gasirea anvelopelor!");

            }
        }

        public async Task<Result<CaSetAnvelope>> DeleteSetAnvelopeAsync(int id)
        {
            try
            {
                var entity = await _db.CaSetAnvelope.FirstAsync(x => x.Id == id);
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

        public async Task<Result<List<Position>>> GetAvailablePositionsAsync(string searchString = null)
        {
            try
            {
                var occupiedPositions = await _db.CaSetAnvelope.Select(x => new Position(x.Rand, x.Pozitie, x.Interval)).ToListAsync();
                var allCombinations = Helpers.GetAllCombinationsRowsAndPositionsAndIntervals();
                var availablePositions = allCombinations.Where(p => !occupiedPositions.Any(p2 => p2.Rand == p.Rand && p2.Poz == p.Poz && p2.Interval == p.Interval)).ToList();



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



        public async Task<Result<CaMarca>> GetMarcaByIdAsync(uint id)
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
        public async Task<Result<List<CaMarca>>> GetMarciAsync()
        {
            try
            {
                var value = await _db.CaMarca.ToListAsync();
                return Result<List<CaMarca>>.ResultOk(value);
            }
            catch (Exception er)
            {
                return Result<List<CaMarca>>.ResultError(null, er, "Ceva nu a mers bine gasirea marcilor!");
            }

        }

        public async Task<Result<CaMarca>> AddMarcaAsync(CaMarca marca)
        {
            try
            {
                var value = await _db.CaMarca.AddAsync(marca);
                await _db.SaveChangesAsync();
                return Result<CaMarca>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                return Result<CaMarca>.ResultError(null, er, "Ceva nu a mers bine la adaugarea marcii!");
            }
        }

        public async Task<Result<CaMarca>> GetMarcaByLableAsync(string label)
        {
            try
            {
                label = string.IsNullOrEmpty(label) ? string.Empty : label.Trim();
                var value = await _db.CaMarca.FirstOrDefaultAsync(x => x.Label.ToLower().Contains(label.ToLower()));
                return Result<CaMarca>.ResultOk(value);
            }
            catch (Exception er)
            {
                return Result<CaMarca>.ResultError(null, er, "Ceva nu a mers bine gasirea marcilor!");
            }

        }




        public async Task<Result<CaFlota>> GetFlotaByIdAsync(uint id)
        {
            try
            {
                var value = await _db.CaFlota.FirstAsync(x => x.Id == id);
                return Result<CaFlota>.ResultOk(value);
            }
            catch (Exception er)
            {
                return Result<CaFlota>.ResultError(null, er, "Ceva nu a mers bine la gasirea setului de anvelope!");
            }

        }

        public async Task<Result<List<CaFlota>>> GetFlotaAsync()
        {
            try
            {
                var value = await _db.CaFlota.ToListAsync();
                return Result<List<CaFlota>>.ResultOk(value);
            }
            catch (Exception er)
            {
                return Result<List<CaFlota>>.ResultError(null, er, "Ceva nu a mers bine gasirea flotelor!");
            }

        }

        public async Task<Result<CaFlota>> AddFlotaAsync(CaFlota flota)
        {
            try
            {
                var value = await _db.CaFlota.AddAsync(flota);
                await _db.SaveChangesAsync();
                return Result<CaFlota>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                return Result<CaFlota>.ResultError(null, er, "Ceva nu a mers bine la adaugarea flotei!");
            }
        }

        public async Task<Result<CaFlota>> GetFlotaByLableAsync(string label)
        {
            try
            {
                label = string.IsNullOrEmpty(label) ? string.Empty : label.Trim();
                var value = await _db.CaFlota.FirstOrDefaultAsync(x => x.Label.ToLower().Contains(label.ToLower()));
                return Result<CaFlota>.ResultOk(value);
            }
            catch (Exception er)
            {
                return Result<CaFlota>.ResultError(null, er, "Ceva nu a mers bine gasirea flotelor!");
            }

        }
    }
}
