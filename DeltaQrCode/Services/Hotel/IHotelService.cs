using System.Collections.Generic;
using System.Threading.Tasks;
using DeltaQrCode.Models;

namespace DeltaQrCode.Services.Hotel
{
    using System.Data;
    using DeltaQrCode.ModelsDto;

    public interface IHotelService
    {
        Task<Result<AnvelopaDto>> GetAnvelopaByIdAsync(uint id);
        Task<Result<AnvelopaDto>> AddAnvelopaAsync(AnvelopaDto setAnv, OperatiunePozitie operatiunePoz = OperatiunePozitie.Adaugare);
        Task<Result<AnvelopaDto>> UpdateAnvelopaAsync(AnvelopaDto setAnv);
        Task<Result<AnvelopaDto>> DeleteAnvelopaAsync(uint id);
        Task<Result<AnvelopaDto>> DeleteAnvelopaFromDataBaseAsync(uint id);
        Task<Result<SetAnvelopeDto>> DeleteSetAnvelopeAsync(uint id);
        Task<Result<List<AnvelopaDto>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Task<Result<List<SetAnvelopeDto>>> SearchSetAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Task<Result<List<AnvelopaDto>>> SearchAnvelopeByStatusCurentAsync(string searchString, uint setId, int page, int itemsPerPage );
        Task<Result<List<SetAnvelopeDto>>> SearchAnvelopeSetAsync();
        Task<Result<List<AnvelopaDto>>> GetAnvelopeBySetIdAsync(uint setId);

        Task<Result<List<CaMarca>>> GetMarci();
        //Task<Result<List<CaFlota>>> GetFlote();
        Task<DataTable> GenerateDataForExcel();

        Task<Result<SetAnvelopeDto>> GetSetAnvelopeByIdAsync(uint id);
        Task<Result<SetAnvelopeDto>> GetSetAnvelopeByClientIdAsync(uint clientId);
        Task<Result<SetAnvelopeDto>> GetSetAnvelopeByMasinaIdAsync(uint masinaId);
        Task<Result<SetAnvelopeDto>> AddSetAnvelopeAsync(SetAnvelopeDto setAnvelope);
        Task<Result<SetAnvelopeDto>> EditSetAnvelopeAsync(SetAnvelopeDto setAnvelope);

        Task<Result<MasinaDto>> GetMasinaByIdAsync(uint id);
        Task<Result<MasinaDto>> GetMasinaByNrAutoAsync(string nrAuto);
        Task<Result<MasinaDto>> GetMasinaBySerieSasiuAsync(string serieSasiu);
        Task<Result<MasinaDto>> GetMasinaBySerieSasiuOrNrAutoAsync(string serieSasiu, string nrAuto);
        Task<Result<MasinaDto>> GetMasinaForSetIdAsync(uint setId);
        Task<Result<MasinaDto>> AddMasinaAsync(MasinaDto masina);
        Task<Result<MasinaDto>> EditMasinaAsync(MasinaDto masina);

        Task<Result<ClientHotelDto>> GetClientByIdAsync(uint id);
        Task<Result<ClientHotelDto>> GetClientByNameAsync(string numeClient, string numarTelefon);
        Task<Result<ClientHotelDto>> GetClientForSetIdAsync(uint setId);
        Task<Result<ClientHotelDto>> AddClientAsync(ClientHotelDto client);
        Task<Result<ClientHotelDto>> EditClientAsync(ClientHotelDto client);

    }
}
