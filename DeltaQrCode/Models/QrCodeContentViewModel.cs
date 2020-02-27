using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Models
{
    public class QrCodeContentViewModel
    {
        public int ID { get; set; }
        public string Company { get; set; }
        public string CarNumber { get; set; }
        public byte[] Image { get; set; }
        public IFormFile UploadPicture { get; set; }
    }
}
