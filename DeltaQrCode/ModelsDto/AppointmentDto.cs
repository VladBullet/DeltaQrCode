using System;

namespace DeltaQrCode.ModelsDto
{

    public class AppointmentDto
    {
        public AppointmentDto()
        {

        }
        public AppointmentDto(DateTime startTime)
        {
            DataAppointment = startTime;
        }
        public int Id { get; set; }
        public int? ApptIndex { get; set; }
        public uint? ServiciuId { get; set; }
        public string Serviciu { get; set; }
        public int RampId { get; set; }
        public string NumarInmatriculare { get; set; }
        public string NumeClient { get; set; }
        public string EmailClient { get; set; }
        public string NumarTelefon { get; set; }
        public TimeSpan OraInceput { get; set; }
        public DateTime DataAppointment { get; set; }
        public int DurataInMinute { get; set; }
        public TimeSpan OraSfarsit => OraInceput.Add(new TimeSpan(0, 0, DurataInMinute, 0));
        public DateTime DataIntroducere { get; set; }
        public bool Confirmed { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public DateTime LastModified { get; set; }
        public string Observatii { get; set; }
        public string ConfirmedCode { get; set; }


    }
}
