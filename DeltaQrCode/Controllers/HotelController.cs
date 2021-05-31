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

        [HttpGet]
        public async Task<IActionResult> EditSetAnv(uint id, string actionType)
        {

            try
            {
                var set = await _hotelService.GetSetAnvelopeByIdAsync(id);
                var setvm = _mapper.Map<SetAnvelopeVM>(set.Entity);

                var anvList = await _hotelService.SearchAnvelopeByStatusCurentAsync("InRaft", id, 1, PageSize);
                var anvListvm = _mapper.Map<List<AnvelopaVM>>(anvList.Entity);
                foreach (var item in anvListvm)
                {
                    item.OldUzura = item.Uzura;
                    item.OldPozitieId = item.PozitieId;
                }

                var masina = await _hotelService.GetMasinaForSetIdAsync(set.Entity.Id);
                var masinavm = _mapper.Map<MasinaVM>(masina.Entity);

                var client = await _hotelService.GetClientForSetIdAsync(set.Entity.Id);
                var clientvm = _mapper.Map<ClientHotelVM>(client.Entity);

                var setAnv = new AddEditSetAnvelopeVM();

                setAnv.Client = clientvm;
                setAnv.Masina = masinavm;
                setAnv.SetAnvelope = setvm;
                setAnv.Anvelope.Add(anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "StangaFata") != null ? anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "StangaFata") : new AnvelopaVM("StangaFata"));
                setAnv.Anvelope.Add(anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "DreaptaFata") != null ? anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "DreaptaFata") : new AnvelopaVM("DreaptaFata"));
                setAnv.Anvelope.Add(anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "StangaSpate") != null ? anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "StangaSpate") : new AnvelopaVM("StangaSpate"));
                setAnv.Anvelope.Add(anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "DreaptaSpate") != null ? anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "DreaptaSpate") : new AnvelopaVM("DreaptaSpate"));
                setAnv.Anvelope.Add(anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "Optional1") != null ? anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "Optional1") : new AnvelopaVM("Optional1"));
                setAnv.Anvelope.Add(anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "Optional2") != null ? anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "Optional2") : new AnvelopaVM("Optional2"));

                return View(setAnv);
            }
            catch (Exception e)
            {

                Log.Error(e, "Ceva nu a mers bine la modificarea setului de anvelope in controller!");
                return BadRequest("Ceva nu a mers bine la modificarea setului de anvelope in controller!");
            }



        }

        public async Task<IActionResult> SearchAnvelopeByStatusCurent(string searchString, uint setId, int pageNumber = 1)
        {
            var anvelopeResult = await _hotelService.SearchAnvelopeByStatusCurentAsync(searchString, setId, pageNumber, PageSize);
            var anvelope = anvelopeResult.Entity;

            var result = _mapper.Map<List<AnvelopaVM>>(anvelope);

            var paginatedModel = PaginatedList<AnvelopaVM>.Create(result, pageNumber, 3);
            var model = new HotelListViewModel(paginatedModel);
            return PartialView("_EditSetAnvelopeListPartial", model);
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Search(string searchString, int pageNumber = 1)
        {
            //var anvelopeResult = await _hotelService.SearchAnvelopeAsync(searchString, pageNumber, PageSize);
            //var anvelope = anvelopeResult.Entity;

            var sets = await _hotelService.SearchSetAnvelopeAsync(searchString, pageNumber, PageSize);
            var setsAnv = sets.Entity;

            //var mapper = _mapper.Map<List<HotelAnvelopaVm>>(anvelope);

            var mapper = new List<HotelAnvelopaVm>();
            //var listanvVm = _mapper.Map<List<SetAnvelopeVM>>(setsAnv);
            foreach (var item in setsAnv)
            {

                var client = await _hotelService.GetClientByIdAsync(item.ClientId);
                var masina = await _hotelService.GetMasinaByIdAsync(item.MasinaId);


                var anv = _mapper.Map<HotelAnvelopaVm>(item);
                anv.Client = _mapper.Map<ClientHotelVM>(client.Entity);
                anv.Masina = _mapper.Map<MasinaVM>(masina.Entity);

                mapper.Add(anv);
            }

            var paginatedModel = PaginatedList<HotelAnvelopaVm>.Create(mapper, pageNumber, PageSize);
            var model = new HotelListViewModel(paginatedModel);
            return PartialView("_HotelList", model);
        }

        public async Task<IActionResult> SearchAnvelopeForInfoSet(string searchString, uint setId, int pageNumber = 1)
        {
            var anvelopeResult = await _hotelService.SearchAnvelopeByStatusCurentAsync(searchString, setId, pageNumber, PageSize);
            var anvelope = anvelopeResult.Entity;

            var result = _mapper.Map<List<AnvelopaVM>>(anvelope);

            var paginatedModel = PaginatedList<AnvelopaVM>.Create(result, pageNumber, 3);
            var model = new HotelListViewModel(paginatedModel);
            return PartialView("_InfoAnvelopeList", model);
        }


        [HttpGet]
        public async Task<IActionResult> InfoModal(uint id, string actionType)
        {
            try
            {
                ActionType actType = ActionType.Info;

                var set = await _hotelService.GetSetAnvelopeByIdAsync(id);
                var setvm = _mapper.Map<SetAnvelopeVM>(set.Entity);

                var anvList = await _hotelService.GetAnvelopeBySetIdAsync(id);
                var anvListvm = _mapper.Map<List<AnvelopaVM>>(anvList.Entity);
                foreach (var item in anvListvm)
                {
                    item.OldPozitieId = item.PozitieId;
                }

                var masina = await _hotelService.GetMasinaForSetIdAsync(set.Entity.Id);
                var masinavm = _mapper.Map<MasinaVM>(masina.Entity);

                var client = await _hotelService.GetClientForSetIdAsync(set.Entity.Id);
                var clientvm = _mapper.Map<ClientHotelVM>(client.Entity);

                var setAnv = new AddEditSetAnvelopeVM();

                setAnv.Client = clientvm;
                setAnv.Masina = masinavm;
                setAnv.SetAnvelope = setvm;
                setAnv.Anvelope.Add(anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "StangaFata") != null ? anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "StangaFata") : new AnvelopaVM("StangaFata"));
                setAnv.Anvelope.Add(anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "DreaptaFata") != null ? anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "DreaptaFata") : new AnvelopaVM("DreaptaFata"));
                setAnv.Anvelope.Add(anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "StangaSpate") != null ? anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "StangaSpate") : new AnvelopaVM("StangaSpate"));
                setAnv.Anvelope.Add(anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "DreaptaSpate") != null ? anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "DreaptaSpate") : new AnvelopaVM("DreaptaSpate"));
                setAnv.Anvelope.Add(anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "Optional1") != null ? anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "Optional1") : new AnvelopaVM("Optional1"));
                setAnv.Anvelope.Add(anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "Optional2") != null ? anvListvm.FirstOrDefault(x => x.PozitiePeMasina == "Optional2") : new AnvelopaVM("Optional2"));

                //model.OldPozitieId = model.PozitieId;


                HotelModalVM setVm = new HotelModalVM(setAnv, actType);


                return PartialView("_InfoSetAnvPartial", setVm.SetAnvelope);
            }
            catch (Exception e)
            {

                Log.Error(e, "Ceva nu a mers bine la modificarea setului de anvelope in controller!");
                return BadRequest("Ceva nu a mers bine la modificarea setului de anvelope in controller!");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditModal(AddEditSetAnvelopeVM setAnv)
        {
            try
            {
                var client = setAnv.Client;
                var clientDto = _mapper.Map<ClientHotelDto>(client);

                var masina = setAnv.Masina;
                var masinaDto = _mapper.Map<MasinaDto>(masina);

                var setanvelope = setAnv.SetAnvelope;
                setanvelope.MasinaId = masina.Id;
                setanvelope.ClientId = client.Id;
                var setDto = _mapper.Map<SetAnvelopeDto>(setanvelope);

                var anvList = setAnv.Anvelope;
                var anvListDto = _mapper.Map<List<AnvelopaDto>>(anvList);

                var updateClient = await _hotelService.EditClientAsync(clientDto);
                var updateMasina = await _hotelService.EditMasinaAsync(masinaDto);
                var setAnvelopeUpdate = await _hotelService.EditSetAnvelopeAsync(setDto);

                bool updatedAnvSuccessful = true;

                foreach (var item in anvListDto)
                {
                    if (item.Uzura != 0)
                    {
                        var result = await _hotelService.UpdateAnvelopaAsync(item);

                        if (!result.Successful)
                        {
                            updatedAnvSuccessful = false;
                        }
                    }

                }


                if (updateClient.Successful && updateMasina.Successful && setAnvelopeUpdate.Successful && updatedAnvSuccessful)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddModal(AddEditSetAnvelopeVM setAnv)
        {
            try
            {
                var clientToMapp = setAnv.Client;
                var client = _mapper.Map<ClientHotelDto>(clientToMapp);

                var masinaToMapp = setAnv.Masina;
                var masina = _mapper.Map<MasinaDto>(masinaToMapp);


                var anvList = new List<AnvelopaVM>();

                anvList.AddRange(setAnv.Anvelope);

                anvList = anvList.Where(x => x.Uzura > 0).ToList();

                var dtoAnvList = _mapper.Map<List<AnvelopaDto>>(anvList);

                uint clientId = 0;
                uint masinaId = 0;

                var existingClient = await _hotelService.GetClientByNameAsync(setAnv.Client.NumeClient, setAnv.Client.NumarTelefon);

                if (existingClient.Successful && existingClient.Entity != null)
                {
                    clientId = existingClient.Entity.Id;
                }
                else
                {
                    var addClient = await _hotelService.AddClientAsync(client);
                    clientId = addClient.Entity.Id;

                }

                var existingMasina = await _hotelService.GetMasinaBySerieSasiuOrNrAutoAsync(setAnv.Masina.SerieSasiu, setAnv.Masina.NumarInmatriculare);

                if (existingMasina.Successful && existingMasina.Entity != null)
                {
                    masinaId = existingMasina.Entity.Id;
                }
                else
                {
                    var addMasina = await _hotelService.AddMasinaAsync(masina);
                    masinaId = addMasina.Entity.Id;
                }


                var setAnvelopeDto = new SetAnvelopeDto();

                setAnvelopeDto.MasinaId = masinaId;
                setAnvelopeDto.ClientId = clientId;
                setAnvelopeDto.NumeSet = setAnv.SetAnvelope.NumeSet;
                setAnvelopeDto.NrBucati = setAnv.SetAnvelope.NrBucati;

                var addSetAnvelope = await _hotelService.AddSetAnvelopeAsync(setAnvelopeDto);
                bool addedAnvSuccessful = true;

                foreach (var item in dtoAnvList)
                {

                    item.Dimensiuni = new Dimensiuni(item.Dimensiuni.Diam, item.Dimensiuni.Lat, item.Dimensiuni.H, item.Dimensiuni.Dot);
                    item.DimensiuniString = item.Dimensiuni.ToCustomString();
                    item.SetAnvelopeId = addSetAnvelope.Entity.Id;

                    var result = await _hotelService.AddAnvelopaAsync(item);

                    if (!result.Successful)
                    {
                        addedAnvSuccessful = false;
                    }
                }

                if (addedAnvSuccessful && clientId != 0 && masinaId != 0)
                {
                    return Ok(JsonConvert.SerializeObject("Set anvelope adaugat cu success!"));
                }

                return BadRequest("Ceva nu a mers bine la adaugarea setului de anvelope in controller!");
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
            return PartialView("_AddSetAnvPartial", new AddEditSetAnvelopeVM(true));
        }


        [HttpGet]
        public IActionResult DeleteModal(uint id)
        {
            return PartialView("_DeleteSetAnvPartial", id);
        }


        [HttpPost]
        public async Task<ActionResult> ConfirmDeleteAnvelopa(uint id)
        {
            try
            {
                var result = await _hotelService.DeleteAnvelopaAsync(id);

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

        [HttpPost]
        public async Task<ActionResult> ConfirmDeleteSet(uint id)
        {
            try
            {

                var anvelope = await _hotelService.GetAnvelopeBySetIdAsync(id);
                var anvelopeList = anvelope.Entity;
                bool successful = true;
                foreach (var item in anvelopeList)
                {
                    var result = await _hotelService.DeleteAnvelopaAsync(item.Id);
                    if (!result.Successful)
                    {
                        successful = false;
                    }
                }
                var deleteSet = await _hotelService.DeleteSetAnvelopeAsync(id);

                if (deleteSet.Successful && successful)
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
        public async Task<IActionResult> GetAvailablePositions(string term, uint nrbuc)
        {
            try
            {
                var positions = await _hotelPositionsService.GetAvailablePositionsAsync(nrbuc);
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
        public async Task<IActionResult> Download()
        {

            var data = await _hotelService.GenerateDataForExcel();
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


        //[Produces("application/json")]
        //public async Task<IActionResult> GetFlote(string term)
        //{
        //    //var flote = await _hotelService.GetFlote();
        //    var list = flote.Entity.Select(x => x.Label).ToList();
        //    if (!string.IsNullOrEmpty(term))
        //    {
        //        list = list.Where(x => x.ToLower().Contains(term.ToLower())).ToList();
        //    }
        //    return new JsonResult(list);
        //}


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
            var list = ConstantsAndEnums.DOTlist().OrderByDescending(x => x).ToList();
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
                list = list.Where(x => x.Value.ToLower().Contains(term.ToLower())).ToDictionary(x => x.Key, x => x.Value);
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

        [HttpPost]
        public async Task<IActionResult> SaveAnvelopaAuto(AnvelopaVM anvelopa)
        {
            try
            {
                var anv = await _hotelService.GetAnvelopaByIdAsync(anvelopa.Id);
                if (anvelopa.StatusCurent == "InRaft")
                {
                    var anvList = await _hotelService.SearchAnvelopeByStatusCurentAsync("InRaft", anv.Entity.SetAnvelopeId, 1, int.MaxValue);
                    var anvListEntity = anvList.Entity;
                    if (anvListEntity.Any(x => x.PozitiePeMasina == anvelopa.PozitiePeMasina))
                    {
                        return BadRequest("Nu s-a putut salva deoarece exista deja o anvelopa In Raft pe pozitia " + anvelopa.PozitiePeMasina + " in acest set");
                    }
                }

                var anvToUpdate = anv.Entity;
                anvToUpdate.StatusCurent = anvelopa.StatusCurent;
                anvToUpdate.OldUzura = anvToUpdate.Uzura;
                anvToUpdate.Uzura = anvelopa.Uzura;
                anvToUpdate.OldPozitieId = anvToUpdate.PozitieId;
                if (anvelopa.PozitieId != null)
                {
                    anvToUpdate.PozitieId = anvelopa.PozitieId;

                }

                var result = await _hotelService.UpdateAnvelopaAsync(anvToUpdate);

                if (result.Successful)
                {
                    return Ok(JsonConvert.SerializeObject("Anvelopa salvata cu success!"));
                }
                return BadRequest("Ceva nu a mers bine la salvarea anvelopei in controller!");
            }
            catch (Exception e)
            {

                Log.Error(e, "Ceva nu a mers bine la salvarea anvelopei in controller!");
                return BadRequest("Ceva nu a mers bine la salvarea anvelopei in controller!");
            }

        }
    }
}
