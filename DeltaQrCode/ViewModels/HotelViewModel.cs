using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ViewModels
{
    public class HotelViewModel
    {
        public string NrAuto { get; set; }
        public string Client { get; set; }
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

        public static List<HotelViewModel> fakelist()
        {
            return new List<HotelViewModel>() {
                new HotelViewModel {

                NrAuto="bz02gdm",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdm",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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
                new HotelViewModel {

                NrAuto="bz02gdk",
                Client="delta",
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
                new HotelViewModel {

                NrAuto="bz01gdk",
                Client="delta1",
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

    }
}
