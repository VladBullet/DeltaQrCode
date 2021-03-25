using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeltaQrCode.Services;
using DeltaQrCode.ViewModels;

namespace DeltaQrCode.Controllers
{
    using DeltaQrCode.HelpersAndExtensions;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class HotelController : Controller
    {
        private const int PageSize = 20;
        public IActionResult Index()
        {
            var list = HotelViewModel.fakelist();
            int count = (int)Math.Ceiling((decimal)list.Count / (decimal)20);
            list = list.Skip(0).Take(PageSize).ToList();
            var model = new HotelListViewModel(list, count, 1);
            return View(model);
        }

        public IActionResult Search(string searchString = null, int? page = 1)
        {
            var list = HotelViewModel.fakelist();
            int count = (int)Math.Ceiling((decimal)list.Count / (decimal)PageSize);
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.Client.ToLower().Contains(searchString.ToLower()) || x.NrAuto.ToLower().Contains(searchString.ToLower())).ToList();
            }
            list = list.Skip((page.Value - 1) * PageSize).Take(PageSize).ToList();
            var model = new HotelListViewModel(list, count, page.Value);

            return PartialView("_HotelList", model);
        }
    }
}
