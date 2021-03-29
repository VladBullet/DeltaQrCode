using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services.Hotel
{
    using AutoMapper;

    using DeltaQrCode.Models;
    using DeltaQrCode.Repositories;
    using DeltaQrCode.ViewModels;

    public class HotelService : IHotelService
    {
        private readonly IHotelAnvelopeRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelAnvelopeRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<Result<SetAnvelopeVM>> GetSetAnvelopeByIdAsync(int id)
        {
            try
            {
                var value = await _hotelRepository.GetSetAnvelopeByIdAsync(id);
                var model = _mapper.Map<SetAnvelopeVM>(value);
                return Result<SetAnvelopeVM>.ResultOk(model);
            }
            catch (Exception er)
            {
                return Result<SetAnvelopeVM>.ResultError(null, er, "Ceva nu a mers bine la gasirea setului de anvelope!");
            }
        }

    }
}
