using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeltaQrCode.Models;
using System.IO;

namespace DeltaQrCode.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CheckQrCode(QrCodeContentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var MembertoUpdate = new QrCodeContentViewModel
                {
                };

                using (var memomyStream = new MemoryStream())
                {
                    await model.UploadPicture.CopyToAsync(memomyStream);
                    MembertoUpdate.Image = memomyStream.ToArray();
                }
                return Ok("works");
            }
            return BadRequest("Picture not good! take another one!");
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
