using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    using Serilog;
    using ClosedXML.Excel;
    using System.IO;
    using DeltaQrCode.Services.Hotel_Positions;

    [Authorize]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IHotelPositionsService _hotelPositionsService;
        private readonly IMapper _mapper;
        private const int PageSize = 20;


        public HotelController(IHotelService hotelService, IHotelPositionsService hotelPositionsService, IMapper mapper)
        {
            _hotelService = hotelService;
            _hotelPositionsService = hotelPositionsService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string searchString, int pageNumber = 1)
        {
            var anvelopeResult = _hotelService.SearchAnvelope(searchString, pageNumber, PageSize);
            var anvelope = anvelopeResult.Entity;


            var paginatedModel = PaginatedList<SetAnvelopeDto>.Create(anvelope, pageNumber, PageSize);
            var model = new HotelListViewModel(paginatedModel);
            return PartialView("_HotelList", model);
        }

        //EDIT

        [HttpGet]
        public IActionResult EditModal(int id, string actionType)
        {
            ActionType actType = ActionType.Edit;
            if (actionType == "info")
            {
                actType = ActionType.Info;
            }
            var set = _hotelService.GetSetAnvelopeById(id);
            var model = _mapper.Map<AddEditSetAnvelopeVM>(set.Entity);

            model.OldPozitieId = model.PozitieId;
            model.OldNumarBucati = model.NrBucati;

            HotelModalVM setVm = new HotelModalVM(model, actType);

            if (actType == ActionType.Info)
            {
                return PartialView("_InfoSetAnvPartial", setVm.SetAnvelope);
            }
            return PartialView("_EditSetAnvPartial", setVm.SetAnvelope);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult EditModal(AddEditSetAnvelopeVM setAnvelope)
        {
            try
            {
                var dto = _mapper.Map<SetAnvelopeDto>(setAnvelope);
                var result = _hotelService.UpdateSetAnvelope(dto);
                if (result.Successful)
                {
                    return Ok(JsonConvert.SerializeObject("Set anvelope modificat cu success!"));
                }
                return BadRequest("Ceva nu a mers bine la modificarea setului de anvelope in controller!");

            }
            catch (Exception e)
            {
                Log.Error(e, "Ceva nu a mers bine la modificarea setului de anvelope in controller!");
                return BadRequest("Ceva nu a mers bine la modificarea setului de anvelope in controller!");
            }
        }

        //ADD

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddModal(AddEditSetAnvelopeVM setAnvelope)
        {
            try
            {
                var dto = _mapper.Map<SetAnvelopeDto>(setAnvelope);
                dto.Uzura = new Uzura(setAnvelope.StangaFata, setAnvelope.StangaSpate, setAnvelope.DreaptaFata, setAnvelope.DreaptaSpate);
                dto.UzuraString = dto.Uzura.ToCustomString();

                dto.Dimensiuni = new Dimensiuni(setAnvelope.Diametru, setAnvelope.Latime, setAnvelope.Inaltime, setAnvelope.Dot);
                dto.DimensiuniString = dto.Dimensiuni.ToCustomString();

                var result = _hotelService.AddSetAnvelope(dto);
                if (result.Successful)
                {
                    return Ok(JsonConvert.SerializeObject("Set anvelope adaugat cu success!"));
                }

                return BadRequest("Ceva nu a mers bine la adaugarea setului de anvelope in controller!" );
            }
            catch (Exception e)
            {
                Log.Error(e, "Ceva nu a mers bine la adaugarea setului de anvelope in controller!");
                return BadRequest("Ceva nu a mers bine la adaugarea setului de anvelope in controller!");
            }
        }
        [HttpGet]
        public IActionResult AddModalNew()
        {
            return PartialView("_AddSetAnvPartial", new AddEditSetAnvelopeVM());
        }

        //DELETE


        [HttpGet]
        public IActionResult DeleteModal(int id)
        {
            return PartialView("_DeleteSetAnvPartial", id);
        }

        [HttpPost]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                var result = _hotelService.DeleteSetAnvelope(id);

                if (result.Successful)
                {
                    return Ok(JsonConvert.SerializeObject("Setul de anvelope a fost sters!"));
                }

                return BadRequest("Ceva nu a mers bine la stergerea setului de anvelope in controller!");
            }
            catch (Exception e)
            {
                Log.Error(e, "Ceva nu a mers bine la stergerea setului de anvelope in controller!");
                return BadRequest("Ceva nu a mers bine la stergerea setului de anvelope in controller!");
            }
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAvailablePositions(string term, int nrbuc)
        {
            try
            {
                var positions = _hotelPositionsService.GetAvailablePositions(nrbuc);
                var availablepositions = _mapper.Map<List<HotelPositionsDto>>(positions.Entity);
                if (!string.IsNullOrEmpty(term))
                {
                    availablepositions = availablepositions.Where(x => (x.Rand + x.Pozitie + x.Interval).ToLower().Contains(term.ToLower())).ToList();
                }
                var model = new List<ItemVM>();
                model = availablepositions.Select(x => new ItemVM(x.Id, x.ToDisplayString())).ToList();

                return new JsonResult(model);
            }
            catch (Exception e)
            {
                Log.Error(e, "Ceva nu a mers bine la gasirea pozitiilor disponibile din hotel in controller!");
                return BadRequest("Ceva nu a mers bine la gasirea pozitiilor disponibile din hotel in controller!");
            }

        }

        [HttpGet]
        public IActionResult Download()
        {

            var data = _hotelService.GenerateDataForExcel();
            var filename = "RaportHotelAnvelope" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + DateTime.Now.Minute + ".xlsx";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(data);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);

                }
            }
        }

        [Produces("application/json")]
        public IActionResult GetMarci(string term)
        {
            var marci = _hotelService.GetMarci();
            var list = marci.Entity.Select(x => x.Label).ToList();
            if (!string.IsNullOrEmpty(term))
            {
                list = list.Where(x => x.ToLower().Contains(term.ToLower())).ToList();
            }
            return new JsonResult(list);
        }

        [Produces("application/json")]
        public IActionResult GetFlote(string term)
        {
            var flote = _hotelService.GetFlote();
            var list = flote.Entity.Select(x => x.Label).ToList();
            if (!string.IsNullOrEmpty(term))
            {
                list = list.Where(x => x.ToLower().Contains(term.ToLower())).ToList();
            }
            return new JsonResult(list);
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetTireTypes()
        {
            var list = new List<string>();
            list.Add(TireType.Vara.ToDisplayString());
            list.Add(TireType.Iarna.ToDisplayString());
            list.Add(TireType.AllSeason.ToDisplayString());
            return new JsonResult(list);
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetStatusAnvelope()
        {
            var list = new List<string>();
            list.Add(StatusAnvelope.Casate.ToDisplayString());
            list.Add(StatusAnvelope.InRaft.ToDisplayString());
            list.Add(StatusAnvelope.Montate.ToDisplayString());
            list.Add(StatusAnvelope.Predate.ToDisplayString());
            return new JsonResult(list);
        }

        public IActionResult GetDot(string term)
        {
            var list = ConstantsAndEnums.DOTlist();
            if (!string.IsNullOrEmpty(term))
            {
                list = list.Where(x => (x).ToLower().Contains(term.ToLower())).ToList();
            }
            return new JsonResult(list);
        }

        public IActionResult GetDiametru(string term)
        {
            var list = ConstantsAndEnums.DiametruDictionary;
            if (!string.IsNullOrEmpty(term))
            {
                list = list.Where(x => x.Value.ToLower().Contains(term.ToLower())).ToDictionary(x =>x.Key, x=>x.Value);   // Select(x => new KeyValuePair<int, string>(x.Key, x.Value)).ToList().ToDictionary<int, string>(y => y.Key, y => y.Value)
            }
            return new JsonResult(list);

        }

        public IActionResult GetLatime(string term)
        {
            var list = ConstantsAndEnums.Latime;
            List<string> result = list.ToList();
            if (!string.IsNullOrEmpty(term))
            {
                result = result.Where(x => (x).ToLower().Contains(term.ToLower())).ToList();
            }
            return new JsonResult(result);

        }

        public IActionResult GetInaltime(string term)
        {
            var list = ConstantsAndEnums.Inaltime;
            List<string> result = list.ToList();
            if (!string.IsNullOrEmpty(term))
            {
                result = result.Where(x => (x).ToLower().Contains(term.ToLower())).ToList();
            }
            return new JsonResult(result);

        }
    }
}
