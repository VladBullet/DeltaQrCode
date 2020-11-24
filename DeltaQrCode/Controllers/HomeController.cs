using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeltaQrCode.ViewModels;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DeltaQrCode.Services;

namespace DeltaQrCode.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly IQrService qrService;
        public HomeController(IQrService qrS)
        {
            qrService = qrS;
        }
        [AllowAnonymous]
        public IActionResult Index()
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
