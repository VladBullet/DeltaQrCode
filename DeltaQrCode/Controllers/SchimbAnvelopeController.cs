using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeltaQrCode.Services;
using DeltaQrCode.Services.SchimbAnvelope;
using DeltaQrCode.ViewModels.Appointments;
using DeltaQrCode.ViewModels.HotelAnvelope;
using DeltaQrCode.ViewModels.SchimbAnvelope;
using Microsoft.AspNetCore.Mvc;

namespace DeltaQrCode.Controllers
{
    public class SchimbAnvelopeController : Controller
    {
        private readonly ISchimbAnvelopeService _schimbAnvelopeService;
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public SchimbAnvelopeController(ISchimbAnvelopeService schimbAnvelopeService, IAppointmentService appointmentService, IMapper mapper)
        {
            _schimbAnvelopeService = schimbAnvelopeService;
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetStepOne(int rampId)
        {
            var appt = new StepOneVM();
            var appointmentsDto = await _appointmentService.GetAppointmentsByDateAndRampAsync(DateTime.Today,rampId);
            var appointments = _mapper.Map<List<AppointmentVM>>(appointmentsDto.Entity);
            appt.Appointments = appointments;
            return PartialView("_StepOne", appt);
        }

        //public async Task<IActionResult> SetStepOne()
        //{

        //}
        public IActionResult GetStepTwo(StepTwoAndThreeVM set)
        {

            return PartialView("_StepTwo", set);
        }
        public ActionResult GetStepThree(StepTwoAndThreeVM set)
        {

            return PartialView("_StepThree", set);
        }
        public ActionResult GetStepFour(StepFourVM set)
        {

            return PartialView("_StepFour", set);
        }
        public ActionResult GetStep(SchimbAnvelopeIndexVM model)
        {
            switch (model.StepNr)
            {
                case 1:
                    return RedirectToAction("GetStepOne", model.StepOne.RampId);
                case 2:
                    return RedirectToAction("GetStepTwo");
                case 3:
                    return RedirectToAction("GetStepThree");
                case 4:
                    return RedirectToAction("GetStepFour");
                default :
                    return BadRequest("ceva nu a mers bine");
                    
            }
                
        }

        public ActionResult SetStep(SchimbAnvelopeIndexVM model)
        {
            switch (model.StepNr)
            {
                case 1:
                    return RedirectToAction("SetStepOne");
                case 2:
                    return RedirectToAction("SetStepTwo");
                case 3:
                    return RedirectToAction("SetStepThree");
                case 4:
                    return RedirectToAction("SetStepFour");
                default:
                    return BadRequest("ceva nu a mers bine");

            }

        }
    }
}
