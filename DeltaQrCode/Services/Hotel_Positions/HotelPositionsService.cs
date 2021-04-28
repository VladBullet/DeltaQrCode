using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeltaQrCode.Models;
using DeltaQrCode.ModelsDto;
using DeltaQrCode.Repositories.Hotel_Positions;
using Serilog;

namespace DeltaQrCode.Services.Hotel_Positions
{
    public class HotelPositionsService : IHotelPositionsService
    {

        private readonly IHotelPositionsRepository _hotelPositionsRepository;
        private readonly IMapper _mapper;

        public HotelPositionsService(IHotelPositionsRepository hotelPositionsRepository, IMapper mapper)
        {
            _hotelPositionsRepository = hotelPositionsRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<HotelPositionsDto>>> GetAvailablePositionsAsync(int? nrbuc = null)
        {
            try
            {
                var value = await _hotelPositionsRepository.GetAvailablePositionsAsync(nrbuc);
                var model = _mapper.Map<List<HotelPositionsDto>>(value.Entity);

                return Result<List<HotelPositionsDto>>.ResultOk(model);
            }

            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea pozitiilor in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea pozitiilor in servicii!", er);
            }
        }

        public async Task<Result<HotelPositionsDto>> GetPositionByIdAsync(uint id)
        {
            try
            {
                var value = await _hotelPositionsRepository.GetPositionByIdAsync(id);
                var model = _mapper.Map<HotelPositionsDto>(value.Entity);
                return Result<HotelPositionsDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea pozitiei in functie de id in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea pozitiei in functie de id in servicii!", er);
            }
        }

        public Result<HotelPositionsDto> UpdatePosition(uint id, int nrbuc, OperatiunePozitie op)
        {
            try
            {
                var value = new HotelPositionsDto();
                var result = new Result<CaHotelPositions>();
                switch (op)
                {
                    case OperatiunePozitie.Adaugare:
                        result = _hotelPositionsRepository.PunePePozitie(id, nrbuc);
                        break;
                    case OperatiunePozitie.Scoatere:
                        result = _hotelPositionsRepository.ElibereazaPozitie(id, nrbuc);
                        break;
                    case OperatiunePozitie.Setare:
                        result = _hotelPositionsRepository.SeteazaPozitia(id, nrbuc);
                        break;
                    default:
                        Log.Error("Ceva nu a mers bine la modificarea pozitiei in servicii!");
                        throw new Exception("Nu am putut modifica pozitia!");
                }
                value = _mapper.Map<HotelPositionsDto>(result.Entity);

                return Result<HotelPositionsDto>.ResultOk(value);
            }

            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la modificarea pozitiei in servicii!");
                throw new Exception("Ceva nu a mers bine la modificarea pozitiei in servicii!", er);
            }
        }
    }

}

