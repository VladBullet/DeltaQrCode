using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaQrCode.Services.Hotel
{
    using System.Data;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using ClosedXML.Excel;
    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.Repositories;
    using DeltaQrCode.Repositories.Hotel_Positions;
    using DeltaQrCode.Services.Hotel_Positions;

    using DocumentFormat.OpenXml.Drawing.Wordprocessing;
    using DocumentFormat.OpenXml.Spreadsheet;

    using Microsoft.AspNetCore.Mvc;
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

        public async Task<Result<SetAnvelopeDto>> GetSetAnvelopeByIdAsync(int id)
        {
            try
            {
                var value = await _hotelRepository.GetSetAnvelopeByIdAsync(id);
                var model = _mapper.Map<SetAnvelopeDto>(value.Entity);
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
                if (value.Entity.FlotaId != null)
                {
                    var flota = await _hotelRepository.GetFlotaByIdAsync(value.Entity.FlotaId.Value);
                    model.Flota = flota.Successful ? flota.Entity.Label : string.Empty;
                }
                return Result<SetAnvelopeDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea setului de anvelope in functie de id in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea setului de anvelope in functie de id in servicii!", er);
            }
        }
        public async Task<Result<SetAnvelopeDto>> AddSetAnvelopeAsync(SetAnvelopeDto setAnv, OperatiunePozitie operatiunePoz = OperatiunePozitie.Adaugare)
        {
            try
            {
                if (setAnv.PozitieId != null && setAnv.StatusCurent == "InRaft")
                {
                    var position = await _hotelPositionsService.GetPositionByIdAsync(setAnv.PozitieId.Value);
                    if (!position.Successful)
                    {
                        Log.Error("Ceva nu a mers bine la gasirea pozitiei metoda de adaugare anvelope in servicii!");
                        throw new Exception("Ceva nu a mers bine la gasirea pozitiei in metoda de adaugare anvelope in servicii!");
                    }

                    _hotelPositionsService.UpdatePosition(setAnv.PozitieId.Value, setAnv.NrBucati, operatiunePoz);
                    //if (!position.Entity.Ocupat && (ConstantsAndEnums.MaxLocuriPoz - position.Entity.Locuriocupate) >= setAnv.NrBucati)
                    //{
                    //}

                    position = await _hotelPositionsService.GetPositionByIdAsync(setAnv.PozitieId.Value);
                    setAnv.Pozitie = position.Entity;
                }

                var marca = await _hotelRepository.GetMarcaByLableAsync(setAnv.Marca);
                if (!marca.Successful)
                {
                    Log.Error("Ceva nu a mers bine la gasirea marcii in functie de label in metoda de adaugare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la gasirea marcii in functie de label in metoda de adaugare anvelope in servicii!");
                }

                if (marca.Entity == null && !string.IsNullOrEmpty(setAnv.Marca))
                {
                    marca = _hotelRepository.AddMarca(new CaMarca() { Label = setAnv.Marca.ToUpper() });
                }

                if (!marca.Successful)
                {
                    Log.Error("Ceva nu a mers bine la adaugarea marcii in metoda de adaugare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la adaugarea marcii in metoda de adaugare anvelope in servicii!");
                }
                setAnv.MarcaId = marca.Entity.Id;

                var flota = Result<CaFlota>.ResultOk(null);

                if (!string.IsNullOrEmpty(setAnv.Flota))
                {
                    flota = await _hotelRepository.GetFlotaByLableAsync(setAnv.Flota);
                }

                if (!flota.Successful)
                {
                    Log.Error("Ceva nu a mers bine la gasirea floyei in functie de label in metoda de adaugare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la gasirea flotei in functie de label in metoda de adaugare anvelope in servicii!");
                }

                if (flota.Entity == null && !string.IsNullOrEmpty(setAnv.Flota))
                {
                    flota = _hotelRepository.AddFlota(new CaFlota() { Label = setAnv.Flota.ToUpper() });
                }

                if (!flota.Successful)
                {
                    Log.Error("Ceva nu a mers bine la adaugarea flotei in metoda de adaugare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la adaugarea flotei in metoda de adaugare anvelope in servicii!");
                }
                if (flota.Entity != null)
                {
                    setAnv.FlotaId = flota.Entity.Id;
                }
                else
                {
                    setAnv.FlotaId = null;
                }

                var modelForDatabase = _mapper.Map<CaSetAnvelope>(setAnv);

                modelForDatabase.DataUltimaModificare = DateTime.Now;

                modelForDatabase.Deleted = false;

                modelForDatabase.NumarInmatriculare = modelForDatabase.NumarInmatriculare.ToUpper();
                modelForDatabase.NumeClient = modelForDatabase.NumeClient.ToUpper();
                modelForDatabase.SerieSasiu = modelForDatabase.SerieSasiu.ToUpper();
                // send model to database
                var value = _hotelRepository.AddSetAnvelope(modelForDatabase);
                var returnModel = _mapper.Map<SetAnvelopeDto>(value.Entity);
                return Result<SetAnvelopeDto>.ResultOk(returnModel);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea setului de anvelope in servicii!");
                throw new Exception("Ceva nu a mers bine la adaugarea setului de anvelope in servicii!", er);
            }
        }

        public async Task<Result<SetAnvelopeDto>> UpdateSetAnvelopeAsync(SetAnvelopeDto setAnv)
        {
            try
            {
                var oldSetAnv = setAnv.Copy();

                // case: changed status in other than InRaft =>  empty the position for this set
                if (setAnv.OldPozitieId != null && setAnv.StatusCurent != "InRaft")
                {
                    _hotelPositionsService.UpdatePosition(setAnv.OldPozitieId.Value, setAnv.NrBucati, OperatiunePozitie.Scoatere);
                }

                // case: was inRaft, will be in raft => position changed
                if (setAnv.OldPozitieId != null && setAnv.StatusCurent == "InRaft" && setAnv.PozitieId != null && setAnv.NrBucati == setAnv.OldNumarBucati)
                {
                    _hotelPositionsService.UpdatePosition(setAnv.OldPozitieId.Value, setAnv.NrBucati, OperatiunePozitie.Scoatere);
                    _hotelPositionsService.UpdatePosition(setAnv.PozitieId.Value, setAnv.NrBucati, OperatiunePozitie.Adaugare);
                }

                // case: no change for position but change for nr of anv => 
                if (setAnv.OldPozitieId != null && setAnv.StatusCurent == "InRaft" && setAnv.NrBucati != setAnv.OldNumarBucati && setAnv.PozitieId == null)
                {
                    setAnv.PozitieId = setAnv.OldPozitieId;
                    // case we have added some new anv to this set => we have to also update the nr inside position
                    if (setAnv.NrBucati > setAnv.OldNumarBucati)
                    {
                        var newNrBuc = setAnv.NrBucati - setAnv.OldNumarBucati;
                        var operatiune = OperatiunePozitie.Adaugare;
                        _hotelPositionsService.UpdatePosition(setAnv.PozitieId.Value, newNrBuc, operatiune);
                    }
                    // case we have removed some anv from this set => we have to also update the nr inside position
                    if (setAnv.NrBucati < setAnv.OldNumarBucati)
                    {
                        var newNrBuc = setAnv.OldNumarBucati - setAnv.NrBucati;
                        var operatiune = OperatiunePozitie.Scoatere;
                        _hotelPositionsService.UpdatePosition(setAnv.PozitieId.Value, newNrBuc, operatiune);
                    }
                }

                // case: was InRaft, will be InRaft, Position changed, NrBuc changed

                if (setAnv.OldPozitieId != null && setAnv.StatusCurent == "InRaft" && setAnv.PozitieId != null && setAnv.PozitieId != setAnv.OldPozitieId && setAnv.NrBucati != setAnv.OldNumarBucati)
                {

                    var newNrBuc = setAnv.OldNumarBucati - oldSetAnv.NrBucati;
                    var operatiune = OperatiunePozitie.Adaugare;
                    _hotelPositionsService.UpdatePosition(setAnv.PozitieId.Value, newNrBuc, operatiune);

                }

                // case: was not in hotel but will be in hotel
                if (setAnv.PozitieId != null && setAnv.OldPozitieId == null)
                {
                    _hotelPositionsService.UpdatePosition(setAnv.PozitieId.Value, setAnv.NrBucati, OperatiunePozitie.Adaugare);
                }



                if (setAnv.PozitieId != null)
                {
                    var position = await _hotelPositionsRepository.GetPositionByIdAsync(setAnv.PozitieId.Value);

                    if (!position.Successful)
                    {
                        Log.Error("Ceva nu a mers bine la gasirea pozitiei in metoda de editare anvelope in servicii!");
                        throw new Exception("Ceva nu a mers bine la gasirea pozitiei in metoda de editare anvelope in servicii!");
                    }

                    setAnv.Pozitie = _mapper.Map<HotelPositionsDto>(position.Entity);
                }


                /////////////////// MARCA

                var marca = await _hotelRepository.GetMarcaByLableAsync(setAnv.Marca);
                if (!marca.Successful)
                {
                    Log.Error("Ceva nu a mers bine la gasirea marcii in functie de label in metoda de editare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la gasirea marcii in functie de label in metoda de editare anvelope in servicii!");
                }

                if (marca.Entity == null && !string.IsNullOrEmpty(setAnv.Marca))
                {
                    marca = _hotelRepository.AddMarca(new CaMarca() { Label = setAnv.Marca.ToUpper() });
                }

                if (!marca.Successful)
                {
                    Log.Error("Ceva nu a mers bine la adaugarea marcii in metoda de editare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la adaugarea marcii in metoda de editare anvelope in servicii!");
                }
                setAnv.MarcaId = marca.Entity.Id;



                ///////////////////// FLOTA
                var flota = await _hotelRepository.GetFlotaByLableAsync(setAnv.Flota);
                if (!flota.Successful)
                {
                    Log.Error("Ceva nu a mers bine la gasirea floyei in functie de label in metoda de editare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la gasirea flotei in functie de label in metoda de editare anvelope in servicii!");
                }

                if (flota.Entity == null && !string.IsNullOrEmpty(setAnv.Flota))
                {
                    flota = _hotelRepository.AddFlota(new CaFlota() { Label = setAnv.Flota.ToUpper() });
                }

                if (!flota.Successful)
                {
                    Log.Error("Ceva nu a mers bine la adaugarea flotei in metoda de editare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la adaugarea flotei in metoda de editare anvelope in servicii!");
                }
                setAnv.FlotaId = flota.Entity.Id;

                // ------------------------------------------------------------------------------------------

                var modelForDatabase = _mapper.Map<CaSetAnvelope>(setAnv);
                modelForDatabase.NumarInmatriculare = modelForDatabase.NumarInmatriculare.ToUpper();
                modelForDatabase.NumeClient = modelForDatabase.NumeClient.ToUpper();

                if (modelForDatabase.SerieSasiu != null)
                {
                    modelForDatabase.SerieSasiu = modelForDatabase.SerieSasiu.ToUpper();
                }

                modelForDatabase.DataUltimaModificare = DateTime.Now;

                var value = _hotelRepository.UpdateSetAnvelope(modelForDatabase);
                if (!value.Successful)
                {
                    return Result<SetAnvelopeDto>.ResultError(value.Error);
                }
                var returnModel = _mapper.Map<SetAnvelopeDto>(value.Entity);

                // if we move n(1/2/3) out of 4 we need to create a new set with the remaining values that were not moved to the new position
                //////////////// Creeaza + Adauga set cu anvelopele ramase
                if (oldSetAnv.OldNumarBucati > oldSetAnv.NrBucati && oldSetAnv.PozitieId != oldSetAnv.OldPozitieId && oldSetAnv.PozitieId != null && oldSetAnv.OldPozitieId != null)
                {
                    oldSetAnv.NrBucati = oldSetAnv.OldNumarBucati - oldSetAnv.NrBucati;
                    oldSetAnv.PozitieId = oldSetAnv.OldPozitieId;
                    if (oldSetAnv.NrBucati < 4)
                    {
                        oldSetAnv.Uzura.DrS = null;
                        if (oldSetAnv.NrBucati < 3)
                        {
                            oldSetAnv.Uzura.StS = null;
                            if (oldSetAnv.NrBucati == 1)
                            {
                                oldSetAnv.Uzura.DrF = null;
                            }
                        }
                    }
                    oldSetAnv.Id = 0;

                    var addedOldSet = await AddSetAnvelopeAsync(oldSetAnv, OperatiunePozitie.Setare);

                    if (!addedOldSet.Successful)
                    {
                        Log.Error("Nu am putut muta setul pe noua pozitie pentru ca nu a fost salvat vechiul set.");
                        throw new Exception("Nu am putut muta setul pe noua pozitie pentru ca nu a fost salvat vechiul set.");
                    }
                }

                return Result<SetAnvelopeDto>.ResultOk(returnModel);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la editarea setului de anvelope in servicii!");
                throw new Exception("Ceva nu a mers bine la editarea setului de anvelope in servicii!", er);
            }
        }

        public async Task<Result<List<SetAnvelopeDto>>> SearchAnvelopeAsync(string searchString, int page, int itemsPerPage)
        {
            try
            {
                var result = await _hotelRepository.SearchAnvelopeAsync(searchString, page, itemsPerPage);
                var model = new List<SetAnvelopeDto>();
                if (result.Successful)
                {

                    model = _mapper.Map<List<SetAnvelopeDto>>(result.Entity);

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
                        if (item.FlotaId != null)
                        {
                            var flota = await _hotelRepository.GetFlotaByIdAsync(item.FlotaId.Value);
                            item.Flota = flota.Successful ? flota.Entity.Label : string.Empty;
                        }

                    }
                    return Result<List<SetAnvelopeDto>>.ResultOk(model);
                }
                return Result<List<SetAnvelopeDto>>.ResultError(model, null, "Eroare la citirea din serviciu!");
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la cautarea setului de anvelope in servicii!");
                throw new Exception("Ceva nu a mers bine la cautarea setului de anvelope in servicii!", er);
            }
        }
        public async Task<Result<SetAnvelopeDto>> DeleteSetAnvelopeAsync(int id)
        {
            try
            {
                var set = await _hotelRepository.GetSetAnvelopeByIdAsync(id);
                var setAnv = _mapper.Map<SetAnvelopeDto>(set.Entity);


                if (setAnv.PozitieId != null)
                {
                    _hotelPositionsService.UpdatePosition(setAnv.PozitieId.Value, setAnv.NrBucati, OperatiunePozitie.Scoatere);
                    var position = await _hotelPositionsRepository.GetPositionByIdAsync(setAnv.PozitieId.Value);

                    if (!position.Successful)
                    {
                        Log.Error("Ceva nu a mers bine la stergerea pozitiei in metoda de stergere set anv in servicii!");
                        throw new Exception("Ceva nu a mers bine la stergerea pozitiei in metoda de stergere set anv in servicii!");
                    }
                }

                var value = _hotelRepository.DeleteSetAnvelope(id);
                var model = _mapper.Map<SetAnvelopeDto>(value.Entity);

                return Result<SetAnvelopeDto>.ResultOk(model);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la stergerea setului de anvelope in servicii!");
                throw new Exception("Ceva nu a mers bine la stergerea setului de anvelope in servicii!", er);
            }
        }

        //public async Task<Result<List<Position>>> GetAvailablePositionsAsync()
        //{
        //    try
        //    {
        //        var positions = await _hotelRepository.GetAvailablePositionsAsync();

        //        return positions;
        //    }
        //    catch (Exception er)
        //    {
        //        Log.Error(er, "Ceva nu a mers bine la gasirea pozitiilor disponibile in hotel in servicii!");
        //        throw new Exception("Ceva nu a mers bine la gasirea pozitiilor disponibile in hotel in servicii!", er);
        //    }
        //}

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

        public async Task<Result<List<CaFlota>>> GetFlote()
        {
            try
            {
                var result = await _hotelRepository.GetFlotaAsync();
                return Result<List<CaFlota>>.ResultOk(result.Entity);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea flotei in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea flotei in servicii!", er);
            }
        }

        public async Task<DataTable> GenerateDataForExcel()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[23] {
                                            new DataColumn("NumeClient"),
                                            new DataColumn("NumarInmatriculare"),
                                            new DataColumn("NumarTelefon"),
                                            new DataColumn("SerieSasiu"),
                                            new DataColumn("Rand"),
                                            new DataColumn("Pozitie"),
                                            new DataColumn("Interval"),
                                            new DataColumn("LocuriOcupate"),
                                            new DataColumn("Marca"),
                                            new DataColumn("Flota"),
                                            new DataColumn("NrBucati"),
                                            new DataColumn("Diametru"),
                                            new DataColumn("Latime"),
                                            new DataColumn("Inaltime"),
                                            new DataColumn("DOT"),
                                            new DataColumn("StangaFata"),
                                            new DataColumn("StangaSpate"),
                                            new DataColumn("DreaptaFata"),
                                            new DataColumn("DreaptaSpate"),
                                            new DataColumn("TipSezon"),
                                            new DataColumn("Observatii"),
                                            new DataColumn("StatusCurent"),
                                            new DataColumn("DataUltimaModificare") });

            var allAnv = await SearchAnvelopeAsync(string.Empty, 1, int.MaxValue);
            var model = _mapper.Map<List<SetAnvelopeDto>>(allAnv.Entity);


            var setanvelope = from anvelope in model
                              select anvelope;

            foreach (var anvelope in setanvelope)
            {
                dt.Rows.Add(anvelope.NumeClient,
                    anvelope.NumarInmatriculare,
                    anvelope.NumarTelefon,
                    anvelope.SerieSasiu,
                    anvelope.Pozitie.Rand,
                    anvelope.Pozitie.Pozitie,
                    anvelope.Pozitie.Interval,
                    anvelope.Pozitie.Locuriocupate,
                    anvelope.Marca,
                    anvelope.Flota,
                    anvelope.NrBucati,
                    anvelope.Dimensiuni.Diam,
                    anvelope.Dimensiuni.Lat,
                    anvelope.Dimensiuni.H,
                    anvelope.Dimensiuni.Dot,
                    anvelope.Uzura.StF,
                    anvelope.Uzura.StS,
                    anvelope.Uzura.DrF,
                    anvelope.Uzura.DrS,
                    anvelope.TipSezon,
                    anvelope.Observatii,
                    anvelope.StatusCurent,
                    anvelope.DataUltimaModificare);
            }
            return dt;
        }

        public Result<SetAnvelopeDto> AddSetAnvelope(SetAnvelopeDto setAnv, OperatiunePozitie operatiunePoz = OperatiunePozitie.Adaugare)
        {
            throw new NotImplementedException();
        }
    }
}
