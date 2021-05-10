using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using DeltaQrCode.Data;
    using DeltaQrCode.Models;
    using DeltaQrCode.Repositories.Hotel_Positions;
    using Microsoft.EntityFrameworkCore;
    using Serilog;

    public class HotelAnvelopeRepository : IHotelAnvelopeRepository
    {
        private ApplicationDbContext _db;
        private readonly IHotelPositionsRepository _hotelPositionRepository;

        public HotelAnvelopeRepository(ApplicationDbContext db, IHotelPositionsRepository hotelPositionsRepository)
        {
            _db = db;
            _hotelPositionRepository = hotelPositionsRepository;
        }


        public async Task<Result<CaAnvelopa>> GetAnvelopaByIdAsync(int id)
        {
            try
            {
                var value = await _db.CaAnvelopa.FirstAsync(x => x.Id == id && !x.Deleted);
                return Result<CaAnvelopa>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea setului de anvelope in functie de id in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea setului de anvelope in functie de id in repository!", er);
            }
        }


        public async Task<Result<List<CaAnvelopa>>> GetAllAnvelopaAsync()
        {
            try
            {
                var list = await _db.CaAnvelopa.Where(x => !x.Deleted).OrderBy(x => x.DataUltimaModificare).ToListAsync();
                return Result<List<CaAnvelopa>>.ResultOk(list);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea tuturor seturilor de anvelope in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea tuturor seturilor de anvelope in repository!", er);
            }
        }


        public async Task<Result<CaAnvelopa>> AddAnvelopaAsync(CaAnvelopa setAnv)
        {
            try
            {
                var value = await _db.CaAnvelopa.AddAsync(setAnv);
                await _db.SaveChangesAsync();
                return Result<CaAnvelopa>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea setului de anvelope in repository!");
                throw new Exception("Ceva nu a mers bine la adaugarea setului de anvelope in repository!", er);
            }
        }


        public async Task<Result<CaAnvelopa>> UpdateAnvelopaAsync(CaAnvelopa setAnv)
        {
            try
            {
                _db.CaAnvelopa.Update(setAnv);
                await _db.SaveChangesAsync();

                return Result<CaAnvelopa>.ResultOk(setAnv);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la editarea setului de anvelope in repository!");
                throw new Exception("Ceva nu a mers bine la editarea setului de anvelope in repository!", er);
            }
        }


        public async Task<Result<List<CaAnvelopa>>> SearchAnvelopeAsync(string searchString, int page = 1, int itemsPerPage = 20)
        {
            try
            {
                var flote = _db.CaFlota.Where(x => x.Label.ToLower().Contains(searchString.ToLower()));
                var list = await _db.CaAnvelopa.Where(x => !x.Deleted).ToListAsync();
                //if (!string.IsNullOrEmpty(searchString))
                //{
                //    list = list.Where(x => x.NumeClient.ToLower().Contains(searchString.ToLower()) || flote.Any(y => y.Id == x.FlotaId) || x.NumarInmatriculare.ToLower().Contains(searchString.ToLower()) || x.SerieSasiu.ToLower().Contains(searchString.ToLower())).ToList();
                //}
                return Result<List<CaAnvelopa>>.ResultOk(list);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la cautarea setului de anvelope in repository!");
                throw new Exception("Ceva nu a mers bine la cautarea setului de anvelope in repository!", er);
            }
        }

        public async Task<Result<CaAnvelopa>> DeleteAnvelopaAsync(int id)
        {
            try
            {
                var entity = await _db.CaAnvelopa.FirstAsync(x => x.Id == id);
                entity.Deleted = true;
                var value = _db.CaAnvelopa.Update(entity);
                await _db.SaveChangesAsync();

                return Result<CaAnvelopa>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la stergerea setului de anvelope in repository!");
                throw new Exception("Ceva nu a mers bine la stergerea setului de anvelope in repository!", er);
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
                Log.Error(er, "Ceva nu a mers bine la gasirea marcii in functie de id in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea marcii in functie de id in repository!", er);
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
                Log.Error(er, "Ceva nu a mers bine la gasirea marcii in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea marcii in repository!", er);
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
                Log.Error(er, "Ceva nu a mers bine la adaugarea marcii in repository!");
                throw new Exception("Ceva nu a mers bine la adaugarea marcii in repository!", er);
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
                Log.Error(er, "Ceva nu a mers bine la gasirea marcii in functie de label in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea marcii in functie de label in repository!", er);
            }

        }


        //public async Task<Result<CaFlota>> GetFlotaByIdAsync(uint id)
        //{
        //    try
        //    {
        //        var value = await _db.CaFlota.FirstAsync(x => x.Id == id);
        //        return Result<CaFlota>.ResultOk(value);
        //    }
        //    catch (Exception er)
        //    {
        //        Log.Error(er, "Ceva nu a mers bine la gasirea flotei in functie de id in repository!");
        //        throw new Exception("Ceva nu a mers bine la gasirea flotei in functie de id in repository!", er);
        //    }

        //}


        //public async Task<Result<List<CaFlota>>> GetFlotaAsync()
        //{
        //    try
        //    {
        //        var value = await _db.CaFlota.ToListAsync();
        //        return Result<List<CaFlota>>.ResultOk(value);
        //    }
        //    catch (Exception er)
        //    {
        //        Log.Error(er, "Ceva nu a mers bine la gasirea flotei in repository!");
        //        throw new Exception("Ceva nu a mers bine la gasirea flotei in repository!", er);
        //    }

        //}


        //public async Task<Result<CaFlota>> AddFlotaAsync(CaFlota flota)
        //{
        //    try
        //    {
        //        var value = await _db.CaFlota.AddAsync(flota);
        //        await _db.SaveChangesAsync();
        //        return Result<CaFlota>.ResultOk(value.Entity);

        //    }
        //    catch (Exception er)
        //    {
        //        Log.Error(er, "Ceva nu a mers bine la adaugarea flotei in repository!");
        //        throw new Exception("Ceva nu a mers bine la adaugarea flotei in repository!", er);
        //    }
        //}


        //public async Task<Result<CaFlota>> GetFlotaByLableAsync(string label)
        //{
        //    try
        //    {
        //        label = string.IsNullOrEmpty(label) ? string.Empty : label.Trim();
        //        var value = await _db.CaFlota.FirstOrDefaultAsync(x => x.Label.ToLower().Contains(label.ToLower()));
        //        return Result<CaFlota>.ResultOk(value);
        //    }
        //    catch (Exception er)
        //    {
        //        Log.Error(er, "Ceva nu a mers bine la gasirea flotei in functie de label in repository!");
        //        throw new Exception("Ceva nu a mers bine la gasirea flotei in functie de label in repository!", er);
        //    }

        //}

        public Task<Result<CaMasina>> GetMasinaByIdAsync(uint id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<CaMarca>> AddMasinaAsync(CaMarca marca)
        {
            throw new NotImplementedException();
        }
    }
}
