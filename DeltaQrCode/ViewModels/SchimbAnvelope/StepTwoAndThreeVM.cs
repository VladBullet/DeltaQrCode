using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.ViewModels.HotelAnvelope;

namespace DeltaQrCode.ViewModels.SchimbAnvelope
{
    public class StepTwoAndThreeVM
    {
        public HotelListViewModel HotelList { get; set; }
        public  List<AddEditSetAnvelopeVM> SetAnvelope  { get; set; }
    }
}
