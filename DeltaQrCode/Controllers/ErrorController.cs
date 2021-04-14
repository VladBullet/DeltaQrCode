using Microsoft.AspNetCore.Mvc;

namespace DeltaQrCode.Controllers
{
    public class ErrorController : Controller
    {
        [HttpPost]
        public IActionResult ErrorModal(string contact)
        {
            return PartialView("~/Views/Shared/_ErrorModal.cshtml", contact);
        }

        public IActionResult ErrorResult(string message)
        {
            return PartialView("~/Views/Shared/_ErrorResult.cshtml", message);
        }
    }
}