using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.ViewModels.Appointments;
using DeltaQrCode.ViewModels.HotelAnvelope;
using Microsoft.AspNetCore.Mvc;

namespace DeltaQrCode.Controllers
{
    public class SchimbAnvelopeController : Controller
    {
        public ActionResult Index(AppointmentVM appt)
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
    }
}
