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
                var value = await _db.CaHotelPositions.FirstAsync(x => x.Id == id);
                return Result<CaHotelPositions>.ResultOk(value);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea pozitiei in functie de id in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea pozitiei in functie de id in repository!", er);
            }
        }
    }
}
