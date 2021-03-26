using DeltaQrCode.Models;
using DeltaQrCode.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services
{
    public interface IQrService
    {
        bool SaveQrCode(Bitmap img);
       Result<QrCodeContentViewModel> SaveOperation(QrCodeContentViewModel model);
    }
}
