﻿using System;
using System.Collections.Generic;

namespace DeltaQrCode.Models
{
    public partial class CaAppointments
    {
        public uint Id { get; set; }
        public int? ApptIndex { get; set; }
        public int ServiciuId { get; set; }
        public string NumarInmatriculare { get; set; }
        public string NumeClient { get; set; }
        public string NumarTelefon { get; set; }
        public DateTime DataAppointment { get; set; }
        public string DataIntroducere { get; set; }
        public TimeSpan OraInceput { get; set; }
        public bool Deleted { get; set; }
        public string Observatii { get; set; }
        public bool Confirmed { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public string ConfirmedCode { get; set; }
    }
}
