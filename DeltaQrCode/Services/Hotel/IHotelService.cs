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

        Task<Result<SetAnvelopeDto>> AddSetAnvelopeAsync(SetAnvelopeDto setAnv, OperatiunePozitie operatiunePoz = OperatiunePozitie.Adaugare);
        Result<SetAnvelopeDto> AddSetAnvelope(SetAnvelopeDto setAnv, OperatiunePozitie operatiunePoz = OperatiunePozitie.Adaugare);
        Task<Result<SetAnvelopeDto>> UpdateSetAnvelopeAsync(SetAnvelopeDto setAnv);
        Task<Result<List<SetAnvelopeDto>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Task<Result<SetAnvelopeDto>> DeleteSetAnvelopeAsync(int id);
        Task<Result<List<CaMarca>>> GetMarci();
        Task<Result<List<CaFlota>>> GetFlote();

        Task<DataTable> GenerateDataForExcel();

        Task<Result<List<HotelPositionsDto>>> GetAvailablePositionsAsync(int? nrbuc = null);
        Result<List<HotelPositionsDto>> GetAvailablePositions(int? nrbuc = null);
        Task<Result<HotelPositionsDto>> GetPositionByIdAsync(uint id);
        Result<HotelPositionsDto> GetPositionById(uint id);
        //Result<HotelPositionsDto> UpdatePosition(uint id, int nrbuc, OperatiunePozitie op);

    }
}
