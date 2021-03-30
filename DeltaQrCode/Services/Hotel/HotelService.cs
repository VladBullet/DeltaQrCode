using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services.Hotel
{
    using System.Net.Http.Headers;

    using AutoMapper;

    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;
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

        public async Task<Result<SetAnvelopeDto>> GetSetAnvelopeByIdAsync(int id)
        {
            try
            {
                var value = await _hotelRepository.GetSetAnvelopeByIdAsync(id);
                var model = _mapper.Map<SetAnvelopeDto>(value);
                return Result<SetAnvelopeDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                return Result<SetAnvelopeDto>.ResultError(null, er, "Ceva nu a mers bine la gasirea setului de anvelope!");
            }
        }
        public async Task<Result<SetAnvelopeDto>> AddSetAnvelopeAsync(SetAnvelopeDto setAnv)
        {
            try
            {
                var modelForDatabase = _mapper.Map<CaSetAnvelope>(setAnv);

                // Set right position
                var position = setAnv.Position;
                modelForDatabase.Pozitie = position.Poz;
                modelForDatabase.Rand = position.Rand;


                // setare dimensiuni
                modelForDatabase.Dimensiuni = setAnv.Dimensiuni.ToJson();

                // setare uzura
                modelForDatabase.Uzura = setAnv.Uzura.ToJson();

                // send model to database
                var value = await _hotelRepository.AddSetAnvelopeAsync(modelForDatabase);
                var returnModel = _mapper.Map<SetAnvelopeDto>(value);
                return Result<SetAnvelopeDto>.ResultOk(returnModel);

            }
            catch (Exception er)
            {
                return Result<SetAnvelopeDto>.ResultError(null, er, "Ceva nu a mers bine la adaugarea setului de anvelope!");
            }
        }

        public async Task<Result<SetAnvelopeDto>> UpdateSetAnvelopeAsync(SetAnvelopeDto setAnv)
        {
            try
            {
                var modelForDatabase = _mapper.Map<CaSetAnvelope>(setAnv);

                var position = setAnv.Position;
                modelForDatabase.Pozitie = position.Poz;
                modelForDatabase.Rand = position.Rand;
                // setare dimensiuni
                modelForDatabase.Dimensiuni = setAnv.Dimensiuni.ToJson();

                // setare uzura
                modelForDatabase.Uzura = setAnv.Uzura.ToJson();

                var value = await _hotelRepository.UpdateSetAnvelopeAsync(modelForDatabase);
                var returnModel = _mapper.Map<SetAnvelopeDto>(value);
                return Result<SetAnvelopeDto>.ResultOk(returnModel);

            }
            catch (Exception er)
            {
                return Result<SetAnvelopeDto>.ResultError(null, er, "Ceva nu a mers bine la modificarea setului de anvelope!");
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
        public async Task<Result<List<SetAnvelopeDto>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage)
        {

            var result = await _hotelRepository.SearchAnvelopeAsync(searchString, page, itemsPerPage);
            var model = new List<SetAnvelopeDto>();
            if (result.Successful)
            {
               
                model = _mapper.Map<List<SetAnvelopeDto>>(result.Entity);
                // add marca as string
                //foreach (var item in model)
                //{
                //    item.Marca = _hotelRepository.GetMarcaByIdAsync().Label;
                //}
                return Result<List<SetAnvelopeDto>>.ResultOk(model);
            }
            return Result<List<SetAnvelopeDto>>.ResultError(model, null, "Eroare la citirea din serviciu!");
        }
        public async Task<Result<SetAnvelopeDto>> DeleteSetAnvelopeAsync(int id)
        {
            try
            {
                var value = await _hotelRepository.DeleteSetAnvelopeAsync(id);
                var model = _mapper.Map<SetAnvelopeDto>(value);

                return Result<SetAnvelopeDto>.ResultOk(model);

            }
            catch (Exception er)
            {
                return Result<SetAnvelopeDto>.ResultError(null, er, "Ceva nu a mers bine la stergerea setului de anvelope!");
            }
        }

    }
}
