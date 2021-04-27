using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.Models;
using DeltaQrCode.ModelsDto;

namespace DeltaQrCode.Services.Hotel_Positions
{
    public interface IHotelPositionsService
    {
        Task<Result<List<HotelPositionsDto>>> GetAvailablePositionsAsync(int? nrbuc = null);
        Task<Result<HotelPositionsDto>> GetPositionByIdAsync(int id);
        Result<HotelPositionsDto> UpdatePositionAsync(uint id, int nrbuc, OperatiunePozitie op );
    }
}
