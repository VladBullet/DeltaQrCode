using System.Collections.Generic;

namespace DeltaQrCode.ViewModels.HotelAnvelope
{
    public class AddEditSetAnvelopeVM
    {

        public AddEditSetAnvelopeVM()
        {
            Anvelope = new List<AnvelopaVM>();
            Client = new ClientHotelVM();
            Masina = new MasinaVM();
            SetAnvelope = new SetAnvelopeVM();
        }
        public List<AnvelopaVM> Anvelope { get; set; }
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
