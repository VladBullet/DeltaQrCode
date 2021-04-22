using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.Models;

namespace DeltaQrCode.Repositories.Hotel_Positions
{
    public interface IHotelPositionsRepository
    {
        Task<Result<List<CaHotelPositions>>> GetAvailablePositionsAsync(int? nrbuc = null);
    }
}
