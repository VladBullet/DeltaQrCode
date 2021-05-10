using System;

namespace DeltaQrCode.ModelsDto
{
    public class AnvelopDto
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

        public string OldNumarInmatriculare { get; set; }
        public string OldNumeClient { get; set; }
        public string OldNumarTelefon { get; set; }
        public string OldSerieSasiu { get; set; }
        public uint? OldMarcaId { get; set; }
        public uint? OldFlotaId { get; set; }
        public Dimensiuni OldDimensiuni { get; set; }
        public Uzura OldUzura { get; set; }
        public string OldTipSezon { get; set; }
        public string OldObservatii { get; set; }

        public uint? OldPozitieId { get; set; }
        public int OldNumarBucati { get; set; }
        public string OldFlota { get; set; }
        public string OldMarca { get; set; }
    }

}
