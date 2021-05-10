using System.Collections.Generic;
using System.Threading.Tasks;
using DeltaQrCode.Models;

namespace DeltaQrCode.Services.Hotel
{
    using System.Data;
    using DeltaQrCode.ModelsDto;

    public interface IHotelService
    {
        Task<Result<AnvelopDto>> GetAnvelopaByIdAsync(int id);
        Task<Result<AnvelopDto>> AddAnvelopaAsync(AnvelopDto setAnv, OperatiunePozitie operatiunePoz = OperatiunePozitie.Adaugare);
        Task<Result<AnvelopDto>> UpdateAnvelopaAsync(AnvelopDto setAnv);
        Task<Result<List<AnvelopDto>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Task<Result<AnvelopDto>> DeleteAnvelopaAsync(int id);
        Task<Result<List<CaMarca>>> GetMarci();
        //Task<Result<List<CaFlota>>> GetFlote();
        Task<DataTable> GenerateDataForExcel();

        Task<Result<SetAnvelopeDto>> GetSetAnvelopeByIdAsync(int id);
        Task<Result<SetAnvelopeDto>> GetSetAnvelopeByClientIdAsync(int clientId);
        Task<Result<SetAnvelopeDto>> GetSetAnvelopeByMasinaIdAsync(int masinaId);

        Task<Result<MasinaDto>> GetMasinaByIdAsync(int id);
        Task<Result<MasinaDto>> GetMasinaByNrAutoAsync(string nrAuto);
        Task<Result<MasinaDto>> GetMasinaBySerieSasiuAsync(string serieSasiu);
        Task<Result<MasinaDto>> GetMasinaForSetIdAsync(int setId);
        Task<Result<MasinaDto>> AddMasinaAsync(CaMasina masina);
        Task<Result<MasinaDto>> EditMasinaAsync(CaMasina masina);

    }
}
