using System;

namespace DeltaQrCode.ModelsDto
{
    using DeltaQrCode.Models;

    public class SetAnvelopeDto
    {
        public uint Id { get; set; }
        public string NumarInmatriculare { get; set; }
        public string NumeClient { get; set; }
        public string NumarTelefon { get; set; }
        public string SerieSasiu { get; set; }
        public uint? MarcaId { get; set; }
        public uint? FlotaId { get; set; }
        public string Flota { get; set; }
        public string Marca { get; set; }
        public int NrBucati { get; set; }
        public uint? OldPozitieId { get; set; }

        public Dimensiuni Dimensiuni { get; set; }
        public Uzura Uzura { get; set; }
        public HotelPositionsDto Pozitie { get; set; }

        public uint? PozitieId { get; set; }

        public string DimensiuniString { get; set; }
        public string UzuraString { get; set; }

        public string TipSezon { get; set; }
        public string Observatii { get; set; }
        public string StatusCurent { get; set; }
        public DateTime DataUltimaModificare { get; set; }
        public bool Deleted { get; set; }
    }

}
