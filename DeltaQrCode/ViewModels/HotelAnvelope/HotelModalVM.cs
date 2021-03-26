using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.ModelsDto;

namespace DeltaQrCode.ViewModels
{
    public class HotelModalVM
    {
        public HotelModalVM()
        {

        }
        public HotelModalVM(SetAnvelopeVM set, ActionType actionType)
        {
            SetAnvelope = set;
            ActionType = actionType;
        }


        public TyreType Tip { get; set; }
        public SetAnvelopeVM SetAnvelope { get; set; }
        public ActionType ActionType { get; set; }
    }

}
