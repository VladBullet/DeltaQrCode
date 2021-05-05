using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeltaQrCode.Controllers
{
    using System;

    using DeltaQrCode.Services.Guest;

    using Newtonsoft.Json;

    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(string guid)
        {
            if (!string.IsNullOrEmpty(guid))
            {
                var model = await _guestService.GetAppointmentInfoByGuid(guid);
                if (model.Successful && model.Entity.AppointmentExists)
                {
                    var result = model.Entity;
                    result.CallBackUrl = HttpContext.Request.Scheme +@"://"+ HttpContext.Request.Host + Url.Action("Details", "Guest", new { guid = guid });
                    return View(result);
                }
            }
            return RedirectToAction("WrongAppointment");
        }
        [Route("Guest/ConfirmAppointment")]
        [HttpPost]
        public async Task<IActionResult> ConfirmAppointment(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                return RedirectToAction("WrongAppointment");
            }

            var confirmed = await _guestService.ConfirmAppointmentAsync(guid);
            if (confirmed.Successful)
            {
                return Ok(JsonConvert.SerializeObject("Am modificat programarea dvs. si service-ul a fost anuntat!"));
            }
            return RedirectToAction("WrongAppointment");
        }

        [HttpGet]
        public IActionResult WrongAppointment()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult NotFound()
        //{
        //    return View();
        //}
    }
}
