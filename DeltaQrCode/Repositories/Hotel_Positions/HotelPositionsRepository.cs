using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.Data;
using DeltaQrCode.Models;
using Microsoft.EntityFrameworkCore;

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
            var availablepositions = await _db.CaHotelPositions.Where(x => !x.Ocupat).ToListAsync();
            if (nrbuc != null)
            {
                availablepositions = availablepositions.Where(x => nrbuc.Value <= ConstantsAndEnums.MaxLocuriPoz - x.Locuriocupate).ToList();
            }

            return Result<List<CaHotelPositions>>.ResultOk(availablepositions);
        }
    }
}
