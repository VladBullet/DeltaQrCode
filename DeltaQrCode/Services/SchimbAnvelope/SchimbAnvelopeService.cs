using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeltaQrCode.Models;
using DeltaQrCode.ModelsDto;
using DeltaQrCode.Repositories.SchimbAnvelope;
using Serilog;

namespace DeltaQrCode.Services.SchimbAnvelope
{
    public class SchimbAnvelopeService : ISchimbAnvelopeService
    {

        private readonly ISchimbAnvelopeRepository _schimbAnvelopeRepository;
        private readonly IMapper _mapper;

        public SchimbAnvelopeService(ISchimbAnvelopeRepository schimbAnvelopeRepository, IMapper mapper)
        {
            _schimbAnvelopeRepository = schimbAnvelopeRepository;
            _mapper = mapper;
        }

        public async Task<Result<SchimbAnvelopeDto>> FinalizareOperatiuneAsync(SchimbAnvelopeDto schimb)
        {
            try
            {
                var model = _mapper.Map<CaOperatiuneSchimbAnvelope>(schimb);
                var value = await _schimbAnvelopeRepository.FinalizareOperatiuneAsync(model);

                var returnModel = _mapper.Map<SchimbAnvelopeDto>(value.Entity);
                return Result<SchimbAnvelopeDto>.ResultOk(returnModel);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la finalizarea operatiunii in servicii!");
                throw new Exception("Ceva nu a mers bine la finalizarea operatiunii in servicii!", er);
            }
        }

        public async Task<Result<SchimbAnvelopeDto>> GetOperatiuneByIdAsync(int id)
        {
            try
            {
                var value = await _schimbAnvelopeRepository.GetOperatiuneByIdAsync(id);
                var model = _mapper.Map<SchimbAnvelopeDto>(value.Entity);
                return Result<SchimbAnvelopeDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea operatiunii in functie de id in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea operatiunii in functie de id in servicii!", er);
            }
        }

        public async Task<Result<SchimbAnvelopeDto>> SetOperationStep(int pas)
        {
            throw new NotImplementedException();
        }
    }
}
