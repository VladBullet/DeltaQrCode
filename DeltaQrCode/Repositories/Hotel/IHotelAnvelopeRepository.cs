using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using DeltaQrCode.Models;

    public interface IHotelAnvelopeRepository
    {
        Task<Result<CaSetAnvelope>> GetSetAnvelopeByIdAsync(int id);
        Task<Result<CaSetAnvelope>> AddSetAnvelopeAsync(CaSetAnvelope setAnv);
        Result<CaSetAnvelope> AddSetAnvelope(CaSetAnvelope setAnv);
        Task<Result<CaSetAnvelope>> UpdateSetAnvelopeAsync(CaSetAnvelope setAnv);
        Task<Result<List<CaSetAnvelope>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Task<Result<CaSetAnvelope>> DeleteSetAnvelopeAsync(int id);
        Task<Result<List<CaSetAnvelope>>> GetAllSetAnvelopeAsync();
        //Task<Result<List<Position>>> GetAvailablePositionsAsync(string searchString = null);

        Task<Result<CaMarca>> GetMarcaByIdAsync(uint id);
        Task<Result<List<CaMarca>>> GetMarciAsync(); 
        Task<Result<CaMarca>> AddMarcaAsync(CaMarca marca);
        Task<Result<CaMarca>> GetMarcaByLableAsync(string label);

        Task<Result<CaFlota>> GetFlotaByIdAsync(uint id);
        Task<Result<List<CaFlota>>> GetFlotaAsync();
        Task<Result<CaFlota>> AddFlotaAsync(CaFlota flota);
        Task<Result<CaFlota>> GetFlotaByLableAsync(string label);

        Task<Result<List<CaHotelPositions>>> GetAvailablePositionsAsync(int? nrbuc = null);
        Result<List<CaHotelPositions>> GetAvailablePositions(int? nrbuc = null);
        Task<Result<CaHotelPositions>> GetPositionByIdAsync(uint id);
        Result<CaHotelPositions> GetPositionById(uint id);
        Result<CaHotelPositions> ElibereazaPozitie(uint id, int nrbuc);
        Result<CaHotelPositions> PunePePozitie(uint id, int nrbuc);
        Result<CaHotelPositions> SeteazaPozitia(uint id, int nrbuc);

    }
}
