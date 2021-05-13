using System;
using System.Collections.Generic;

namespace DeltaQrCode.ViewModels.HotelAnvelope
{
    public class AddEditSetAnvelopeVM
    {
        public List<AnvelopaVM> Anvelope { get; set; }
        //public AnvelopaVM StangaFata { get; set; }
        //public AnvelopaVM DreaptaFata { get; set; }
        //public AnvelopaVM StangaSpate { get; set; }
        //public AnvelopaVM DreaptaSpate { get; set; }
        //public AnvelopaVM Optional1 { get; set; }
        //public AnvelopaVM Optional2 { get; set; }

        public ClientHotelVM Client { get; set; }
        public MasinaVM Masina { get; set; }
        public SetAnvelopeVM SetAnvelope { get; set; }

        public AddEditSetAnvelopeVM(bool initListAnv = false)
        {
            Anvelope = new List<AnvelopaVM>();

            if (initListAnv)
            {
                foreach (var item in ConstantsAndEnums.PozitiiPeMasina)
                {
                    Anvelope.Add(new AnvelopaVM(item));
                }
            }
            
            Client = new ClientHotelVM();
            Masina = new MasinaVM();
            SetAnvelope = new SetAnvelopeVM();
        }

    }
}
