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

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
