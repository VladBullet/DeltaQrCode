using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaQrCode.Repositories
{
    using DeltaQrCode.Models;

    public interface IHotelAnvelopeRepository
    {
        Task<Result<CaAnvelopa>> GetAnvelopaByIdAsync(int id);
        Task<Result<CaAnvelopa>> AddAnvelopaAsync(CaAnvelopa setAnv);
        Task<Result<CaAnvelopa>> UpdateAnvelopaAsync(CaAnvelopa setAnv);
        Task<Result<List<CaAnvelopa>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Task<Result<CaAnvelopa>> DeleteAnvelopaAsync(int id);


        Task<Result<CaMarca>> GetMarcaByIdAsync(uint id);
        Task<Result<List<CaMarca>>> GetMarciAsync();
        Task<Result<CaMarca>> AddMarcaAsync(CaMarca marca);
        Task<Result<CaMarca>> GetMarcaByLableAsync(string label);


        //Task<Result<CaFlota>> GetFlotaByIdAsync(uint id);
        //Task<Result<List<CaFlota>>> GetFlotaAsync();
        //Task<Result<CaFlota>> AddFlotaAsync(CaFlota flota);
        //Task<Result<CaFlota>> GetFlotaByLableAsync(string label);

        Task<Result<CaSetAnvelope>> GetSetAnvelopeByIdAsync(int id);
        Task<Result<CaSetAnvelope>> GetSetAnvelopeByClientIdAsync(int clientId);
        Task<Result<CaSetAnvelope>> GetSetAnvelopeByMasinaIdAsync(int masinaId);

        Task<Result<CaMasina>> GetMasinaByIdAsync(int id);
        Task<Result<CaMasina>> GetMasinaByNrAutoAsync(string nrAuto);
        Task<Result<CaMasina>> GetMasinaBySerieSasiuAsync(string serieSasiu);
        Task<Result<CaMasina>> GetMasinaForSetIdAsync(int setId);
        Task<Result<CaMasina>> AddMasinaAsync(CaMasina masina);
        Task<Result<CaMasina>> EditMasinaAsync(CaMasina masina);

    }
}
