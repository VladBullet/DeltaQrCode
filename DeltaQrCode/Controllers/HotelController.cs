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
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class HotelController : Controller
    {
        private const int PageSize = 20;
        public IActionResult Index()
        {
            var list = SetAnvelopeVM.fakelist();
            int count = (int)Math.Ceiling((decimal)list.Count / (decimal)20);
            list = list.Skip(0).Take(PageSize).ToList();
            var model = new HotelListViewModel(list, count, 1);
            return View(model);
        }

        public IActionResult Search(string searchString = null, int? page = 1)
        {
            var list = SetAnvelopeVM.fakelist();
            int count = (int)Math.Ceiling((decimal)list.Count / (decimal)PageSize);
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.NumeClient.ToLower().Contains(searchString.ToLower()) || x.NumarInmatriculare.ToLower().Contains(searchString.ToLower())).ToList();
            }
            list = list.Skip((page.Value - 1) * PageSize).Take(PageSize).ToList();
            var model = new HotelListViewModel(list, count, page.Value);

            return PartialView("_HotelList", model);
        }

        public ActionResult EditModal(int id, string actionType)
        {
            ActionType actType = ActionType.Edit;
            if (actionType == "info")
            {
                actType = ActionType.Info;
            }
            var set = new SetAnvelopeVM();

            HotelModalVM setVm = new HotelModalVM(set, actType);

            return PartialView("_EditSetAnvPartial", setVm);
        }

        public ActionResult EditModalNew()
        {
            return PartialView("_EditSetAnvPartial", new HotelModalVM() { ActionType = ActionType.Add });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditModal([Bind(include: "")] SetAnvelopeVM setAnvelope)
        {
            if (ModelState.IsValid)
            {

                return Content("success");
            }
            else
            {
                // List the errors.
                StringBuilder sbError = new StringBuilder();
                foreach (var row in ModelState.Values)
                {
                    foreach (var err in row.Errors)
                    {
                        sbError.AppendLine(err.ErrorMessage);
                    }
                }
                return Content(sbError.ToString());
            }

        }
    }
}
