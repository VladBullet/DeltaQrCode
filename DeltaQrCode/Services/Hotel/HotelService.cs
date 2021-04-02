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
                var model = _mapper.Map<SetAnvelopeDto>(value.Entity);
                if (value.Entity.MarcaId != null)
                {
                    var marca = await _hotelRepository.GetMarcaByIdAsync(value.Entity.MarcaId.Value);
                    model.Marca = marca.Entity.Label;
                }
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
                var marca = await _hotelRepository.GetMarcaByLableAsync(setAnv.Marca);
                if (!marca.Successful)
                {
                    return Result<SetAnvelopeDto>.ResultError(marca.Error, "Problema la gasirea marcii!");
                }

                if (marca.Entity == null && !string.IsNullOrEmpty(setAnv.Marca))
                {
                    marca = await _hotelRepository.AddMarcaAsync(new CaMarca() { Label = setAnv.Marca });
                }

                if (!marca.Successful)
                {
                    return Result<SetAnvelopeDto>.ResultError(marca.Error, "Problema la adaugarea marcii!");
                }
                setAnv.MarcaId = marca.Entity.Id;

                var modelForDatabase = _mapper.Map<CaSetAnvelope>(setAnv);

                modelForDatabase.DataUltimaModificare = DateTime.Now;
                
                modelForDatabase.Deleted = false;
                

                // send model to database
                var value = await _hotelRepository.AddSetAnvelopeAsync(modelForDatabase);
                var returnModel = _mapper.Map<SetAnvelopeDto>(value.Entity);
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
                var marca = await _hotelRepository.GetMarcaByLableAsync(setAnv.Marca);
                if (!marca.Successful)
                {
                    return Result<SetAnvelopeDto>.ResultError(marca.Error, "Problema la gasirea marcii!");
                }

                if (marca.Entity == null && !string.IsNullOrEmpty(setAnv.Marca))
                {
                    marca = await _hotelRepository.AddMarcaAsync(new CaMarca() { Label = setAnv.Marca });
                }

                if (!marca.Successful)
                {
                    return Result<SetAnvelopeDto>.ResultError(marca.Error, "Problema la adaugarea marcii!");
                }
                setAnv.MarcaId = marca.Entity.Id;

                var modelForDatabase = _mapper.Map<CaSetAnvelope>(setAnv);

                modelForDatabase.DataUltimaModificare = DateTime.Now;

                var value = await _hotelRepository.UpdateSetAnvelopeAsync(modelForDatabase);
                var returnModel = _mapper.Map<SetAnvelopeDto>(value.Entity);
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
            try
            {


                var result = await _hotelRepository.SearchAnvelopeAsync(searchString, page, itemsPerPage);
                var model = new List<SetAnvelopeDto>();
                if (result.Successful)
                {

                    model = _mapper.Map<List<SetAnvelopeDto>>(result.Entity);

                    // add marca as string
                    foreach (var item in model)
                    {
                        if (item.MarcaId != null)
                        {
                            var marca = await _hotelRepository.GetMarcaByIdAsync(item.MarcaId.Value);
                            item.Marca = marca.Entity.Label;
                        }

                    }
                    return Result<List<SetAnvelopeDto>>.ResultOk(model);
                }
                return Result<List<SetAnvelopeDto>>.ResultError(model, null, "Eroare la citirea din serviciu!");
            }
            catch (Exception er)
            {
                return Result<List<SetAnvelopeDto>>.ResultError(null, er, "Eroare la citirea din serviciu!");

            }
        }
        public async Task<Result<SetAnvelopeDto>> DeleteSetAnvelopeAsync(int id)
        {
            try
            {
                var value = await _hotelRepository.DeleteSetAnvelopeAsync(id);
                var model = _mapper.Map<SetAnvelopeDto>(value.Entity);

                return Result<SetAnvelopeDto>.ResultOk(model);

            }
            catch (Exception er)
            {
                return Result<SetAnvelopeDto>.ResultError(null, er, "Ceva nu a mers bine la stergerea setului de anvelope!");
            }
        }

        public async Task<Result<List<CaMarca>>> GetMarci()
        {
            try
            {
                var result = await _hotelRepository.GetMarciAsync();
                return Result<List<CaMarca>>.ResultOk(result.Entity);
            }
            catch (Exception e)
            {
                return Result<List<CaMarca>>.ResultError(e);
            }
        }

    }
}
