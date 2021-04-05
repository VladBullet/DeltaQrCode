using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ViewModels.HotelAnvelope
{
    public class AddEditSetAnvelopeVM
    {
        public uint Id { get; set; }
        public string NumarInmatriculare { get; set; }
        public string NumeClient { get; set; }
        public string NumarTelefon { get; set; }
        public string PozitieInRaft { get; set; }
        public int? MarcaId { get; set; }
        public int? FlotaId { get; set; }
        public string Marca { get; set; }
        public string Flota { get; set; }
        public string NumeSet { get; set; }
        public int NrBucati { get; set; }
        public int Diametru { get; set; }
        public int Latime { get; set; }
        public int Inaltime { get; set; }
        public string DimensiuniString { get; set; }
        public int StangaFata { get; set; }
        public int StangaSpate { get; set; }
        public int DreaptaFata { get; set; }
        public int DreaptaSpate { get; set; }
        public string TipSezon { get; set; }
        public string Observatii { get; set; }
        public string StatusCurent { get; set; }
        public DateTime DataUltimaModificare { get; set; }
        public bool Deleted { get; set; }
    }
}
