using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DeltaQrCode.Controllers
{
    using System.Diagnostics;
    using System.Net;
    using System.Text;
    using AutoMapper;
    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.Services;
    using DeltaQrCode.ViewModels;
    using DeltaQrCode.ViewModels.Appointments;

    using Microsoft.AspNetCore.Authorization;

    using Newtonsoft.Json;

    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;


        public AppointmentsController(IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        //GET: Appointments
        public ActionResult Index(string startDateString, string activeDateString, string professionalIdString)
        {


            // Get the appointments with a list of employees details this user can access
            CalendarVm calendarVm = new CalendarVm(User.Claims.FirstOrDefault(x => x.Type == "id")?.Value);
            var startDate = Helpers.GetStartDateFromStringParam(startDateString);
            var activeDate = Helpers.GetStartDateFromStringParam(activeDateString);

            if (startDate < DateTime.Now) startDate = DateTime.Now;
            if (activeDate < startDate) activeDate = startDate;

            if (activeDate.Date > startDate.Date.AddDays(3))
            {
                startDate = activeDate.AddDays(-2);
            }

            calendarVm.AvailableDates = Helpers.CreateAvailableDatesList(startDate);
            calendarVm.ActiveDate = activeDate;

            return View(calendarVm);
        }




        public async Task<JsonResult> GetAppointmentsForDate(string apptDate)
        {
            DateTime dt;
            DateTime.TryParse(apptDate, out dt);
            if (dt == new DateTime())
                dt = DateTime.Now.Date;

            //List<AppointmentForProUiDto> appointmentsList = _appointmentQueries.GetUiDto_AppointmentsForProfessionalOrEmployee(User.Identity.GetUserId(), professionalId, dt.Date, dt.Date.AddDays(1));

            var appointmentsList = await _appointmentService.GetAppointmentsAsync(dt);
            if (appointmentsList.Entity == null)
            {
                return new JsonResult(new List<AppointmentsIndexVm>());
            }
            var rampIds = appointmentsList.Entity.Select(x => x.RampId).Distinct();
            var result = new List<AppointmentsIndexVm>();
            foreach (var item in rampIds)
            {
                var list = appointmentsList.Entity.Where(x => x.RampId == item).ToList();
                var mappedList = _mapper.Map<List<AppointmentVM>>(list);
                var aux = new AppointmentsIndexVm(item, mappedList);
                result.Add(aux);
            }

            return new JsonResult(result);
        }


        public ActionResult EditModalNew(string startDateStr, string startHour, string rampNr)
        {
            DateTime startDate = DateTime.Parse(startDateStr);
            var s = startHour.Split('_');
            DateTime appointmentStart = startDate.AddHours(int.Parse(s[0])).AddMinutes(int.Parse(s[1]));

            var appointment = new AppointmentVM(appointmentStart);

            AppointmentModalVm appointmentVm = new AppointmentModalVm(User.Claims.FirstOrDefault(x => x.Type == "id")?.Value, appointment, ActionType.Add);

            return PartialView("_EditAppointmentPartial", appointmentVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditXModal(AppointmentVM appt)
        {
            var dto = _mapper.Map<AppointmentDto>(appt);
            var result = await _appointmentService.UpdateAppointmentAsync(dto);

            if (result.Successful)
            {
                return Ok(JsonConvert.SerializeObject("Set anvelope modificat cu success!"));
            }

            return BadRequest(JsonConvert.SerializeObject(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = result.Error.Message }));
        }

        [HttpGet]
        public async Task<ActionResult> EditModal(int id, string startDateStr)
        {

            DateTime startDate = DateTime.Parse(startDateStr);
            var appt = await _appointmentService.GetAppointmentByIdAsync(id);
            var apptModel = _mapper.Map<AppointmentVM>(appt.Entity);
            var appointment = new AppointmentModalVm
            {
                Appointment = apptModel, // AppointmentDto.FakeList(startDate).FirstOrDefault(x => x.AppointmentId == id),
                ActiveDate = startDate,
                CreateOrEdit = ActionType.Edit
            };
            return PartialView("_EditAppointmentPartial", appointment /*,appointmentVm*/);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditModal(AppointmentModalVm appt) //ADDMODAL
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<AppointmentDto>(appt);
                var result = await _appointmentService.AddAppointmentAsync(model);
                //_appointmentQueries.AddOrUpdateAppointmentFromDto(User.Identity.GetUserId(), appointment.ProfessionalId.ToString(), appointment);

                return Ok(JsonConvert.SerializeObject("Programarea a fost editata cu succes!"));
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

        [HttpGet]
        public IActionResult DeleteModal(int id)
        {
            return PartialView("_DeleteAppointmentPartial", id); // TODO: Create Delete Partial for Appointments
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmDelete(int id)
        {

            var result = await _appointmentService.DeleteAppointmentAsync(id);

            if (result.Successful)
            {
                return Ok(JsonConvert.SerializeObject("Programarea a fost stearsa!"));
            }

            return BadRequest(JsonConvert.SerializeObject(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = result.Error.Message }));
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetTipServiciu()
        {
            var list = new List<string>();
            list.Add(ServiceType.Mecanica.ToDisplayString());
            list.Add(ServiceType.Vulcanizare.ToDisplayString());
            return new JsonResult(list);
        }
        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetTimeDictionary()
        {
            return new JsonResult(ConstantsAndEnums.TimeDictionary);

        }

    }
}
