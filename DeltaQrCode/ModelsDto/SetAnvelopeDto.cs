using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.ModelsDto;

namespace DeltaQrCode.ModelsDto
{
    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.Models;

    public class SetAnvelopeDto
    {
        public uint Id { get; set; }
        public string NumarInmatriculare { get; set; }
        public string NumeClient { get; set; }
        public string NumarTelefon { get; set; }
        public uint? MarcaId { get; set; }
        public string Marca { get; set; }
        public string NumeSet { get; set; }
        public int NrBucati { get; set; }

        public Dimensiuni Dimensiuni { get; set; }
        public Uzura Uzura { get; set; }
        public Position Position { get; set; }

        public string DimensiuniString { get; set; }
        public string UzuraString { get; set; }
        public string PositionString { get; set; }

        public string TipSezon { get; set; }
        public string Evaluare { get; set; }
        public string StatusCurent { get; set; }
        public DateTime DataUltimaModificare { get; set; }
        public bool Deleted { get; set; }
    }
}
