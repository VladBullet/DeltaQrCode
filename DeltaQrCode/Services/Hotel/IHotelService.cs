using System.Collections.Generic;
using System.Threading.Tasks;
using DeltaQrCode.Models;

namespace DeltaQrCode.Services.Hotel
{
    using System.Data;
    using DeltaQrCode.ModelsDto;

    public interface IHotelService
    {
        Task<Result<SetAnvelopeDto>> GetSetAnvelopeByIdAsync(int id);
        Task<Result<SetAnvelopeDto>> AddSetAnvelopeAsync(SetAnvelopeDto setAnv);
        Task<Result<SetAnvelopeDto>> UpdateSetAnvelopeAsync(SetAnvelopeDto setAnv);
        Task<Result<List<SetAnvelopeDto>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Task<Result<SetAnvelopeDto>> DeleteSetAnvelopeAsync(int id);
        Task<Result<List<SetAnvelopeDto>>> GetAllSetAnvelopeAsync();
        Task<Result<List<Position>>> GetAvailablePositionsAsync();

        Task<Result<List<CaMarca>>> GetMarci();
        Task<Result<List<CaFlota>>> GetFlote();

        Task<DataTable> GenerateDataForExcel();

    }
}
