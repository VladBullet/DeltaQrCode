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
    public class HomeController : Controller
    {
        private IQrService qrService;
        public HomeController(IQrService qrS)
        {
            qrService = qrS;
        }
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
                return BadRequest(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = result.Error.Message }); ;
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
