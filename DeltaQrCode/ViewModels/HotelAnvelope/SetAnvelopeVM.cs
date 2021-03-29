using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.ModelsDto;

namespace DeltaQrCode.ViewModels
{
    using DeltaQrCode.Models;

    public class SetAnvelopeVM
    {
        public uint Id { get; set; }
        public string NumarInmatriculare { get; set; }
        public string NumeClient { get; set; }
        public string NumarTelefon { get; set; }
        public Position Position { get; set; }
        public int? MarcaId { get; set; }
        public string NumeSet { get; set; }
        public int NrBucati { get; set; }

        public Dimensiuni Dimensiuni { get; set; }
        public Uzura Uzura { get; set; }

        public string DimensiuniString => string.Format("Diam:{0}, Lat:{1}, H:{2}", Dimensiuni.Diametru, Dimensiuni.Latime, Dimensiuni.Inaltime);

        public string UzuraString => string.Format("StF:{0}, DrF:{1}, StS:{2}, DrS:{3}", Uzura.StangaFata, Uzura.DreaptaFata, Uzura.StangaSpate, Uzura.DreaptaSpate);
        public string TipSezon { get; set; }
        public int Evaluare { get; set; }
        public string StatusCurent { get; set; }
        public DateTime DataUltimaModificare { get; set; }
        public bool Deleted { get; set; }


        public static List<SetAnvelopeVM> fakelist()
        {
            return new List<SetAnvelopeVM>() {
                new SetAnvelopeVM {

                NumarInmatriculare="bz02gdm",
                NumeClient="delta",
                NrBucati=4,
                // Diametru="15",
                //Latime="123",
                //Inaltime="15",
                //StangaFata="1",
                //StangaSpate="2",
                //DreaptaFata="1",
                //DreaptaSpate="2",
                TipSezon="nush",
                Evaluare=10,
                //Rand=1,
                },
                new SetAnvelopeVM {

                NumarInmatriculare="bz01gdk",
                NumeClient="delta1",
                NrBucati=3,
                //Diametru="15",
                //Latime="123",
                //Inaltime="15",
                //StangaFata="1",
                //StangaSpate="2",
                //DreaptaFata="1",
                //DreaptaSpate="2",
                TipSezon="nush",
                Evaluare=10,
                //Rand=1,

                },
                new SetAnvelopeVM {

                NumarInmatriculare="bz01gdk",
                NumeClient="delta1",
                NrBucati=3,
                //Diametru="15",
                //Latime="123",
                //Inaltime="15",
                //StangaFata="1",
                //StangaSpate="2",
                //DreaptaFata="1",
                //DreaptaSpate="2",
                TipSezon="nush",
                Evaluare=10,
                //Rand=1,

                }
            };
        }

        public Guid SetId { get; set; }

        public TyreType TyreType { get; set; }
    }
}
