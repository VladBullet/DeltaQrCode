using AutoMapper;
using DeltaQrCode.Services;
using DeltaQrCode.Services.SchimbAnvelope;
using DeltaQrCode.ViewModels.Appointments;
using DeltaQrCode.ViewModels.HotelAnvelope;
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
            //SetStep(step);
            return View();
        }
        public ActionResult StepOne(AppointmentVM appt)
        {

            return PartialView("_StepOne", appt);
        }
        public ActionResult StepTwo(AddEditSetAnvelopeVM set)
        {

            return PartialView("_StepTwo", set);
        }
        public ActionResult StepThree(AddEditSetAnvelopeVM set)
        {

            return PartialView("_StepThree", set);
        }
        public ActionResult StepFour(AddEditSetAnvelopeVM set)
        {

            return PartialView("_StepFour", set);
        }
        public ActionResult SetStep(int step)
        {
            switch (step)
            {
                case 1:
                    return RedirectToAction("StepOne");
                case 2:
                    return RedirectToAction("StepTwo");
                case 3:
                    return RedirectToAction("StepThree");
                case 4:
                    return RedirectToAction("StepFour");
                default :
                    return BadRequest("ceva nu a mers bine");
                    
            }
                
        }
    }
}
