using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using DeltaQrCode.Models;

    public interface IHotelAnvelopeRepository
    {
        Task<Result<CaSetAnvelope>> GetSetAnvelopeByIdAsync(int id);
        Result<CaSetAnvelope> AddSetAnvelopeAsync(CaSetAnvelope setAnv);
        Result<CaSetAnvelope> UpdateSetAnvelopeAsync(CaSetAnvelope setAnv);
        Task<Result<List<CaSetAnvelope>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Task<Result<CaSetAnvelope>> DeleteSetAnvelopeAsync(int id);
        Task<Result<CaMarca>> GetMarcaByIdAsync(uint id);
        Task<Result<List<CaMarca>>> GetMarciAsync(); 
        Task<Result<CaMarca>> AddMarcaAsync(CaMarca marca);
        Task<Result<CaMarca>> GetMarcaByLableAsync(string label);
        Task<Result<CaFlota>> GetFlotaByIdAsync(uint id);
        Task<Result<List<CaFlota>>> GetFlotaAsync();
        Task<Result<CaFlota>> AddFlotaAsync(CaFlota flota);
        Task<Result<CaFlota>> GetFlotaByLableAsync(string label);

    }
}
