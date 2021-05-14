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
                Log.Error(er, "Ceva nu a mers bine la gasirea anvelopei in functie de id in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea anvelopei in functie de id in repository!", er);
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
                Log.Error(er, "Ceva nu a mers bine la gasirea tuturor anvelopelor in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea tuturor anvelopelor in repository!", er);
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
                Log.Error(er, "Ceva nu a mers bine la adaugarea anvelopei in repository!");
                throw new Exception("Ceva nu a mers bine la adaugarea anvelopei in repository!", er);
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
                Log.Error(er, "Ceva nu a mers bine la editarea anvelopei in repository!");
                throw new Exception("Ceva nu a mers bine la editarea anvelopei in repository!", er);
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
                Log.Error(er, "Ceva nu a mers bine la cautarea anvelopei in repository!");
                throw new Exception("Ceva nu a mers bine la cautarea anvelopei in repository!", er);
            }
        }

        public async Task<Result<List<CaSetAnvelope>>> SearchAnvelopeSetAsync()
        {
            try
            {
                var list = await _db.CaSetAnvelope.ToListAsync();

                return Result<List<CaSetAnvelope>>.ResultOk(list);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la cautarea anvelopei in repository!");
                throw new Exception("Ceva nu a mers bine la cautarea anvelopei in repository!", er);
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
                Log.Error(er, "Ceva nu a mers bine la stergerea anvelopei in repository!");
                throw new Exception("Ceva nu a mers bine la stergerea anvelopei in repository!", er);
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//


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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public async Task<Result<CaSetAnvelope>> GetSetAnvelopeByIdAsync(uint id)
        {
            try
            {
                var value = await _db.CaSetAnvelope.FirstAsync(x => x.Id == id);
                return Result<CaSetAnvelope>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea setului de anvelope in functie de id in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea setului de anvelope in functie de id in repository!", er);
            }
        }

        public async Task<Result<CaSetAnvelope>> GetSetAnvelopeByClientIdAsync(uint clientId)
        {
            try
            {
                var value = await _db.CaSetAnvelope.FirstAsync(x => x.ClientId == clientId);
                return Result<CaSetAnvelope>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea setului de anvelope in functie de clientId in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea setului de anvelope in functie de clientId in repository!", er);
            }
        }

        public async Task<Result<CaSetAnvelope>> GetSetAnvelopeByMasinaIdAsync(uint masinaId)
        {
            try
            {
                var value = await _db.CaSetAnvelope.FirstAsync(x => x.MasinaId == masinaId);
                return Result<CaSetAnvelope>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea setului de anvelope in functie de masinaId in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea setului de anvelope in functie de masinaId in repository!", er);
            }
        }

        public async Task<Result<CaSetAnvelope>> AddSetAnvelopeAsync(CaSetAnvelope setAnvelope)
        {
            try
            {
                var value = await _db.CaSetAnvelope.AddAsync(setAnvelope);
                await _db.SaveChangesAsync();
                return Result<CaSetAnvelope>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea setului de anvelope in repository!");
                throw new Exception("Ceva nu a mers bine la adaugarea setului de anvelope in repository!", er);
            }
        }

        public async Task<Result<CaSetAnvelope>> EditSetAnvelopeAsync(CaSetAnvelope setAnvelope)
        {
            try
            {
                _db.CaSetAnvelope.Update(setAnvelope);
                await _db.SaveChangesAsync();

                return Result<CaSetAnvelope>.ResultOk(setAnvelope);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la editarea setului de anvelope in repository!");
                throw new Exception("Ceva nu a mers bine la editarea setului de anvelope in repository!", er);
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public async Task<Result<CaMasina>> GetMasinaByIdAsync(uint id)
        {
            try
            {
                var value = await _db.CaMasina.FirstAsync(x => x.Id == id);
                return Result<CaMasina>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea masinii in functie de id in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea masinii in functie de id in repository!", er);
            }
        }

        public async Task<Result<CaMasina>> GetMasinaByNrAutoAsync(string nrAuto)
        {
            try
            {
                var value = await _db.CaMasina.FirstOrDefaultAsync(x => x.NumarInmatriculare.ToLower().Contains(nrAuto.ToLower()));
                return Result<CaMasina>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea masinii in functie de NrAuto in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea masinii in functie de NrAuto in repository!", er);
            }
        }

        public async Task<Result<CaMasina>> GetMasinaBySerieSasiuAsync(string serieSasiu)
        {
            try
            {
                var value = await _db.CaMasina.FirstOrDefaultAsync(x => x.SerieSasiu.ToLower().Contains(serieSasiu.ToLower()));
                return Result<CaMasina>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea masinii in functie de serieSasiu in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea masinii in functie de serieSasiu in repository!", er);
            }
        }

        public async Task<Result<CaMasina>> GetMasinaForSetIdAsync(uint setId)
        {
            try
            {
                var value = await _db.CaSetAnvelope.FirstOrDefaultAsync(x => x.Id == setId);
                var masina = await _db.CaMasina.FirstOrDefaultAsync(x => x.Id == value.MasinaId);
                return Result<CaMasina>.ResultOk(masina);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea masinii in functie de serieSasiu in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea masinii in functie de serieSasiu in repository!", er);
            }
        }

        public async Task<Result<CaMasina>> AddMasinaAsync(CaMasina masina)
        {
            try
            {
                var value = await _db.CaMasina.AddAsync(masina);
                await _db.SaveChangesAsync();
                return Result<CaMasina>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea masinii in repository!");
                throw new Exception("Ceva nu a mers bine la adaugarea masinii in repository!", er);
            }
        }

        public async Task<Result<CaMasina>> EditMasinaAsync(CaMasina masina)
        {
            try
            {
                _db.CaMasina.Update(masina);
                await _db.SaveChangesAsync();

                return Result<CaMasina>.ResultOk(masina);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la editarea masinii in repository!");
                throw new Exception("Ceva nu a mers bine la editarea masinii in repository!", er);
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public async Task<Result<CaClientHotel>> GetClientByIdAsync(uint id)
        {
            try
            {
                var value = await _db.CaClientHotel.FirstAsync(x => x.Id == id);
                return Result<CaClientHotel>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea clientului in functie de id in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea clientului in functie de id in repository!", er);
            }
        }

        public async Task<Result<CaClientHotel>> GetClientByNameAsync(string numeClient, string numarTelefon)
        {
            try
            {
                var value = await _db.CaClientHotel.FirstOrDefaultAsync(x => x.NumeClient.ToLower().Contains(numeClient.ToLower()) && x.NumarTelefon == numarTelefon);
                return Result<CaClientHotel>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea clientului in functie de numeClient in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea clientului in functie de numeClient in repository!", er);
            }
        }

        public async Task<Result<CaClientHotel>> GetClientForSetIdAsync(uint setId)
        {
            try
            {
                var value = await _db.CaSetAnvelope.FirstOrDefaultAsync(x => x.Id == setId);
                var client = await _db.CaClientHotel.FirstOrDefaultAsync(x => x.Id == value.ClientId);
                return Result<CaClientHotel>.ResultOk(client);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea masinii in functie de serieSasiu in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea masinii in functie de serieSasiu in repository!", er);
            }
        }

        public async Task<Result<CaClientHotel>> AddClientAsync(CaClientHotel client)
        {
            try
            {
                var value = await _db.CaClientHotel.AddAsync(client);
                await _db.SaveChangesAsync();
                return Result<CaClientHotel>.ResultOk(value.Entity);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea clientului in repository!");
                throw new Exception("Ceva nu a mers bine la adaugarea clientului in repository!", er);
            }
        }

        public async Task<Result<CaClientHotel>> EditClientAsync(CaClientHotel client)
        {
            try
            {
                _db.CaClientHotel.Update(client);
                await _db.SaveChangesAsync();

                return Result<CaClientHotel>.ResultOk(client);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la editarea clientului in repository!");
                throw new Exception("Ceva nu a mers bine la editarea clientului in repository!", er);
            }
        }

        public async Task<Result<List<CaAnvelopa>>> GetAnvelopeBySetIdAsync(uint setId)
        {
            try
            {
                var list = new List<CaAnvelopa>();
                list = await _db.CaAnvelopa.Where(x => x.SetAnvelopeId == setId).ToListAsync();
                return Result<List<CaAnvelopa>>.ResultOk(list);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea masinii in functie de serieSasiu in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea masinii in functie de serieSasiu in repository!", er);
            }
        }



        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    }
}
