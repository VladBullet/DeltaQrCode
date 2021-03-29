using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.ModelsDto;

namespace DeltaQrCode.ViewModels
{
    public class SetAnvelopeVM
    {
        public string NumarInmatriculare { get; set; }
        public string NumeClient { get; set; }
        public string Marca { get; set; }
        public int NrBucati { get; set; }
        public string Diametru { get; set; }
        public string Latime { get; set; }
        public string Inaltime { get; set; }
        public string Dimensiuni => string.Format("Diam:{0}, Lat:{1}, H:{2}", Diametru, Latime, Inaltime);
        public string StangaFata { get; set; }
        public string StangaSpate { get; set; }
        public string DreaptaFata { get; set; }
        public string DreaptaSpate { get; set; }
        public string Uzura => string.Format("StF:{0}, DrF:{1}, StS:{2}, DrS:{3}", StangaFata, DreaptaFata, StangaSpate, DreaptaSpate);
        public string Tip { get; set; }
        public int Evaluare { get; set; }
        public int Rand { get; set; }
        public int Poz { get; set; }

        public static List<SetAnvelopeVM> fakelist()
        {
            return new List<SetAnvelopeVM>() {
                new SetAnvelopeVM {

                NumarInmatriculare="bz02gdm",
                NumeClient="delta",
                Marca="ford",
                NrBucati=4,
                Diametru="15",
                Latime="123",
                Inaltime="15",
                StangaFata="1",
                StangaSpate="2",
                DreaptaFata="1",
                DreaptaSpate="2",
                Tip="nush",
                Evaluare=10,
                Rand=1,
                Poz=1
                },
                new SetAnvelopeVM {

                NumarInmatriculare="bz01gdk",
                NumeClient="delta1",
                Marca="mercedes",
                NrBucati=3,
                Diametru="15",
                Latime="123",
                Inaltime="15",
                StangaFata="1",
                StangaSpate="2",
                DreaptaFata="1",
                DreaptaSpate="2",
                Tip="nush",
                Evaluare=10,
                Rand=1,
                Poz=1

                },
                new SetAnvelopeVM {

                NumarInmatriculare="bz01gdk",
                NumeClient="delta1",
                Marca="mercedes",
                NrBucati=3,
                Diametru="15",
                Latime="123",
                Inaltime="15",
                StangaFata="1",
                StangaSpate="2",
                DreaptaFata="1",
                DreaptaSpate="2",
                Tip="nush",
                Evaluare=10,
                Rand=1,
                Poz=1

                }
            };
        }

        public Guid SetId { get; set; }

        public TyreType TyreType { get; set; }
    }
}
