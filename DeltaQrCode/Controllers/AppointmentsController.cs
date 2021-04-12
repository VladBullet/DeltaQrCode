﻿using System;
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
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
    using Serilog;

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
        public ActionResult Index(string startDateString, string activeDateString) // TODO: De vazut VLAD
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




        public async Task<JsonResult> GetAppointmentsForDate(string apptDate) // TODO: De vazut VLAD
        {
            DateTime dt;
            DateTime.TryParse(apptDate, out dt);
            if (dt == new DateTime())
                dt = DateTime.Now.Date;
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


        [HttpGet]
        public async Task<ActionResult> ModalEdit(int id)
        {

            var appt = await _appointmentService.GetAppointmentByIdAsync(id);
            var appointment = _mapper.Map<AppointmentVM>(appt.Entity);

            return PartialView("_EditAppointmentPartial", appointment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAppt(AppointmentVM appt)
        {
            try
            {
                appt.OraInceput = TimeSpan.FromMinutes(appt.StartTime_Hour * 60 + appt.StartTime_Minutes);
                appt.DataAppointment.AddHours(appt.StartTime_Hour).AddMinutes(appt.StartTime_Minutes);

                var dto = _mapper.Map<AppointmentDto>(appt);
                var result = await _appointmentService.UpdateAppointmentAsync(dto);

                if (result.Successful)
                {
                    return Ok(JsonConvert.SerializeObject("Programarea a fost editata cu succes!"));
                }

                return RedirectToAction("ErrorModal", "Error", "Ceva nu a mers bine la editarea programarii in controller!");

            }
            catch (Exception e)
            {
                Log.Error(e, "Ceva nu a mers bine la editarea programarii in controller!");
                return RedirectToAction("ErrorModal", "Error", "Ceva nu a mers bine la editarea programarii in controller!");
            }
        }

        [HttpGet]
        public ActionResult ModalAdd(string startDateStr, string startHour, string rampId)
        {
            DateTime startDate = DateTime.Parse(startDateStr);
            var s = startHour.Split('_');
            DateTime appointmentStart = startDate.AddHours(int.Parse(s[0])).AddMinutes(int.Parse(s[1]));

            var appointment = new AppointmentVM(appointmentStart);
            appointment.RampId = int.Parse(rampId);


            return PartialView("_AddAppointmentPartial", appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAppt(AppointmentVM appt) //ADDMODAL
        {
            try
            {
                appt.OraInceput = new TimeSpan(appt.StartTime_Hour, appt.StartTime_Minutes, 0);
                var model = _mapper.Map<AppointmentDto>(appt);
                var result = await _appointmentService.AddAppointmentAsync(model);

                if (result.Successful)
                {
                    return Ok(JsonConvert.SerializeObject("Programarea a fost adaugata!"));

                }

                return RedirectToAction("ErrorModal", "Error", "Ceva nu a mers bine la adaugarea programarii in controller!");
            }
            catch (Exception e)
            {
                Log.Error(e, "Ceva nu a mers bine la adaugarea programarii in controller!");
                return RedirectToAction("ErrorModal", "Error", "Ceva nu a mers bine la adaugarea programarii in controller!");
            }
        }

        // DELETE

        [HttpGet]
        public IActionResult DeleteModal(int id)
        {
            return PartialView("_DeleteAppointmentPartial", id);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            try
            {
                var result = await _appointmentService.DeleteAppointmentAsync(id);

                if (result.Successful)
                {
                    return Ok(JsonConvert.SerializeObject("Programarea a fost stearsa!"));
                }

                return RedirectToAction("ErrorModal", "Error", "Ceva nu a mers bine la stergerea programarii in controller!");
            }
            catch (Exception e)
            {
                Log.Error(e, "Ceva nu a mers bine la stergerea programarii in controller!");
                return RedirectToAction("ErrorModal", "Error", "Ceva nu a mers bine la stergerea programarii in controller!");
            }

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

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAvailableSpans(string startDateStr, string startHour, string rampId, string duration = "30")
        {
            try
            {
                DateTime selectedDate = DateTime.Parse(startDateStr);
                var str = startHour.Split('_');
                TimeSpan selectedOraInceput = new TimeSpan(int.Parse(str[0]), int.Parse(str[1]), 0);
                var result = await _appointmentService.DateAndHourIsAvailable(selectedDate, selectedOraInceput, int.Parse(duration), int.Parse(rampId));
                if (result.Successful)
                {
                    return new JsonResult(result.Entity);
                }

                return RedirectToAction("ErrorModal", "Error", "Ceva nu a mers bine la gasirea intervalului orar in controller!");
            }
            catch (Exception e)
            {
                Log.Error(e, "Ceva nu a mers bine la gasirea intervalului orar in controller!");
                return RedirectToAction("ErrorModal", "Error", "Ceva nu a mers bine la gasirea intervalului orar in controller!");
            }
        }
    }
}
