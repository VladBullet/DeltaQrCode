using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeltaQrCode.Services;
using DeltaQrCode.ViewModels;

namespace DeltaQrCode.Controllers
{
    using DeltaQrCode.ModelsDto;
    using System.Text;
    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.Services.Hotel;
    using DeltaQrCode.ViewModels.HotelAnvelope;

    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private const int PageSize = 20;


        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        public async Task<IActionResult> Index()
        {
            //var list = SetAnvelopeVM.fakelist();
            var result = await _hotelService.SearchAnvelopeAsync(string.Empty, 1, 20);
            var list = new List<SetAnvelopeDto>();
            int count = 0;
            if (result.Successful)
            {
                count = (int)Math.Ceiling((decimal)result.Entity.Count / (decimal)20);
                list = result.Entity.Skip(0).Take(PageSize).ToList();
            }

            var model = new HotelListViewModel(list, count, 1);
            return View(model);
        }

        public async Task<IActionResult> Search(string searchString = null, int page = 1)
        {
            var result = await _hotelService.SearchAnvelopeAsync(searchString, page, 20);
            int count = 0;
            if (result.Successful)
            {
                count = (int)Math.Ceiling((decimal)result.Entity.Count / (decimal)20);
            }

            var model = new HotelListViewModel(result.Entity, count, page);

            return PartialView("_HotelList", model);
        }

        public IActionResult EditModal(int id, string actionType)
        {
            ActionType actType = ActionType.Edit;
            if (actionType == "info")
            {
                actType = ActionType.Info;
            }
            var set = new AddEditSetAnvelopeVM();

            HotelModalVM setVm = new HotelModalVM(set, actType);

            return PartialView("_EditSetAnvPartial", setVm);
        }

        public IActionResult EditModalNew()
        {
            return PartialView("_EditSetAnvPartial", new HotelModalVM() { ActionType = ActionType.Add });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddModal(SetAnvelopeDto setAnvelope)
        {
            var result = await _hotelService.AddSetAnvelopeAsync(setAnvelope);

            return View("Index");
        }
    }
}
