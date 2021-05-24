using System;

namespace DeltaQrCode.Models
{
    public partial class CaAnvelopa
    {
        public uint Id { get; set; }
        public uint? MarcaId { get; set; }
        public string Dimensiuni { get; set; }
        public int Uzura { get; set; }
        public string TipSezon { get; set; }
        public string Observatii { get; set; }
        public string StatusCurent { get; set; }
        public DateTime DataUltimaModificare { get; set; }
        public DateTime DataAdaugare { get; set; }
        public bool Deleted { get; set; }
        public uint? PozitieId { get; set; }
        public uint SetAnvelopeId { get; set; }
        public string PozitiePeMasina { get; set; }
    }
}
