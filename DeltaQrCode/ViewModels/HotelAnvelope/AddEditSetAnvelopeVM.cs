using System;

namespace DeltaQrCode.ViewModels.HotelAnvelope
{
    public class AddEditSetAnvelopeVM
    {
        public uint Id { get; set; }
        public string NumarInmatriculare { get; set; }
        public string NumeClient { get; set; }
        public string NumarTelefon { get; set; }
        public string SerieSasiu { get; set; }
        public string PozitieInRaft { get; set; }
        public int? MarcaId { get; set; }
        public int? PozitieId { get; set; }
        public int? OldPozitieId { get; set; }
        public int? FlotaId { get; set; }
        public string Marca { get; set; }
        public string OldMarca { get; set; }
        public int OldNumarBucati { get; set; }
        public string Flota { get; set; }
        public string OldFlota { get; set; }
        public int NrBucati { get; set; }

        public int Diametru { get; set; }
        public int OldDiametru { get; set; }

        public int Latime { get; set; }
        public int OldLatime { get; set; }

        public int Inaltime { get; set; }
        public int OldInaltime { get; set; }

        public int Dot { get; set; }
        public int OldDot { get; set; }

        public string DimensiuniString { get; set; }

        public int StangaFata { get; set; }
        public int OldStangaFata { get; set; }

        public int? StangaSpate { get; set; }
        public int? OldStangaSpate { get; set; }

        public int? DreaptaFata { get; set; }
        public int? OldDreaptaFata { get; set; }

        public int? DreaptaSpate { get; set; }
        public int? OldDreaptaSpate { get; set; }

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
        public string OldTipSezon { get; set; }
        public string OldObservatii { get; set; }
    }
}
