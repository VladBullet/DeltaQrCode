using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.Models;
using DeltaQrCode.ViewModels;

namespace DeltaQrCode.Services.Hotel
{
    public interface IHotelService
    {
        Task<Result<SetAnvelopeVM>> GetSetAnvelopeByIdAsync(int id);
        Task<Result<SetAnvelopeVM>> AddSetAnvelopeAsync(SetAnvelopeVM setAnv);
        Task<Result<SetAnvelopeVM>> UpdateSetAnvelopeAsync(SetAnvelopeVM setAnv);
        Task<Result<List<Position>>> GetAvailablePositionsAsync();
        Task<Result<List<SetAnvelopeVM>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage);
        Task<Result<SetAnvelopeVM>> DeleteSetAnvelopeAsync(int id);
    }
}
