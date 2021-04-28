using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using DeltaQrCode.Models;

    public interface IHotelAnvelopeRepository
    {
        Task<Result<CaSetAnvelope>> GetSetAnvelopeByIdAsync(int id);
        Result<CaSetAnvelope> AddSetAnvelope(CaSetAnvelope setAnv);
        Result<CaSetAnvelope> UpdateSetAnvelope(CaSetAnvelope setAnv);
        Task<Result<List<CaSetAnvelope>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Result<CaSetAnvelope> DeleteSetAnvelope(int id);
        Task<Result<CaMarca>> GetMarcaByIdAsync(uint id);
        Task<Result<List<CaMarca>>> GetMarciAsync();
        Result<CaMarca> AddMarca(CaMarca marca);
        Task<Result<CaMarca>> GetMarcaByLableAsync(string label);
        Task<Result<CaFlota>> GetFlotaByIdAsync(uint id);
        Task<Result<List<CaFlota>>> GetFlotaAsync();
        Result<CaFlota> AddFlota(CaFlota flota);
        Task<Result<CaFlota>> GetFlotaByLableAsync(string label);

    }
}
