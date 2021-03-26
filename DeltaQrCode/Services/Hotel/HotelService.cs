using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services.Hotel
{
    using DeltaQrCode.Repositories;

    public class HotelService : IHotelService
    {
        private readonly IHotelAnvelopeRepository _hotelRepository;

        public HotelService(IHotelAnvelopeRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

    }
}
