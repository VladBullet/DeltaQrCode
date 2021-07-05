using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaQrCode.Services.Hotel
{
    using System.Data;
    using System.Linq;
    using AutoMapper;
    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.Repositories;
    using DeltaQrCode.Repositories.Hotel_Positions;
    using DeltaQrCode.Services.Hotel_Positions;
    using DeltaQrCode.ViewModels.HotelAnvelope;
    using Serilog;

    public class HotelService : IHotelService
    {
        private readonly IHotelAnvelopeRepository _hotelRepository;
        private readonly IHotelPositionsRepository _hotelPositionsRepository;
        private readonly IHotelPositionsService _hotelPositionsService;
        private readonly IMapper _mapper;

        public HotelService(IHotelAnvelopeRepository hotelRepository, IHotelPositionsRepository hotelPositionsRepository, IHotelPositionsService hotelPositionsService, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _hotelPositionsRepository = hotelPositionsRepository;
            _hotelPositionsService = hotelPositionsService;
            _mapper = mapper;
        }


        public async Task<Result<AnvelopaDto>> GetAnvelopaByIdAsync(uint id)
        {
            try
            {
                var value = await _hotelRepository.GetAnvelopaByIdAsync(id);
                var model = _mapper.Map<AnvelopaDto>(value.Entity);
                if (model.PozitieId != null)
                {
                    var pozitie = await _hotelPositionsRepository.GetPositionByIdAsync(model.PozitieId.Value);
                    var pozitiedto = _mapper.Map<HotelPositionsDto>(pozitie.Entity);
                    model.Pozitie = pozitie.Successful ? pozitiedto : null;
                }
                if (value.Entity.MarcaId != null)
                {
                    var marca = await _hotelRepository.GetMarcaByIdAsync(value.Entity.MarcaId.Value);
                    model.Marca = marca.Successful ? marca.Entity.Label : string.Empty;
                }
                return Result<AnvelopaDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea anvelopei in functie de id in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea anvelopei in functie de id in servicii!", er);
            }
        }


        public async Task<Result<AnvelopaDto>> AddAnvelopaAsync(AnvelopaDto setAnv, OperatiunePozitie operatiunePoz = OperatiunePozitie.Adaugare)
        {
            try
            {
                if (setAnv.PozitieId != null && setAnv.StatusCurent == "InRaft")
                {
                    var position = await _hotelPositionsRepository.GetPositionByIdAsync(setAnv.PozitieId.Value);

                    if (!position.Successful)
                    {
                        Log.Error("Ceva nu a mers bine la gasirea pozitiei metoda de adaugare anvelope in servicii!");
                        throw new Exception("Ceva nu a mers bine la gasirea pozitiei in metoda de adaugare anvelope in servicii!");
                    }

                    await _hotelPositionsService.UpdatePositionAsync(setAnv.PozitieId.Value, 1, operatiunePoz);
                    position = await _hotelPositionsRepository.GetPositionByIdAsync(setAnv.PozitieId.Value);
                    setAnv.Pozitie = _mapper.Map<HotelPositionsDto>(position.Entity);
                }

                var marca = await _hotelRepository.GetMarcaByLableAsync(setAnv.Marca);
                if (!marca.Successful)
                {
                    Log.Error("Ceva nu a mers bine la gasirea marcii in functie de label in metoda de adaugare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la gasirea marcii in functie de label in metoda de adaugare anvelope in servicii!");
                }

                if (marca.Entity == null && !string.IsNullOrEmpty(setAnv.Marca))
                {
                    marca = await _hotelRepository.AddMarcaAsync(new CaMarca() { Label = setAnv.Marca.ToUpper() });
                }

                if (!marca.Successful)
                {
                    Log.Error("Ceva nu a mers bine la adaugarea marcii in metoda de adaugare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la adaugarea marcii in metoda de adaugare anvelope in servicii!");
                }
                setAnv.MarcaId = marca.Entity.Id;

                var modelForDatabase = _mapper.Map<CaAnvelopa>(setAnv);

                modelForDatabase.DataUltimaModificare = DateTime.Now;
                modelForDatabase.DataAdaugare = DateTime.Now;

                modelForDatabase.Deleted = false;

                var value = await _hotelRepository.AddAnvelopaAsync(modelForDatabase);
                var returnModel = _mapper.Map<AnvelopaDto>(value.Entity);
                return Result<AnvelopaDto>.ResultOk(returnModel);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea anvelopei in servicii!");
                throw new Exception("Ceva nu a mers bine la adaugarea anvelopei in servicii!", er);
            }
        }


        public async Task<Result<AnvelopaDto>> UpdateAnvelopaAsync(AnvelopaDto addSetAnv)
        {
            try
            {
                var editSetAnv = addSetAnv.Copy();

                if (editSetAnv.OldUzura == 0 && editSetAnv.Uzura != 0)
                {
                    var addAnvelopa = await AddAnvelopaAsync(editSetAnv);

                    var returnModel = _mapper.Map<AnvelopaDto>(addAnvelopa.Entity);

                    return Result<AnvelopaDto>.ResultOk(returnModel);
                }
                else
                {
                   
                    //case: was InRaft, will be different =>  Working!
                    if (editSetAnv.OldPozitieId != null && editSetAnv.StatusCurent != "InRaft" && editSetAnv.OldUzura != 0)
                    {
                        await _hotelPositionsService.UpdatePositionAsync(editSetAnv.OldPozitieId.Value, 1, OperatiunePozitie.Scoatere);
                        editSetAnv.PozitieId = null;
                    }


                    // case: was inRaft, will be in raft, position changed => Working!
                    if (editSetAnv.OldPozitieId != null && editSetAnv.StatusCurent == "InRaft" && editSetAnv.PozitieId != null && editSetAnv.OldPozitieId != editSetAnv.PozitieId && editSetAnv.OldUzura != 0)
                    {
                        await _hotelPositionsService.UpdatePositionAsync(editSetAnv.OldPozitieId.Value, 1, OperatiunePozitie.Scoatere);
                        await _hotelPositionsService.UpdatePositionAsync(editSetAnv.PozitieId.Value, 1, OperatiunePozitie.Adaugare);
                    }


                    // case: was NOT InRaft, will be InRaft => Working!
                    if (editSetAnv.PozitieId != null && editSetAnv.OldPozitieId == null && editSetAnv.OldUzura != 0)
                    {
                        await _hotelPositionsService.UpdatePositionAsync(editSetAnv.PozitieId.Value, 1, OperatiunePozitie.Adaugare);
                    }


                    // case: was InRaft, will be InRaft, No Changes
                    if (editSetAnv.PozitieId != null && editSetAnv.StatusCurent == "InRaft" && editSetAnv.OldPozitieId == editSetAnv.PozitieId && editSetAnv.OldUzura != 0)
                    {
                        editSetAnv.PozitieId = editSetAnv.OldPozitieId;
                    }

                    // case: was not InRaft, will not be InRaft
                    //if (editSetAnv.PozitieId == null && editSetAnv.StatusCurent != "InRaft" && editSetAnv.OldPozitieId == null)
                    //{
                    //    editSetAnv.PozitieId = editSetAnv.OldPozitieId;
                    //}


                    if (editSetAnv.PozitieId != null)
                    {
                        var position = await _hotelPositionsRepository.GetPositionByIdAsync(editSetAnv.PozitieId.Value);

                        if (!position.Successful)
                        {
                            Log.Error("Ceva nu a mers bine la gasirea pozitiei in metoda de editare anvelope in servicii!");
                            throw new Exception("Ceva nu a mers bine la gasirea pozitiei in metoda de editare anvelope in servicii!");
                        }

                        editSetAnv.Pozitie = _mapper.Map<HotelPositionsDto>(position.Entity);
                    }


                    // Marca
                    var marca = await _hotelRepository.GetMarcaByLableAsync(editSetAnv.Marca);
                    if (!marca.Successful)
                    {
                        Log.Error("Ceva nu a mers bine la gasirea marcii in functie de label in metoda de editare anvelope in servicii!");
                        throw new Exception("Ceva nu a mers bine la gasirea marcii in functie de label in metoda de editare anvelope in servicii!");
                    }

                    if (marca.Entity == null && !string.IsNullOrEmpty(editSetAnv.Marca))
                    {
                        marca = await _hotelRepository.AddMarcaAsync(new CaMarca() { Label = editSetAnv.Marca.ToUpper() });
                    }

                    if (!marca.Successful)
                    {
                        Log.Error("Ceva nu a mers bine la adaugarea marcii in metoda de editare anvelope in servicii!");
                        throw new Exception("Ceva nu a mers bine la adaugarea marcii in metoda de editare anvelope in servicii!");
                    }
                    editSetAnv.MarcaId = marca.Entity.Id;



                    var modelForDatabase = _mapper.Map<CaAnvelopa>(editSetAnv);

                    modelForDatabase.DataUltimaModificare = DateTime.Now;

                    var value = await _hotelRepository.UpdateAnvelopaAsync(modelForDatabase);
                    if (!value.Successful)
                    {
                        return Result<AnvelopaDto>.ResultError(value.Error);
                    }
                    var returnModel = _mapper.Map<AnvelopaDto>(value.Entity);


                    return Result<AnvelopaDto>.ResultOk(returnModel);
                }



            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la editarea anvelopei in servicii!");
                throw new Exception("Ceva nu a mers bine la editarea anvelopei in servicii!", er);
            }
        }


        public async Task<Result<List<AnvelopaDto>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage)
        {
            try
            {
                var result = await _hotelRepository.SearchAnvelopeAsync(searchString, page, itemsPerPage);
                var model = new List<AnvelopaDto>();
                if (result.Successful)
                {

                    model = _mapper.Map<List<AnvelopaDto>>(result.Entity);

                    foreach (var item in model)
                    {
                        if (item.PozitieId != null)
                        {
                            var pozitie = await _hotelPositionsRepository.GetPositionByIdAsync(item.PozitieId.Value);
                            var pozitiedto = _mapper.Map<HotelPositionsDto>(pozitie.Entity);
                            item.Pozitie = pozitie.Successful ? pozitiedto : null;
                        }

                        if (item.MarcaId != null)
                        {
                            var marca = await _hotelRepository.GetMarcaByIdAsync(item.MarcaId.Value);
                            item.Marca = marca.Successful ? marca.Entity.Label : string.Empty;
                        }

                    }
                    return Result<List<AnvelopaDto>>.ResultOk(model);
                }
                return Result<List<AnvelopaDto>>.ResultError(model, null, "Eroare la citirea din serviciu!");
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la cautarea anvelopei in servicii!");
                throw new Exception("Ceva nu a mers bine la cautarea anvelopei in servicii!", er);
            }
        }

        public async Task<Result<List<SetAnvelopeDto>>> SearchSetAnvelopeAsync(string searchString, int page, int itemsPerPage)
        {
            try
            {
                var resultClient = await _hotelRepository.SearchClientAsync(searchString);
                var resultMasina = await _hotelRepository.SearchMasinaAsync(searchString);

                var result = resultClient.Entity;
                result.AddRange(resultMasina.Entity);
                result = result.Distinct().ToList();

                var model = new List<SetAnvelopeDto>();


                if (resultClient.Successful && resultMasina.Successful)
                {

                    model = _mapper.Map<List<SetAnvelopeDto>>(result);

                    foreach (var item in model)
                    {
                        var anvelope = await _hotelRepository.GetAnvelopeBySetIdAsync(item.Id);
                        var anvelopeInRaft = anvelope.Entity.Where(x => x.PozitieId != null && !x.Deleted);
                        item.PozitieSet = string.Empty;
                        var stringPozList = new List<string>();
                        foreach (var anvelopa in anvelopeInRaft)
                        {
                            var pozitie = await _hotelPositionsRepository.GetPositionByIdAsync(anvelopa.PozitieId.Value);
                            var pozitiedto = _mapper.Map<HotelPositionsDto>(pozitie.Entity);
                            //item.PozitieSet = item.PozitieSet +  Environment.NewLine + pozitiedto.ToDisplayString() + ";";
                            stringPozList.Add(pozitiedto.ToDisplayString());
                        }

                        item.PozitieSet = string.Join("\n", stringPozList);
                    }
                    return Result<List<SetAnvelopeDto>>.ResultOk(model);
                }
                return Result<List<SetAnvelopeDto>>.ResultError(model, null, "Eroare la citirea din serviciu!");
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la cautarea setului in servicii!");
                throw new Exception("Ceva nu a mers bine la cautarea setului in servicii!", er);
            }
        }

        public async Task<Result<List<SetAnvelopeDto>>> SearchSetAnvelopeByStatusAsync(string searchString,string status, int page, int itemsPerPage)
        {
            try
            {
                var resultClient = await _hotelRepository.SearchClientAsync(searchString);
                var resultMasina = await _hotelRepository.SearchMasinaAsync(searchString);

                var result = resultClient.Entity;
                result.AddRange(resultMasina.Entity);
                result = result.Distinct().ToList();

                var deleted = new List<CaSetAnvelope>();


                foreach (var item in result)
                {
                    var anv = await _hotelRepository.GetAnvelopeBySetIdAndStatusAsync(item.Id, status);
                    if (anv.Entity.Count == 0) {
                        deleted.Add(item);
                    }
                }

                foreach (var item in deleted) {

                    result.Remove(item);
                }

                var model = new List<SetAnvelopeDto>();


                if (resultClient.Successful && resultMasina.Successful)
                {

                    model = _mapper.Map<List<SetAnvelopeDto>>(result);

                    foreach (var item in model)
                    {
                        var anvelope = await _hotelRepository.GetAnvelopeBySetIdAndStatusAsync(item.Id,status);
                        var anvelopeInRaft = anvelope.Entity.Where(x => x.PozitieId != null && !x.Deleted);
                        item.PozitieSet = string.Empty;
                        var stringPozList = new List<string>();
                        foreach (var anvelopa in anvelopeInRaft)
                        {
                            var pozitie = await _hotelPositionsRepository.GetPositionByIdAsync(anvelopa.PozitieId.Value);
                            var pozitiedto = _mapper.Map<HotelPositionsDto>(pozitie.Entity);
                            //item.PozitieSet = item.PozitieSet +  Environment.NewLine + pozitiedto.ToDisplayString() + ";";
                            stringPozList.Add(pozitiedto.ToDisplayString());
                        }

                        item.PozitieSet = string.Join("\n", stringPozList);
                    }
                    return Result<List<SetAnvelopeDto>>.ResultOk(model);
                }
                return Result<List<SetAnvelopeDto>>.ResultError(model, null, "Eroare la citirea din serviciu!");
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la cautarea setului in functie de status in servicii!");
                throw new Exception("Ceva nu a mers bine la cautarea setului in functie de status in servicii!", er);
            }
        }

        public async Task<Result<List<AnvelopaDto>>> SearchAnvelopeByStatusCurentAsync(string searchString, uint setId, int page, int itemsPerPage)
        {
            try
            {
                var result = await _hotelRepository.SearchAnvelopeByStatusCurentAsync(searchString, setId, page, itemsPerPage);
                var model = new List<AnvelopaDto>();
                if (result.Successful)
                {

                    model = _mapper.Map<List<AnvelopaDto>>(result.Entity);

                    foreach (var item in model)
                    {
                        if (item.PozitieId != null)
                        {
                            var pozitie = await _hotelPositionsRepository.GetPositionByIdAsync(item.PozitieId.Value);
                            var pozitiedto = _mapper.Map<HotelPositionsDto>(pozitie.Entity);
                            item.Pozitie = pozitie.Successful ? pozitiedto : null;
                        }

                        if (item.MarcaId != null)
                        {
                            var marca = await _hotelRepository.GetMarcaByIdAsync(item.MarcaId.Value);
                            item.Marca = marca.Successful ? marca.Entity.Label : string.Empty;
                        }

                    }
                    return Result<List<AnvelopaDto>>.ResultOk(model);
                }
                return Result<List<AnvelopaDto>>.ResultError(model, null, "Eroare la citirea din serviciu!");
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la cautarea anvelopei dupa status  in servicii!");
                throw new Exception("Ceva nu a mers bine la cautarea anvelopei dupa status in servicii!", er);
            }
        }


        public async Task<Result<AnvelopaDto>> DeleteAnvelopaAsync(uint id)
        {
            try
            {
                var set = await _hotelRepository.GetAnvelopaByIdAsync(id);
                var setAnv = _mapper.Map<AnvelopaDto>(set.Entity);


                if (setAnv.PozitieId != null)
                {
                    await _hotelPositionsService.UpdatePositionAsync(setAnv.PozitieId.Value, 1, OperatiunePozitie.Scoatere);
                    var position = await _hotelPositionsRepository.GetPositionByIdAsync(setAnv.PozitieId.Value);

                    if (!position.Successful)
                    {
                        Log.Error("Ceva nu a mers bine la stergerea pozitiei in metoda de stergere set anv in servicii!");
                        throw new Exception("Ceva nu a mers bine la stergerea pozitiei in metoda de stergere set anv in servicii!");
                    }
                }

                var value = await _hotelRepository.DeleteAnvelopaAsync(id);
                var model = _mapper.Map<AnvelopaDto>(value.Entity);

                return Result<AnvelopaDto>.ResultOk(model);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la stergerea anvelopei in servicii!");
                throw new Exception("Ceva nu a mers bine la stergerea anvelopei in servicii!", er);
            }
        }

        public async Task<Result<AnvelopaDto>> DeleteAnvelopaFromDataBaseAsync(uint id)
        {
            try
            {
                var value = await _hotelRepository.DeleteAnvelopaFromDataBaseAsync(id);
                var model = _mapper.Map<AnvelopaDto>(value.Entity);

                return Result<AnvelopaDto>.ResultOk(model);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la stergerea anvelopei in servicii!");
                throw new Exception("Ceva nu a mers bine la stergerea anvelopei in servicii!", er);
            }
        }


        public async Task<Result<SetAnvelopeDto>> DeleteSetAnvelopeAsync(uint id)
        {
            try
            {

                var value = await _hotelRepository.DeleteSetAnvelopeAsync(id);
                var model = _mapper.Map<SetAnvelopeDto>(value.Entity);

                return Result<SetAnvelopeDto>.ResultOk(model);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la stergerea setului in servicii!");
                throw new Exception("Ceva nu a mers bine la stergerea setului in servicii!", er);
            }
        }


        public async Task<Result<List<CaMarca>>> GetMarci()
        {
            try
            {
                var result = await _hotelRepository.GetMarciAsync();
                return Result<List<CaMarca>>.ResultOk(result.Entity);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea marcii in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea marcii in servicii!", er);
            }
        }

        public async Task<DataTable> GenerateDataForExcel()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[24] {
                                            new DataColumn("NumeClient"),
                                            new DataColumn("NumarTelefon"),
                                            new DataColumn("Sofer"),
                                            new DataColumn("NumarInmatriculare"),
                                            new DataColumn("SerieSasiu"),
                                            new DataColumn("TipVehicul"),
                                            new DataColumn("NumeSet"),
                                            new DataColumn("NrBucati"),
                                            new DataColumn("Uzura"),
                                            new DataColumn("Diametru"),
                                            new DataColumn("Latime"),
                                            new DataColumn("Inaltime"),
                                            new DataColumn("DOT"),
                                            new DataColumn("Marca"),
                                            new DataColumn("TipSezon"),
                                            new DataColumn("StatusCurent"),
                                            new DataColumn("Rand"),
                                            new DataColumn("Pozitie"),
                                            new DataColumn("Interval"),
                                            new DataColumn("LocuriOcupate"),
                                            new DataColumn("PozitiePeMasina"),
                                            new DataColumn("DataAdaugare"),
                                            new DataColumn("DataUltimaModificare"),
                                            new DataColumn("Observatii") });


            var allAnv = await SearchAnvelopeAsync(string.Empty, 1, int.MaxValue);
            var allAnvs = allAnv.Entity;
            var model = new List<DataForExcelVM>();
            foreach (var item in allAnvs)
            {
                var client = await GetClientForSetIdAsync(item.SetAnvelopeId);
                var clientMapp = _mapper.Map<ClientHotelVM>(client.Entity);

                var masina = await GetMasinaForSetIdAsync(item.SetAnvelopeId);
                var masinaMapp = _mapper.Map<MasinaVM>(masina.Entity);

                var set = await GetSetAnvelopeByIdAsync(item.SetAnvelopeId);
                var setMapp = _mapper.Map<SetAnvelopeVM>(set.Entity);

                var poz = new Result<HotelPositionsDto>();
                var pozMapp = new HotelPositionsVM();

                if (item.PozitieId != null)
                {
                    poz = await _hotelPositionsService.GetPositionByIdAsync(item.PozitieId.Value);
                    pozMapp = _mapper.Map<HotelPositionsVM>(poz.Entity);
                }
                

                var anvMapp = _mapper.Map<AnvelopaVM>(item);


                var anv = new DataForExcelVM();
                anv.Client = clientMapp;
                anv.Masina = masinaMapp;
                anv.SetAnvelope = setMapp;
                anv.Anvelopa = anvMapp;
                anv.Pozitie = pozMapp;

                model.Add(anv);

            }

            var setanvelope = from anvelope in model
                              select anvelope;

            foreach (var anvelope in setanvelope)
            {
                dt.Rows.Add(
                    anvelope.Client.NumeClient,
                    anvelope.Client.NumarTelefon,
                    anvelope.Client.Sofer,
                    anvelope.Masina.NumarInmatriculare,
                    anvelope.Masina.SerieSasiu,
                    anvelope.Masina.TipVehicul,
                    anvelope.SetAnvelope.NumeSet,
                    anvelope.SetAnvelope.NrBucati,
                    anvelope.Anvelopa.Uzura,
                    anvelope.Anvelopa.Diametru,
                    anvelope.Anvelopa.Latime,
                    anvelope.Anvelopa.Inaltime,
                    anvelope.Anvelopa.Dot,
                    anvelope.Anvelopa.Marca,
                    anvelope.Anvelopa.TipSezon,
                    anvelope.Anvelopa.StatusCurent,
                    anvelope.Pozitie.Rand,
                    anvelope.Pozitie.Pozitie,
                    anvelope.Pozitie.Interval,
                    anvelope.Pozitie.Locuriocupate,
                    anvelope.Anvelopa.PozitiePeMasina,
                    anvelope.Anvelopa.DataAdaugare,
                    anvelope.Anvelopa.DataUltimaModificare,
                    anvelope.Anvelopa.Observatii);
            }
            return dt;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public async Task<Result<SetAnvelopeDto>> GetSetAnvelopeByIdAsync(uint id)
        {
            try
            {
                var value = await _hotelRepository.GetSetAnvelopeByIdAsync(id);
                var model = _mapper.Map<SetAnvelopeDto>(value.Entity);
                return Result<SetAnvelopeDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea setului de anvelope in functie de id in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea setului de anvelope in functie de id in servicii!", er);
            }
        }

        public async Task<Result<SetAnvelopeDto>> GetSetAnvelopeByClientIdAsync(uint clientId)
        {
            try
            {
                var value = await _hotelRepository.GetSetAnvelopeByClientIdAsync(clientId);
                var model = _mapper.Map<SetAnvelopeDto>(value.Entity);
                return Result<SetAnvelopeDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea setului de anvelope in functie de clientId in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea setului de anvelope in functie de clientId in servicii!", er);
            }
        }

        public async Task<Result<SetAnvelopeDto>> GetSetAnvelopeByMasinaIdAsync(uint masinaId)
        {
            try
            {
                var value = await _hotelRepository.GetSetAnvelopeByMasinaIdAsync(masinaId);
                var model = _mapper.Map<SetAnvelopeDto>(value.Entity);
                return Result<SetAnvelopeDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea setului de anvelope in functie de masinaId in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea setului de anvelope in functie de masinaId in servicii!", er);
            }
        }

        public async Task<Result<SetAnvelopeDto>> AddSetAnvelopeAsync(SetAnvelopeDto setAnvelope)
        {
            try
            {
                var model = _mapper.Map<CaSetAnvelope>(setAnvelope);
                var value = await _hotelRepository.AddSetAnvelopeAsync(model);
                var returnModel = _mapper.Map<SetAnvelopeDto>(value.Entity);
                return Result<SetAnvelopeDto>.ResultOk(returnModel);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea setului de anvelope in servicii!");
                throw new Exception("Ceva nu a mers bine la adaugarea setului de anvelope in servicii!", er);
            }
        }

        public async Task<Result<SetAnvelopeDto>> EditSetAnvelopeAsync(SetAnvelopeDto setAnvelope)
        {
            try
            {
                var model = _mapper.Map<CaSetAnvelope>(setAnvelope);
                var value = await _hotelRepository.EditSetAnvelopeAsync(model);
                var returnModel = _mapper.Map<SetAnvelopeDto>(value.Entity);
                return Result<SetAnvelopeDto>.ResultOk(returnModel);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la editarea setului de anvelope in servicii!");
                throw new Exception("Ceva nu a mers bine la editarea setului de anvelope in servicii!", er);
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public async Task<Result<MasinaDto>> GetMasinaByIdAsync(uint id)
        {
            try
            {
                var value = await _hotelRepository.GetMasinaByIdAsync(id);
                var model = _mapper.Map<MasinaDto>(value.Entity);
                return Result<MasinaDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea masinii in functie de id in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea masinii in functie de id in servicii!", er);
            }
        }

        public async Task<Result<MasinaDto>> GetMasinaByNrAutoAsync(string nrAuto)
        {
            try
            {
                var value = await _hotelRepository.GetMasinaByNrAutoAsync(nrAuto);
                var model = _mapper.Map<MasinaDto>(value.Entity);
                return Result<MasinaDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea masinii in functie de nrAuto in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea masinii in functie de nrAuto in servicii!", er);
            }
        }

        public async Task<Result<MasinaDto>> GetMasinaBySerieSasiuAsync(string serieSasiu)
        {
            try
            {
                var value = await _hotelRepository.GetMasinaBySerieSasiuAsync(serieSasiu);
                var model = _mapper.Map<MasinaDto>(value.Entity);
                return Result<MasinaDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea masinii in functie de serieSasiu in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea masinii in functie de serieSasiu in servicii!", er);
            }
        }

        public async Task<Result<MasinaDto>> GetMasinaForSetIdAsync(uint setId)
        {
            try
            {
                var value = await _hotelRepository.GetMasinaForSetIdAsync(setId);
                var masina = _mapper.Map<MasinaDto>(value.Entity);
                return Result<MasinaDto>.ResultOk(masina);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea masinii in functie de setId in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea masinii in functie de setId in servicii!", er);
            }

        }

        public async Task<Result<MasinaDto>> AddMasinaAsync(MasinaDto masina)
        {
            try
            {
                var model = _mapper.Map<CaMasina>(masina);
                var value = await _hotelRepository.AddMasinaAsync(model);
                var returnModel = _mapper.Map<MasinaDto>(value.Entity);

                returnModel.NumarInmatriculare = returnModel.NumarInmatriculare.ToUpper();
                returnModel.SerieSasiu = returnModel.SerieSasiu.ToUpper();

                return Result<MasinaDto>.ResultOk(returnModel);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea masinii in servicii!");
                throw new Exception("Ceva nu a mers bine la adaugarea masinii in servicii!", er);
            }
        }

        public async Task<Result<MasinaDto>> EditMasinaAsync(MasinaDto masina)
        {
            try
            {
                var model = _mapper.Map<CaMasina>(masina);
                var value = await _hotelRepository.EditMasinaAsync(model);
                var returnModel = _mapper.Map<MasinaDto>(value.Entity);

                returnModel.NumarInmatriculare = returnModel.NumarInmatriculare.ToUpper();
                returnModel.SerieSasiu = returnModel.SerieSasiu.ToUpper();

                return Result<MasinaDto>.ResultOk(returnModel);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la editarea masinii in servicii!");
                throw new Exception("Ceva nu a mers bine la editarea masinii in servicii!", er);
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public async Task<Result<ClientHotelDto>> GetClientByIdAsync(uint id)
        {
            try
            {
                var value = await _hotelRepository.GetClientByIdAsync(id);
                var model = _mapper.Map<ClientHotelDto>(value.Entity);
                return Result<ClientHotelDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea clientului in functie de id in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea clientului in functie de id in servicii!", er);
            }
        }

        public async Task<Result<ClientHotelDto>> GetClientByNameAsync(string numeClient, string numarTelefon)
        {
            try
            {
                var value = await _hotelRepository.GetClientByNameAsync(numeClient, numarTelefon);
                var model = _mapper.Map<ClientHotelDto>(value.Entity);
                return Result<ClientHotelDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea clientului in functie de numeClient in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea clientului in functie de numeClient in servicii!", er);
            }
        }

        public async Task<Result<ClientHotelDto>> GetClientForSetIdAsync(uint setId)
        {
            try
            {
                var value = await _hotelRepository.GetClientForSetIdAsync(setId);
                var client = _mapper.Map<ClientHotelDto>(value.Entity);
                return Result<ClientHotelDto>.ResultOk(client);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea clientului in functie de setId in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea clientului in functie de setId in servicii!", er);
            }
        }

        public async Task<Result<ClientHotelDto>> AddClientAsync(ClientHotelDto client)
        {
            try
            {
                var model = _mapper.Map<CaClientHotel>(client);
                var value = await _hotelRepository.AddClientAsync(model);
                var returnModel = _mapper.Map<ClientHotelDto>(value.Entity);

                returnModel.NumeClient = returnModel.NumeClient.ToUpper();

                return Result<ClientHotelDto>.ResultOk(returnModel);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea clientului in servicii!");
                throw new Exception("Ceva nu a mers bine la adaugarea clientului in servicii!", er);
            }
        }

        public async Task<Result<ClientHotelDto>> EditClientAsync(ClientHotelDto client)
        {
            try
            {
                var model = _mapper.Map<CaClientHotel>(client);
                var value = await _hotelRepository.EditClientAsync(model);
                var returnModel = _mapper.Map<ClientHotelDto>(value.Entity);

                returnModel.NumeClient = returnModel.NumeClient.ToUpper();

                return Result<ClientHotelDto>.ResultOk(returnModel);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la editarea clientului in servicii!");
                throw new Exception("Ceva nu a mers bine la editarea clientului in servicii!", er);
            }
        }

        public async Task<Result<MasinaDto>> GetMasinaBySerieSasiuOrNrAutoAsync(string serieSasiu, string nrAuto)
        {
            try
            {
                var value = new Result<CaMasina>();
                if (!string.IsNullOrEmpty(serieSasiu) || !string.IsNullOrEmpty(nrAuto))
                {
                    if (!string.IsNullOrEmpty(serieSasiu))
                    {
                        value = await _hotelRepository.GetMasinaBySerieSasiuAsync(serieSasiu);
                    }
                    if (!string.IsNullOrEmpty(nrAuto))
                    {
                        value = await _hotelRepository.GetMasinaByNrAutoAsync(nrAuto);
                    }
                }

                var model = _mapper.Map<MasinaDto>(value.Entity);
                return Result<MasinaDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea masinii in functie de serieSasiu in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea masinii in functie de serieSasiu in servicii!", er);
            }
        }

        public async Task<Result<List<AnvelopaDto>>> GetAnvelopeBySetIdAsync(uint setId)
        {
            try
            {
                var list = await _hotelRepository.GetAnvelopeBySetIdAsync(setId);
                var result = _mapper.Map<List<AnvelopaDto>>(list.Entity);
                foreach (var item in result)
                {
                    if (item.PozitieId != null)
                    {
                        var position = await _hotelPositionsRepository.GetPositionByIdAsync(item.PozitieId.Value);

                        if (!position.Successful)
                        {
                            Log.Error("Ceva nu a mers bine la gasirea pozitiei in metoda de gasire anvelope dupa setId in servicii!");
                            throw new Exception("Ceva nu a mers bine la gasirea pozitiei in metoda de gasire anvelope dupa setId in servicii!");
                        }

                        item.Pozitie = _mapper.Map<HotelPositionsDto>(position.Entity);
                    }


                    // Marca
                    var marca = await _hotelRepository.GetMarcaByIdAsync(item.MarcaId.Value);
                    if (!marca.Successful)
                    {
                        Log.Error("Ceva nu a mers bine la gasirea marcii in functie de label in metoda de gasire anvelope dupa setId in servicii!");
                        throw new Exception("Ceva nu a mers bine la gasirea marcii in functie de label in metoda de gasire anvelope dupa setId in servicii!");
                    }
                    item.Marca = marca.Entity.Label;
                }


                return Result<List<AnvelopaDto>>.ResultOk(result);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea anvelopei in functie de setId in repository!");
                throw new Exception("Ceva nu a mers bine la gasirea anvelopei in functie de setId in repository!", er);
            }
        }

        public async Task<Result<List<SetAnvelopeDto>>> SearchAnvelopeSetAsync()
        {
            try
            {
                var result = await _hotelRepository.SearchAnvelopeSetAsync();
                var model = _mapper.Map<List<SetAnvelopeDto>>(result.Entity);


                return Result<List<SetAnvelopeDto>>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la cautarea setului in servicii!");
                throw new Exception("Ceva nu a mers bine la cautarea setului in servicii!", er);
            }
        }
    }
}
