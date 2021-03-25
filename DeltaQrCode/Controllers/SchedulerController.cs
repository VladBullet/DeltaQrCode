using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DeltaQrCode.Controllers
{
    using System.Net;
    using System.Text;


    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.ViewModels.Scheduler;

    using Microsoft.AspNetCore.Authorization;

    using Newtonsoft.Json;

    [Authorize]
    public class SchedulerController : Controller
    {
        // GET: Appointments
        public ActionResult Index(string startDateString, string activeDateString, string professionalIdString)
        {


            // Get the appointments with a list of employees details this user can access
            CalendarVm calendarVm = new CalendarVm(User.Claims.FirstOrDefault(x => x.Type == "id")?.Value);
            var startDate = GetStartDateFromStringParam(startDateString);
            var activeDate = GetStartDateFromStringParam(activeDateString);

            if (startDate < DateTime.Now) startDate = DateTime.Now;
            if (activeDate < startDate) activeDate = startDate;

            if (activeDate.Date > startDate.Date.AddDays(3))
            {
                startDate = activeDate.AddDays(-2);
            }

            calendarVm.AvailableDates = CreateAvailableDatesList(startDate);
            calendarVm.ActiveDate = activeDate;

            return View(calendarVm);
        }



        public JsonResult GetAppointmentsForDate(string apptDate)
        {
            DateTime dt;
            DateTime.TryParse(apptDate, out dt);
            if (dt == new DateTime())
                dt = DateTime.Now.Date;

            //List<AppointmentForProUiDto> appointmentsList = _appointmentQueries.GetUiDto_AppointmentsForProfessionalOrEmployee(User.Identity.GetUserId(), professionalId, dt.Date, dt.Date.AddDays(1));
            List<AppointmentForProUiDto> appointmentsList = AppointmentForProUiDto.FakeList(dt);
            var rampIds = appointmentsList.Select(x => x.RampId).Distinct();
            var result = new List<AppointmentsIndexVm>();
            foreach (var item in rampIds)
            {
                var list = appointmentsList.Where(x => x.RampId == item).ToList();
                var aux = new AppointmentsIndexVm(item, list);
                result.Add(aux);
            }

            return new JsonResult(result);
            //return Json(/*appointmentsList*/"");
        }


        public ActionResult EditModalNew(string startDateStr, string startHour, string rampNr)
        {
            DateTime startDate = DateTime.Parse(startDateStr);
            var s = startHour.Split('_');
            DateTime appointmentStart = startDate.AddHours(int.Parse(s[0])).AddMinutes(int.Parse(s[1]));

            var appointment = new AppointmentForProUiDto(appointmentStart);
            appointment.DurationInMinutes = 60;
            appointment.NrMasina = "";
            appointment.NumeClient = "";
            appointment.PhoneNumber = "";

            AppointmentModalVm appointmentVm = new AppointmentModalVm(User.Claims.FirstOrDefault(x => x.Type == "id")?.Value, appointment);

            return PartialView("_editAppointmentPartial", appointmentVm);
        }


        public ActionResult EditModal(Guid? id, string professionalId)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            // Get the appointment for the professional.
            //AppointmentForProUiDto appointment = _appointmentQueries.GetUiDto_AppointmentByIdForProfessionalOrEmployee(User.Identity.GetUserId(), professionalId, (Guid)id);

            //if (appointment == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //AppointmentModalVm appointmentVm = new AppointmentModalVm(User.Claims.FirstOrDefault(x => x.Type == "id")?.Value, professionalId, appointment);

            return PartialView("_editAppointmentPartial", new AppointmentModalVm() /*,appointmentVm*/);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditModal([Bind(include: "ProfessionalId, AppointmentId,AppointmentIndex,AppointmentType,NrMasina,NumeClient,EmailAddress,PhoneNumber,TelephoneMobile,DurationInMinutes,IsCancelled,ProfessionalNotes,StartTime_Date,StartTime_Hour,StartTime_Minutes")] AppointmentForProUiDto appointment)
        {
            if (ModelState.IsValid)
            {
                //_appointmentQueries.AddOrUpdateAppointmentFromDto(User.Identity.GetUserId(), appointment.ProfessionalId.ToString(), appointment);

                return Content("success");
            }
            else
            {
                // List the errors.
                StringBuilder sbError = new StringBuilder();
                foreach (var row in ModelState.Values)
                {
                    foreach (var err in row.Errors)
                    {
                        sbError.AppendLine(err.ErrorMessage);
                    }
                }
                return Content(sbError.ToString());
            }

        }



        #region "Helpers"

        DateTime GetStartDateFromStringParam(string dateString)
        {
            if (String.IsNullOrWhiteSpace(dateString))
            {
                dateString = DateTime.Now.Date.ToString();
            }

            DateTime dt;
            if (DateTime.TryParse(dateString, out dt))
            {
                return dt;
            }

            return DateTime.Now;

        }

        /// <summary>
        /// Return a list of dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        List<DateTime> CreateAvailableDatesList(DateTime startDate)
        {
            var numberOfDaysToShow = 31;
            List<DateTime> availableDatesList = new List<DateTime>();
            for (int i = 0; i < numberOfDaysToShow; i++)
            {
                availableDatesList.Add(startDate.Date.AddDays(i));
            }

            return availableDatesList;
        }

        #endregion


    }
}
