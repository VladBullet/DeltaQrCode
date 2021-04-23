using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.Data;
using DeltaQrCode.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DeltaQrCode.Repositories.Hotel_Positions
{
    public class HotelPositionsRepository : IHotelPositionsRepository
    {
        private ApplicationDbContext _db;

        public HotelPositionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }



        public async Task<Result<List<CaHotelPositions>>> GetAvailablePositionsAsync(int? nrbuc = null)
        {
            try
            {
                var availablepositions = await _db.CaHotelPositions.Where(x => !x.Ocupat).ToListAsync();
                if (nrbuc != null)
                {
                    availablepositions = availablepositions.Where(x => nrbuc.Value <= ConstantsAndEnums.MaxLocuriPoz - x.Locuriocupate).ToList();
                }

                return Result<List<CaHotelPositions>>.ResultOk(availablepositions);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea pozitiilor in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea pozitiilor in repository!", er);
            }
        }

        public async Task<Result<CaHotelPositions>> GetPositionByIdAsync(uint id)
        {
            try
            {
                var value = await _db.CaHotelPositions.FirstOrDefaultAsync(x => x.Id == id);
                return Result<CaHotelPositions>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea pozitiei in functie de id in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea pozitiei in functie de id in repository!", er);
            }
        }

        public async Task<Result<CaHotelPositions>> PunePePozitieAsync(uint id, int nrbuc)
        {
            try
            {
                var value = await _db.CaHotelPositions.FirstOrDefaultAsync(x => x.Id == id);
                value.Locuriocupate = value.Locuriocupate + nrbuc;
                await _db.SaveChangesAsync();

                if(value.Locuriocupate == ConstantsAndEnums.MaxLocuriPoz)
                {
                    value.Ocupat = true;
                }

                return Result<CaHotelPositions>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea nr bucati pe pozitie in repository!");
                throw new Exception("Ceva nu a mers bine la adaugarea nr bucati pe pozitie in repository!", er);
            }
        }

        public async Task<Result<CaHotelPositions>> ElibereazaPozitieAsync(uint id, int nrbuc)
        {
            try
            {
                var value = await _db.CaHotelPositions.FirstOrDefaultAsync(x => x.Id == id);
                value.Locuriocupate = value.Locuriocupate - nrbuc;
                await _db.SaveChangesAsync();

                if (value.Locuriocupate < ConstantsAndEnums.MaxLocuriPoz)
                {
                    value.Ocupat = false;
                }

                return Result<CaHotelPositions>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la scaderea nr bucati pe pozitie in repository!");
                throw new Exception("Ceva nu a mers bine la scaderea nr bucati pe pozitie in repository!", er);
            }
        }
    }
}
