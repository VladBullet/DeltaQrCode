﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ModelsDto
{
    public class SetAnvelopeDto
    {
        public uint Id { get; set; }
        public int MasinaId { get; set; }
        public int ClientId { get; set; }
        public string NumeSet { get; set; }
    }
}
