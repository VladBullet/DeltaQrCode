using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeltaQrCode.Models;
using System.IO;
using ZXing;
using System.Drawing;

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
                Bitmap imageOfQr;
                string result;
                using (var memomyStream = new MemoryStream())
                {
                    await model.UploadPicture.CopyToAsync(memomyStream);
                    MembertoUpdate.Image = memomyStream.ToArray();
                    result = ReadQrCodeFromImage(memomyStream);

                }
                if (!string.IsNullOrEmpty(result))
                    return Ok(result);
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

        private string ReadQrCodeFromImage(MemoryStream imgStream)
        {
            Result coreCompatResult;
            var coreCompatReader = new ZXing.CoreCompat.System.Drawing.BarcodeReader();
            using (var coreCompatImage = (Bitmap)Bitmap.FromStream(imgStream))
            {
                var result = coreCompatReader.Decode(coreCompatImage);
                coreCompatResult = result;
            }

            // BarcodeReaderGeneric<DrawingBitmap> barcodeReader = new BarcodeReaderGeneric<Bitmap>();
            //Bitmap img = new Bitmap(imgStream);
            //Result result2 = coreCompatReader.Decode(img);

            return coreCompatResult?.ToString();
        }
    }
}
