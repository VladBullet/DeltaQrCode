﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DeltaQrCode.Controllers
{
    public class ErrorController : Controller
    {
        [HttpPost]
        public IActionResult ErrorModal([FromBody] string contact)
        {
            return PartialView("~/Views/Shared/_ErrorModal.cshtml", contact);
        }
    }
}