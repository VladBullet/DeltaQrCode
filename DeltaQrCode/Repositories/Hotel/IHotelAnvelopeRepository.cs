using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using DeltaQrCode.Models;

    public interface IHotelAnvelopeRepository
    {

        Task<Result<CaSetAnvelope>> GetSetAnvelopeByIdAsync(int id);
        Task<Result<CaSetAnvelope>> AddSetAnvelopeAsync(CaSetAnvelope setAnv);
        Task<Result<CaSetAnvelope>> UpdateSetAnvelopeAsync(CaSetAnvelope setAnv);
        Task<Result<List<Position>>> GetAvailablePositionsAsync(string searchString = null);
        Task<Result<List<CaSetAnvelope>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Task<Result<CaSetAnvelope>> DeleteSetAnvelopeAsync(int id);
        Task<Result<CaMarca>> GetMarcaByIdAsync(uint id);
        Task<Result<List<CaSetAnvelope>>> GetAllSetAnvelopeAsync();

        Task<Result<List<CaMarca>>> GetMarciAsync(); 
        Task<Result<CaMarca>> AddMarcaAsync(CaMarca marca);
        Task<Result<CaMarca>> GetMarcaByLableAsync(string label);
    }
}
