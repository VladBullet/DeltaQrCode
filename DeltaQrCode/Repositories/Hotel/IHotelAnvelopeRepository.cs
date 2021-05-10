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

        Task<Result<CaMasina>> GetMasinaByIdAsync(uint id);
        Task<Result<CaMarca>> AddMasinaAsync(CaMarca marca);

    }
}
