using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeltaQrCode.Models;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeltaQrCode.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult OperationSelection([FromBody] QrCodeContentViewModel model)
        {
            var date = DateTimeOffset.FromUnixTimeMilliseconds(model.DateTimeTicks).DateTime;
            //QrCodeContentViewModel result = JsonConvert.DeserializeObject<QrCodeContentViewModel>(model);
            // process input and then return some feedback
            return Ok(JsonConvert.SerializeObject("Operatiune adaugata cu succes!"));
            //otherwise send badrequest

            //return BadRequest(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = "RELEASE OACHE!" }); ;
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
