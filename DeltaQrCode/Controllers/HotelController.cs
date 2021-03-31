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

    using AutoMapper;

    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.Services.Hotel;
    using DeltaQrCode.ViewModels.HotelAnvelope;

    using Microsoft.AspNetCore.Authorization;

    using Newtonsoft.Json;
    using System.Diagnostics;

    using DeltaQrCode.Models;

    [Authorize]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;
        private const int PageSize = 20;


        public HotelController(IHotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
            _mapper = mapper;
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

        [HttpGet]
        public async Task<IActionResult> EditModal(int id, string actionType)
        {
            ActionType actType = ActionType.Edit;
            if (actionType == "info")
            {
                actType = ActionType.Info;
            }
            var set = await _hotelService.GetSetAnvelopeByIdAsync(id);

            var model = _mapper.Map<AddEditSetAnvelopeVM>(set.Entity);

            HotelModalVM setVm = new HotelModalVM(model, actType);

            if(actType == ActionType.Info)
            {
                return PartialView("_InfoSetAnvPartial", setVm);
            }
            return PartialView("_EditSetAnvPartial", setVm);
        }

        
        public IActionResult AddModalNew()
        {
            return PartialView("_AddSetAnvPartial", new HotelModalVM() { ActionType = ActionType.Add });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddModal(AddEditSetAnvelopeVM setAnvelope)
        {

            var dto = _mapper.Map<SetAnvelopeDto>(setAnvelope);
            dto.Uzura = new Uzura(setAnvelope.StangaFata, setAnvelope.StangaSpate, setAnvelope.DreaptaFata, setAnvelope.DreaptaSpate);
            dto.UzuraString = dto.Uzura.ToCustomString();

            dto.Dimensiuni = new Dimensiuni(setAnvelope.Diametru, setAnvelope.Latime, setAnvelope.Inaltime);
            dto.DimensiuniString = dto.Dimensiuni.ToCustomString();

            var result = await _hotelService.AddSetAnvelopeAsync(dto);
            if (result.Successful)
            {
                return Ok(JsonConvert.SerializeObject("Set anvelope adaugat cu success!"));
            }

            return BadRequest(JsonConvert.SerializeObject(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = result.Error.Message }));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditModal(AddEditSetAnvelopeVM setAnvelope)
        {
            var dto = _mapper.Map<SetAnvelopeDto>(setAnvelope);
            var result = await _hotelService.UpdateSetAnvelopeAsync(dto);
            if (result.Successful)
            {
                return Ok(JsonConvert.SerializeObject("Set anvelope modificat cu success!"));
            }

            return BadRequest(JsonConvert.SerializeObject(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = result.Error.Message }));
        }
    }
}
