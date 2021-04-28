using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using DeltaQrCode.Models;

    public interface IHotelAnvelopeRepository
    {
        Task<Result<CaSetAnvelope>> GetSetAnvelopeByIdAsync(int id);
        Result<CaSetAnvelope> GetSetAnvelopeById(int id);
        Result<CaSetAnvelope> AddSetAnvelope(CaSetAnvelope setAnv);
        Result<CaSetAnvelope> UpdateSetAnvelope(CaSetAnvelope setAnv);
        Task<Result<List<CaSetAnvelope>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Result<List<CaSetAnvelope>> SearchAnvelope(string searchString, int page, int itemsPerPage);
        Result<CaSetAnvelope> DeleteSetAnvelope(int id);
        Task<Result<CaMarca>> GetMarcaByIdAsync(uint id);
        Result<CaMarca> GetMarcaById(uint id);
        Task<Result<List<CaMarca>>> GetMarciAsync();
        Result<List<CaMarca>> GetMarci();
        Result<CaMarca> AddMarca(CaMarca marca);
        Task<Result<CaMarca>> GetMarcaByLableAsync(string label);
        Result<CaMarca> GetMarcaByLable(string label);
        Task<Result<CaFlota>> GetFlotaByIdAsync(uint id);
        Result<CaFlota> GetFlotaById(uint id);
        Task<Result<List<CaFlota>>> GetFlotaAsync();
        Result<List<CaFlota>> GetFlota();
        Result<CaFlota> AddFlota(CaFlota flota);
        Task<Result<CaFlota>> GetFlotaByLableAsync(string label);
        Result<CaFlota> GetFlotaByLable(string label);

    }
}
