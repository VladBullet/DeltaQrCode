using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.Models;
using DeltaQrCode.ModelsDto;

namespace DeltaQrCode.Services.SchimbAnvelope
{
    public interface ISchimbAnvelopeService
    {
        Task<Result<SchimbAnvelopeDto>> GetOperatiuneByIdAsync(int id);
        Task<Result<SchimbAnvelopeDto>> FinalizareOperatiuneAsync(SchimbAnvelopeDto schimb);
        Task<Result<SchimbAnvelopeDto>> SetOperationStep(int pas);
    }
}
