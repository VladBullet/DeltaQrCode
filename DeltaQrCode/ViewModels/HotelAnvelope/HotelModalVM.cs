using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.ModelsDto;

namespace DeltaQrCode.ViewModels
{
    using DeltaQrCode.ViewModels.HotelAnvelope;

    public class HotelModalVM
    {
        public HotelModalVM()
        {

        }
        public HotelModalVM(AddEditSetAnvelopeVM set, ActionType actionType)
        {
            SetAnvelope = set;
            ActionType = actionType;
        }


        public TireType Tip { get; set; }
        public AddEditSetAnvelopeVM SetAnvelope { get; set; }
        public ActionType ActionType { get; set; }
    }

}
