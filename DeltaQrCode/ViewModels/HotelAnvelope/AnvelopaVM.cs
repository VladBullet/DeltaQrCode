using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ViewModels.HotelAnvelope
{
    public class AnvelopaVM
    {
        public AnvelopaVM()
        {

        }

        public AnvelopaVM(string pozitiePeMasina)
        {
            PozitiePeMasina = pozitiePeMasina;
        }

        public uint Id { get; set; }
        public string PozitieInRaft { get; set; }
        public int? MarcaId { get; set; }
        public int? PozitieId { get; set; }
        public int? OldPozitieId { get; set; }
        public string Marca { get; set; }
        public string OldMarca { get; set; }
        public int OldNumarBucati { get; set; }
        public int NrBucati { get; set; }
        public int Uzura { get; set; }
        public int OldUzura { get; set; }
        public string PozitiePeMasina { get; set; }



        public int Diametru { get; set; }
        public int OldDiametru { get; set; }

        public int Latime { get; set; }
        public int OldLatime { get; set; }

        public int Inaltime { get; set; }
        public int OldInaltime { get; set; }

        public int Dot { get; set; }
        public int OldDot { get; set; }

        public string DimensiuniString { get; set; }
        public string TipSezon { get; set; }
        public string Observatii { get; set; }
        public string StatusCurent { get; set; }
        public DateTime DataUltimaModificare { get; set; }
        public bool Deleted { get; set; }
        public uint? OldMarcaId { get; set; }
        public string OldTipSezon { get; set; }
        public string OldObservatii { get; set; }
        public uint SetAnvelopeId { get; set; }

    }
}
