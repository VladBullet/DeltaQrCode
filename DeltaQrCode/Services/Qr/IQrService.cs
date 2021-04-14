using DeltaQrCode.Models;
using DeltaQrCode.ViewModels;
using System.Drawing;

namespace DeltaQrCode.Services
{
    public interface IQrService
    {
        bool SaveQrCode(Bitmap img);
       Result<QrCodeContentViewModel> SaveOperation(QrCodeContentViewModel model);
    }
}
