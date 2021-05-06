using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.ViewModels.Appointments;

namespace DeltaQrCode.ViewModels.SchimbAnvelope
{
    public class StepOneVM
    {
        public List<AppointmentVM> Appointments{ get; set; }
        public int RampId { get; set; }
    }
}
