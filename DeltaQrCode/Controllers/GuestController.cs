using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeltaQrCode.Controllers
{
    using DeltaQrCode.Services.Guest;

    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmAppointment(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                return RedirectToAction("WrongAppointment");
            }

            var confirmed = await _guestService.ConfirmAppointmentAsync(guid);
            return View();
        }

        [HttpGet]
        public IActionResult WrongAppointment()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
