using Microsoft.AspNetCore.Mvc;

namespace DeltaQrCode.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
