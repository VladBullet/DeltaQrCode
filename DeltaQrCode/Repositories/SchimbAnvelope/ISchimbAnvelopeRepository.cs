using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.Models;

namespace DeltaQrCode.Repositories.SchimbAnvelope
{
    public interface ISchimbAnvelopeRepository
    {
        Task<Result<CaOperatiuneSchimbAnvelope>> GetOperatiuneByIdAsync(int id);
        Result<CaOperatiuneSchimbAnvelope> FinalizareOperatiune(CaOperatiuneSchimbAnvelope schimb);

        //Task<Result<CaOperatiuneSchimbAnvelope>> SetOperationStep(int pas);
        //Task<Result<CaOperatiuneSchimbAnvelope>> GetOperationStep(int pas);
    }
}
