﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ViewModels.HotelAnvelope
{
    public class HotelAnvelopaVm
    {
        public AnvelopaVM Anvelopa { get; set; }
        public ClientHotelVM Client { get; set; }
        public MasinaVM Masina { get; set; }
        public SetAnvelopeVM SetAnvelope { get; set; }
    }
}