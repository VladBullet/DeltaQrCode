using System.Collections.Generic;

namespace DeltaQrCode.ViewModels.Appointments
{
    public class AppointmentsIndexVm
    {
        public AppointmentsIndexVm(int rampId, List<AppointmentVM> appointments)
        {
            RampId = rampId;
            Appointments = appointments;
        }
        public int RampId { get; set; }
        public List<AppointmentVM> Appointments { get; set; }
    }
}
