using System.Collections.Generic;
using System.Threading.Tasks;
using DeltaQrCode.Models;

namespace DeltaQrCode.Services.Hotel
{
    using System.Data;
    using DeltaQrCode.ModelsDto;

    public interface IHotelService
    {
        //Task<Result<SetAnvelopeDto>> GetSetAnvelopeByIdAsync(int id);
        Result<SetAnvelopeDto> GetSetAnvelopeById(int id);

        //Task<Result<SetAnvelopeDto>> AddSetAnvelopeAsync(SetAnvelopeDto setAnv, OperatiunePozitie operatiunePoz = OperatiunePozitie.Adaugare);
        Result<SetAnvelopeDto> AddSetAnvelope(SetAnvelopeDto setAnv, OperatiunePozitie operatiunePoz = OperatiunePozitie.Adaugare);
        //Task<Result<SetAnvelopeDto>> UpdateSetAnvelopeAsync(SetAnvelopeDto setAnv);
        Result<SetAnvelopeDto> UpdateSetAnvelope(SetAnvelopeDto setAnv);
        //Task<Result<List<SetAnvelopeDto>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Result<List<SetAnvelopeDto>> SearchAnvelope(string searchString, int page, int itemsPerPage);
        //Task<Result<SetAnvelopeDto>> DeleteSetAnvelopeAsync(int id);
        Result<SetAnvelopeDto> DeleteSetAnvelope(int id);
        //Task<Result<List<CaMarca>>> GetMarci();
        Result<List<CaMarca>> GetMarci();
        //Task<Result<List<CaFlota>>> GetFlote();
        Result<List<CaFlota>> GetFlote();

        DataTable GenerateDataForExcel();

    }
}
