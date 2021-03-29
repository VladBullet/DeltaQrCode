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
        public string NumeSet { get; set; }
        public int NrBucati { get; set; }
        public string Diametru { get; set; }
        public string Latime { get; set; }
        public string Inaltime { get; set; }
        public string StangaFata { get; set; }
        public string StangaSpate { get; set; }
        public string DreaptaFata { get; set; }
        public string DreaptaSpate { get; set; }
        public string TipSezon { get; set; }
        public int Evaluare { get; set; }
        public string StatusCurent { get; set; }
        public DateTime DataUltimaModificare { get; set; }
        public bool Deleted { get; set; }
    }
}
