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
        Task<Result<List<CaAnvelopa>>> SearchAnvelopeByStatusCurentAsync(string searchString, int page, int itemsPerPage);
        Task<Result<List<CaSetAnvelope>>> SearchAnvelopeSetAsync();
        Task<Result<CaAnvelopa>> DeleteAnvelopaAsync(int id);
        Task<Result<List<CaAnvelopa>>> GetAnvelopeBySetIdAsync(uint setId);


        Task<Result<CaMarca>> GetMarcaByIdAsync(uint id);
        Task<Result<List<CaMarca>>> GetMarciAsync();
        Task<Result<CaMarca>> AddMarcaAsync(CaMarca marca);
        Task<Result<CaMarca>> GetMarcaByLableAsync(string label);


        //Task<Result<CaFlota>> GetFlotaByIdAsync(uint id);
        //Task<Result<List<CaFlota>>> GetFlotaAsync();
        //Task<Result<CaFlota>> AddFlotaAsync(CaFlota flota);
        //Task<Result<CaFlota>> GetFlotaByLableAsync(string label);

        Task<Result<CaSetAnvelope>> GetSetAnvelopeByIdAsync(uint id);
        Task<Result<CaSetAnvelope>> GetSetAnvelopeByClientIdAsync(uint clientId);
        Task<Result<CaSetAnvelope>> GetSetAnvelopeByMasinaIdAsync(uint masinaId);
        Task<Result<CaSetAnvelope>> AddSetAnvelopeAsync(CaSetAnvelope setAnvelope);
        Task<Result<CaSetAnvelope>> EditSetAnvelopeAsync(CaSetAnvelope setAnvelope);

        Task<Result<CaMasina>> GetMasinaByIdAsync(uint id);
        Task<Result<CaMasina>> GetMasinaByNrAutoAsync(string nrAuto);
        Task<Result<CaMasina>> GetMasinaBySerieSasiuAsync(string serieSasiu);
        Task<Result<CaMasina>> GetMasinaForSetIdAsync(uint setId);
        Task<Result<CaMasina>> AddMasinaAsync(CaMasina masina);
        Task<Result<CaMasina>> EditMasinaAsync(CaMasina masina);

        Task<Result<CaClientHotel>> GetClientByIdAsync(uint id);
        Task<Result<CaClientHotel>> GetClientByNameAsync(string numeClient, string numarTelefon);
        Task<Result<CaClientHotel>> GetClientForSetIdAsync(uint setId);
        Task<Result<CaClientHotel>> AddClientAsync(CaClientHotel client);
        Task<Result<CaClientHotel>> EditClientAsync(CaClientHotel client);

    }
}
