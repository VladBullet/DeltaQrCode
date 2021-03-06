﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.Models;

namespace DeltaQrCode.Repositories.Hotel_Positions
{
    public interface IHotelPositionsRepository
    {
        Task<Result<List<CaHotelPositions>>> GetAvailablePositionsAsync(int? nrbuc = null);
        Task<Result<CaHotelPositions>> GetPositionByIdAsync(uint id);
        Task<Result<CaHotelPositions>> ElibereazaPozitieAsync(uint id, int nrbuc);
        Task<Result<CaHotelPositions>> PunePePozitieAsync(uint id, int nrbuc);
        Task<Result<CaHotelPositions>> SeteazaPozitiaAsync(uint id, int nrbuc);

    }
}
