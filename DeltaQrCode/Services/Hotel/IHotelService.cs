using System.Collections.Generic;
using System.Threading.Tasks;
using DeltaQrCode.Models;

namespace DeltaQrCode.Services.Hotel
{
    using System.Data;
    using DeltaQrCode.ModelsDto;

    public interface IHotelService
    {
        Task<Result<AnvelopaDto>> GetAnvelopaByIdAsync(int id);
        Task<Result<AnvelopaDto>> AddAnvelopaAsync(AnvelopaDto setAnv, OperatiunePozitie operatiunePoz = OperatiunePozitie.Adaugare);
        Task<Result<AnvelopaDto>> UpdateAnvelopaAsync(AnvelopaDto setAnv);
        Task<Result<AnvelopaDto>> DeleteAnvelopaAsync(int id);
        Task<Result<List<AnvelopaDto>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);

        Task<Result<List<CaMarca>>> GetMarci();
        //Task<Result<List<CaFlota>>> GetFlote();
        Task<DataTable> GenerateDataForExcel();

        Task<Result<SetAnvelopeDto>> GetSetAnvelopeByIdAsync(int id);
        Task<Result<SetAnvelopeDto>> GetSetAnvelopeByClientIdAsync(int clientId);
        Task<Result<SetAnvelopeDto>> GetSetAnvelopeByMasinaIdAsync(int masinaId);
        Task<Result<SetAnvelopeDto>> AddSetAnvelopeAsync(SetAnvelopeDto setAnvelope);
        Task<Result<SetAnvelopeDto>> EditSetAnvelopeAsync(SetAnvelopeDto setAnvelope);

        Task<Result<MasinaDto>> GetMasinaByIdAsync(int id);
        Task<Result<MasinaDto>> GetMasinaByNrAutoAsync(string nrAuto);
        Task<Result<MasinaDto>> GetMasinaBySerieSasiuAsync(string serieSasiu);
        Task<Result<MasinaDto>> GetMasinaForSetIdAsync(int setId);
        Task<Result<MasinaDto>> AddMasinaAsync(MasinaDto masina);
        Task<Result<MasinaDto>> EditMasinaAsync(MasinaDto masina);

        Task<Result<ClientHotelDto>> GetClientByIdAsync(int id);
        Task<Result<ClientHotelDto>> GetClientByNameAsync(string numeClient);
        Task<Result<ClientHotelDto>> GetClientForSetIdAsync(int setId);
        Task<Result<ClientHotelDto>> AddClientAsync(ClientHotelDto client);
        Task<Result<ClientHotelDto>> EditClientAsync(ClientHotelDto client);

    }
}
