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
        Task<Result<CaOperatiuneSchimbAnvelope>> FinalizareOperatiuneAsync(CaOperatiuneSchimbAnvelope schimb);
        Task<Result<CaOperatiuneSchimbAnvelope>> SetOperationStep();
    }
}
