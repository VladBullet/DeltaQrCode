using System;

namespace DeltaQrCode.ModelsDto
{
    public class GuestInfoDto
    {
        public GuestInfoDto()
        {

        }

        public GuestInfoDto(string guid, bool confirmed, DateTime dataAppt, TimeSpan oraAppt, int durata, bool appointmentExists = true)
        {
            Guid = guid;
            Confirmed = confirmed;
            AppointmentExists = appointmentExists;
            DataAppt = dataAppt;
            OraAppt = oraAppt;
            Durata = durata;
        }

        public string Guid { get; set; }
        public bool Confirmed { get; set; }
        public bool AppointmentExists { get; set; }
        public DateTime DataAppt { get; set; }
        public bool Expired => DataAppt.Add(OraAppt) > DateTime.Now;
        public TimeSpan OraAppt { get; set; }
        public int Durata { get; set; }

    }
}
