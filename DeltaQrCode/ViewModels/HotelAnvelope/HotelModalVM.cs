using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.ModelsDto;

namespace DeltaQrCode.ViewModels
{
    using DeltaQrCode.Models;
    using DeltaQrCode.ViewModels.HotelAnvelope;

    public class HotelModalVM
    {
        public HotelModalVM()
        {

        }
        public HotelModalVM(AddEditSetAnvelopeVM set, ActionType actionType, List<Position> positions = null)
        {
            SetAnvelope = set;
            ActionType = actionType;
            AvailablePositions = positions == null ? new List<Position>() : positions;
        }


        public TireType Tip { get; set; }
        public AddEditSetAnvelopeVM SetAnvelope { get; set; }
        public List<Position> AvailablePositions { get; set; }
        public ActionType ActionType { get; set; }
    }

}
