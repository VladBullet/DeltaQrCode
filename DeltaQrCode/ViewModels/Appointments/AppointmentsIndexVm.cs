using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ViewModels.Appointments
{
    using DeltaQrCode.ModelsDto;

    public class AppointmentsIndexVm
    {
        public AppointmentsIndexVm(int rampId, List<AppointmentForProUiDto> appointments)
        {
            RampId = rampId;
            Appointments = appointments;
        }
        public int RampId { get; set; }
        public List<AppointmentForProUiDto> Appointments { get; set; }
    }
}
