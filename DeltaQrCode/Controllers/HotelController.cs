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

    using AutoMapper;

    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.Services.Hotel;
    using DeltaQrCode.ViewModels.HotelAnvelope;

    using Microsoft.AspNetCore.Authorization;

    using Newtonsoft.Json;
    using System.Diagnostics;


    [Authorize]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;
        private const int PageSize = 2;


        public HotelController(IHotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> Index()
        {
            //var list = SetAnvelopeVM.fakelist();
            var result = await _hotelService.GetAllSetAnvelopeAsync();
            var model = new HotelListViewModel(result.Entity,1, PageSize);
            return View(model);
        }

        public async Task<IActionResult> Search(string searchString,int pageNumber = 1)
        {


            var anvelopeResult = await _hotelService.SearchAnvelopeAsync(searchString,pageNumber, PageSize);
            var anvelope = anvelopeResult.Entity;


            var paginatedModel = PaginatedList<SetAnvelopeDto>.Create(anvelope, pageNumber, PageSize);
            var model = new HotelListViewModel(paginatedModel);
            return PartialView("_HotelList", model);
        }

        //public async Task<IActionResult> Search(string sortOrder,string currentFilter,string searchString,int? pageNumber)
        //{
        //    var result = await _hotelService.SearchAnvelopeAsync(searchString, page, 20);
        //    int count = 0;
        //    if (result.Successful)
        //    {
        //        count = (int)Math.Ceiling((decimal)result.Entity.Count / (decimal)20);
        //    }

        //    var model = new HotelListViewModel(result.Entity, count, page);

        //    return PartialView("_HotelList", model);
        //}

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

            if (actType == ActionType.Info)
            {
                return PartialView("_InfoSetAnvPartial", setVm.SetAnvelope);
            }
            return PartialView("_EditSetAnvPartial", setVm.SetAnvelope);
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
            //return BadRequest(JsonConvert.SerializeObject(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = "Eroare" }));
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
        [HttpGet]
        public async Task<IActionResult> AddModalNew()
        {
            return PartialView("_AddSetAnvPartial", new AddEditSetAnvelopeVM());
        }


        [HttpGet]
        public IActionResult DeleteModal(int id)
        {
            return PartialView("_DeleteSetAnvPartial", id);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmDelete(int id)
        {

            var result = await _hotelService.DeleteSetAnvelopeAsync(id);

            if (result.Successful)
            {
                return Ok(JsonConvert.SerializeObject("Setul de anvelope a fost sters!"));
            }

            return BadRequest(JsonConvert.SerializeObject(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = result.Error.Message }));
        }

        [Produces("application/json")]
        public async Task<IActionResult> GetMarci(string term)
        {
            var marci = await _hotelService.GetMarci();
            var list = marci.Entity.Select(x => x.Label).ToList();
            if (!string.IsNullOrEmpty(term))
            {
                list = list.Where(x => x.ToLower().Contains(term.ToLower())).ToList();
            }
            return new JsonResult(list);
        }
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAvailablePositions(string term)
        {
            var marci = await _hotelService.GetAvailablePositionsAsync();
            var list = marci.Entity.Select(x => x.PositionString).ToList();
            if (!string.IsNullOrEmpty(term))
            {
                list = marci.Entity.Where(x => (x.Rand + x.Poz + x.Interval).ToLower().Contains(term.ToLower())).Select(x => x.PositionString).ToList();
            }
            return new JsonResult(list);
        }
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetTireTypes()
        {
            var list = new List<string>();
            list.Add(TireType.Vara.ToDisplayString());
            list.Add(TireType.Iarna.ToDisplayString());
            list.Add(TireType.AllSeason.ToDisplayString());
            return new JsonResult(list);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetStatusAnvelope()
        {
            var list = new List<string>();
            list.Add(StatusAnvelope.Casate.ToDisplayString());
            list.Add(StatusAnvelope.InRaft.ToDisplayString());
            list.Add(StatusAnvelope.Montate.ToDisplayString());
            list.Add(StatusAnvelope.Predate.ToDisplayString());
            return new JsonResult(list);
        }
    }
}
