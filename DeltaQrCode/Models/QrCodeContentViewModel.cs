using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Models
{
    public class QrCodeContentViewModel
    {
        public string QrCodeVal { get; set; }
        public int Selection { get; set; }
        public long DateTimeTicks { get; set; }
    }
}
