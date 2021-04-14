using System.Diagnostics;
using DeltaQrCode.Services;
using DeltaQrCode.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeltaQrCode.Controllers
{
    [Authorize]
    public class QrCodeController : Controller
    {
        private readonly IQrService qrService;
        public QrCodeController(IQrService qrS)
        {
            qrService = qrS;
        }
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Generate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult OperationSelection([FromBody] QrCodeContentViewModel model)
        {
            // process input and then return some feedback
            var result = qrService.SaveOperation(model);
            if (result.Successful)
            {
                return Ok(JsonConvert.SerializeObject("Operatiune adaugata cu succes!"));
            }
            else
            {
                return BadRequest(JsonConvert.SerializeObject(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = result.Error.Message + " InnerEx: " + result.Error.InnerException.Message }));
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}