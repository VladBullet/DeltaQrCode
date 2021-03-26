using System;
using System.Collections.Generic;

namespace DeltaQrCode.Models
{
    public partial class CaOperatiuneSchimbAnvelope
    {
        public int Id { get; set; }
        public string NumarInmatriculare { get; set; }
        public string PersoanaContact { get; set; }
        public int? SetIesireId { get; set; }
        public int SetIntrareId { get; set; }
        public string Observatii { get; set; }
        public DateTime DataSchimb { get; set; }
        public int? UserId { get; set; }
        public TimeSpan OraInceput { get; set; }
        public TimeSpan? OraSfarsit { get; set; }
        public bool OperatiuneFinalizata { get; set; }
        public int PasCurentOperatiune { get; set; }
    }
}
