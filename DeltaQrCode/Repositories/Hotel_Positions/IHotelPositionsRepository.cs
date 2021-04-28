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
        Result<List<CaHotelPositions>> GetAvailablePositions(int? nrbuc = null);
        Task<Result<CaHotelPositions>> GetPositionByIdAsync(uint id);
        Result<CaHotelPositions> GetPositionById(uint id);
        Result<CaHotelPositions> ElibereazaPozitie(uint id, int nrbuc);
        Result<CaHotelPositions> PunePePozitie(uint id, int nrbuc);
        Result<CaHotelPositions> SeteazaPozitia(uint id, int nrbuc);


    }
}
