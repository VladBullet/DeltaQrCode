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
        public Position Position { get; set; }
        public int? MarcaId { get; set; }
        public string Marca { get; set; }
        public string NumeSet { get; set; }
        public int NrBucati { get; set; }

        public Dimensiuni Dimensiuni { get; set; }
        public Uzura Uzura { get; set; }

        public string DimensiuniString { get; set; }
        public string UzuraString { get; set; }

        public string TipSezon { get; set; }
        public string Evaluare { get; set; }
        public string StatusCurent { get; set; }
        public DateTime DataUltimaModificare { get; set; }
        public bool Deleted { get; set; }


        public static List<SetAnvelopeDto> fakelist()
        {
            return new List<SetAnvelopeDto>() {
                new SetAnvelopeDto {

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
                Evaluare="Ok",
                //Rand=1,
                },
                new SetAnvelopeDto {

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
                Evaluare="OK",
                //Rand=1,

                },
                new SetAnvelopeDto {

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
                Evaluare="OK",
                //Rand=1,

                }
            };
        }

        public Guid SetId { get; set; }

        public TyreType TyreType { get; set; }
    }
}
