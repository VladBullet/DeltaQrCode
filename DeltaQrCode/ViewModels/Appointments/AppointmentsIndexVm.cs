using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ViewModels.Appointments
{
    using DeltaQrCode.ModelsDto;

    public class AppointmentsIndexVm
    {
        public AppointmentsIndexVm(int rampId, List<AppointmentVM> appointments) // TODO: change with apptVM
        {
            RampId = rampId;
            Appointments = appointments;
        }
        public int RampId { get; set; }
        public List<AppointmentVM> Appointments { get; set; } // TODO: change with apptVM
    }
}
