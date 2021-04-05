using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.Models;
using DeltaQrCode.ViewModels;

namespace DeltaQrCode.Services.Hotel
{
    using DeltaQrCode.ModelsDto;

    public interface IHotelService
    {
        Task<Result<SetAnvelopeDto>> GetSetAnvelopeByIdAsync(int id);
        Task<Result<SetAnvelopeDto>> AddSetAnvelopeAsync(SetAnvelopeDto setAnv);
        Task<Result<SetAnvelopeDto>> UpdateSetAnvelopeAsync(SetAnvelopeDto setAnv);
        Task<Result<List<Position>>> GetAvailablePositionsAsync();
        Task<Result<List<SetAnvelopeDto>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Task<Result<SetAnvelopeDto>> DeleteSetAnvelopeAsync(int id);
        Task<Result<List<SetAnvelopeDto>>> GetAllSetAnvelopeAsync();

        Task<Result<List<CaMarca>>> GetMarci();

    }
}
