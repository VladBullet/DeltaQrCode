using System;

namespace DeltaQrCode.ViewModels.HotelAnvelope
{
    public class AddEditSetAnvelopeVM
    {
        public AnvelopaVM StangaFata { get; set; }
        public AnvelopaVM DreaptaFata { get; set; }
        public AnvelopaVM StangaSpate { get; set; }
        public AnvelopaVM DreaptaSpate { get; set; }
        public AnvelopaVM Optional1 { get; set; }
        public AnvelopaVM Optional2 { get; set; }

        public ClientHotelVM Client { get; set; }
        public MasinaVM Masina { get; set; }
        public SetAnvelopeVM SetAnvelope { get; set; }

        public AddEditSetAnvelopeVM()
        {
            StangaFata = new AnvelopaVM();
            DreaptaFata = new AnvelopaVM();
            StangaSpate = new AnvelopaVM();
            DreaptaSpate = new AnvelopaVM();
            Optional1 = new AnvelopaVM();
            Optional2 = new AnvelopaVM();
            Client = new ClientHotelVM();
            Masina = new MasinaVM();
            SetAnvelope = new SetAnvelopeVM();
        }

    }
}
