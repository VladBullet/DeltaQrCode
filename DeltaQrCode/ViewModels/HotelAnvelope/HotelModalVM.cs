
namespace DeltaQrCode.ViewModels
{
    using DeltaQrCode.ViewModels.HotelAnvelope;

    public class HotelModalVM
    {
        public HotelModalVM()
        {

        }
        public HotelModalVM(AnvelopaVM set, ActionType actionType)
        {
            SetAnvelope = set;
            ActionType = actionType;
        }


        public TireType Tip { get; set; }
        public AnvelopaVM SetAnvelope { get; set; }
        public ActionType ActionType { get; set; }
    }

}
