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

    }
}
