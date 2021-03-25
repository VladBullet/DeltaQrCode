using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaQrCode.ModelsDto
{
    using DeltaQrCode.Services;

    public class AppointmentForProUiDto
    {
        public static List<AppointmentForProUiDto> FakeList(DateTime dt)
        {
            return new List<AppointmentForProUiDto>()
                                                                { new AppointmentForProUiDto()
                                                                      {
                                                                          AppointmentId = Guid.Parse("d724f1c8-173f-43bb-b60d-bbd2ccce900c"),
                                                                          StartTime_Date = dt.AddMinutes(30).AddHours(12),
                                                                          DurationInMinutes = 30,
                                                                          StartTime_Hour = 13,
                                                                          StartTime_Minutes = 0,
                                                                          AppointmentType = AppointmentType.Vulcanizare,
                                                                          CreatedTime = dt,
                                                                          EmailAddress = "email@email.com",
                                                                          NrMasina = "B 55 NWN",
                                                                          NumeClient = "Vlad",
                                                                          PhoneNumber = "+40748885529",
                                                                          StartTime = dt.AddMinutes(30).AddHours(12),
                                                                          IsConfirmed = true,
                                                                          RampId = 1
                                                                      },
                                                                    new AppointmentForProUiDto()
                                                                        {
                                                                            AppointmentId = Guid.Parse("d724f1c8-173f-43bb-b60d-bbd2ccce900b"),
                                                                            StartTime_Date = dt.AddMinutes(30).AddHours(13),
                                                                            DurationInMinutes = 30,
                                                                            StartTime_Hour = 13,
                                                                            StartTime_Minutes = 30,
                                                                            AppointmentType = AppointmentType.Vulcanizare,
                                                                            CreatedTime = dt,
                                                                            EmailAddress = "email@email.com",
                                                                            NrMasina = "B 55 NWN",
                                                                            NumeClient = "Vlad",
                                                                            PhoneNumber = "+40748885529",
                                                                            StartTime = dt.AddMinutes(30).AddHours(13),
                                                                            IsConfirmed = true,
                                                                            RampId = 1
                                                                        },
                                                                    new AppointmentForProUiDto()
                                                                        {
                                                                            AppointmentId = Guid.Parse("d724f1c8-173f-43bb-b60d-bbd2ccce900a"),
                                                                            StartTime_Date = dt.AddMinutes(30).AddHours(14),
                                                                            DurationInMinutes = 60,
                                                                            StartTime_Hour = 13,
                                                                            StartTime_Minutes = 0,
                                                                            AppointmentType = AppointmentType.Vulcanizare,
                                                                            CreatedTime = dt,
                                                                            EmailAddress = "email@email.com",
                                                                            NrMasina = "B 55 NWN",
                                                                            NumeClient = "Vlad",
                                                                            PhoneNumber = "+40748885529",
                                                                            StartTime = dt.AddMinutes(30).AddHours(14),
                                                                            IsConfirmed = true,
                                                                            RampId = 2
                                                                        }
                                                                };
        }

        public AppointmentForProUiDto()
        {

        }

        public AppointmentForProUiDto(DateTime startTime)
        {
            this.StartTime = startTime;
            this.FillAdditionalUiProperties();
        }

        public Guid AppointmentId { get; set; }

        public int AppointmentIndex { get; set; }
        public int RampId { get; set; }


        [Required]
        [Display(Name = "Type")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select an appointment type")]
        public AppointmentType AppointmentType { get; set; }

        public string AppointmentTypeAsDisplay => this.AppointmentType.ToDisplayString();

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string NrMasina { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string NumeClient { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(200, MinimumLength = 2)]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\+[1-9]{1}[0-9]{3,14}$", ErrorMessage = "Please enter a valid phone number"), StringLength(12)]
        public string PhoneNumber { get; set; } = "";

        #region "Appointment Time"

        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public int DurationInMinutes { get; set; }

        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        #endregion


        #region "Creation - i.e. who made the appointment."

        [Required]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// If there are several steps to the process, 
        /// set this at the end so we know we're definitely done.
        /// </summary>
        [Required]
        public bool IsConfirmed { get; set; }

        #endregion


        #region "Cancellation"

        [Required]
        [Display(Name = "Cancelled")]
        public bool IsCancelled { get; set; }


        public DateTime CancelledTime { get; set; }

        #endregion

        /// <summary>
        /// NOTE: Never show this to a client. This is for the pro to make a note on the appt.
        /// </summary>
        [StringLength(2000)]
        public string ProfessionalNotes { get; set; } = "";



        #region "Additional UI Properties"

        public void FillAdditionalUiProperties()
        {
            this.StartTime_Date = StartTime.Date;
            this.StartTime_Hour = StartTime.Hour;
            this.StartTime_Minutes = StartTime.Minute;
        }

        public void UpdateTimesFromAdditionalUiProperties()
        {
            this.StartTime = this.StartTime_Date.Date.AddHours(this.StartTime_Hour).AddMinutes(this.StartTime_Minutes);
            this.EndTime = this.StartTime.AddMinutes(this.DurationInMinutes);
        }


        [Display(Name = "Start Date")]
        public DateTime StartTime_Date { get; set; }

        [Display(Name = "Start Hour")]
        [Range(0, 24, ErrorMessage = "Please pick a valid hour in the day.")]
        public int StartTime_Hour { get; set; }

        [Display(Name = "Start Minute")]
        [Range(0, 60, ErrorMessage = "Please pick a valid minute.")]
        public int StartTime_Minutes { get; set; }

        #endregion  


    }
}
