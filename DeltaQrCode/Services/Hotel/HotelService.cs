using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services.Hotel
{
    using AutoMapper;

    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.Models;
    using DeltaQrCode.Repositories;
    using DeltaQrCode.ViewModels;

    using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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
        public async Task<Result<SetAnvelopeVM>> AddSetAnvelopeAsync(SetAnvelopeVM setAnv)
        {
            try
            {
                var set = _mapper.Map<CaSetAnvelope>(setAnv);
                set.Dimensiuni = "{ " + setAnv.DimensiuniString + " }";
                set.Uzura = "{ " + setAnv.UzuraString + " }";

                // Set right position
                var position = setAnv.Position;
                set.Pozitie = position.Poz;
                set.Rand = position.Rand;



                // set right uzura

                // set right dimensiuni

                // send model to database
                var value = await _hotelRepository.AddSetAnvelopeAsync(set);
                var model = _mapper.Map<SetAnvelopeVM>(value);
                return Result<SetAnvelopeVM>.ResultOk(model);

            }
            catch (Exception er)
            {
                return Result<SetAnvelopeVM>.ResultError(null, er, "Ceva nu a mers bine la adaugarea setului de anvelope!");
            }
        }

        public async Task<Result<SetAnvelopeVM>> UpdateSetAnvelopeAsync(SetAnvelopeVM setAnv)
        {
            try
            {
                var set = _mapper.Map<CaSetAnvelope>(setAnv);

                var position = setAnv.Position;
                set.Pozitie = position.Poz;
                set.Rand = position.Rand;

                var value = await _hotelRepository.UpdateSetAnvelopeAsync(set);
                var model = _mapper.Map<SetAnvelopeVM>(value);
                return Result<SetAnvelopeVM>.ResultOk(model);

            }
            catch (Exception er)
            {
                return Result<SetAnvelopeVM>.ResultError(null, er, "Ceva nu a mers bine la modificarea setului de anvelope!");
            }
        }

        public async Task<Result<List<Position>>> GetAvailablePositionsAsync()
        {
            try
            {
                var positions = await _hotelRepository.GetAvailablePositionsAsync();

                return positions;
            }
            catch (Exception er)
            {
                return Result<List<Position>>.ResultError(null, er, "Ceva nu a mers bine la gasirea pozitiilor libere in raft!");
            }
        }
        public async Task<Result<List<SetAnvelopeVM>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage)
        {
            throw new NotImplementedException();
        }
        public async Task<Result<SetAnvelopeVM>> DeleteSetAnvelopeAsync(int id)
        {
            try
            {
                var value = await _hotelRepository.DeleteSetAnvelopeAsync(id);
                var model = _mapper.Map<SetAnvelopeVM>(value);

                return Result<SetAnvelopeVM>.ResultOk(model);

            }
            catch (Exception er)
            {
                return Result<SetAnvelopeVM>.ResultError(null, er, "Ceva nu a mers bine la stergerea setului de anvelope!");
            }
        }

    }
}
