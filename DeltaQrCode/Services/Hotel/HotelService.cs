using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaQrCode.Services.Hotel
{
    using AutoMapper;
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.Repositories;
    using Serilog;

    public class HotelService : IHotelService
    {
        private readonly IHotelAnvelopeRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelAnvelopeRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<Result<SetAnvelopeDto>> GetSetAnvelopeByIdAsync(int id)
        {
            try
            {
                var value = await _hotelRepository.GetSetAnvelopeByIdAsync(id);
                var model = _mapper.Map<SetAnvelopeDto>(value.Entity);
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
        public async Task<Result<List<SetAnvelopeDto>>> GetAllSetAnvelopeAsync()
        {
            try
            {
                var list = await _hotelRepository.GetAllSetAnvelopeAsync();
                var model = new List<SetAnvelopeDto>();
                model = _mapper.Map<List<SetAnvelopeDto>>(list.Entity);

                return Result<List<SetAnvelopeDto>>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea tuturor seturilor de anvelope in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea tuturor seturilor de anvelope in servicii!", er);
            }
        }

        public async Task<Result<SetAnvelopeDto>> AddSetAnvelopeAsync(SetAnvelopeDto setAnv) 
        {
            try
            {
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
                    flota = await _hotelRepository.AddFlotaAsync(new CaFlota() { Label = setAnv.Flota.ToUpper() });
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
                var value = await _hotelRepository.AddSetAnvelopeAsync(modelForDatabase);
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
                var marca = await _hotelRepository.GetMarcaByLableAsync(setAnv.Marca);
                if (!marca.Successful)
                {
                    Log.Error("Ceva nu a mers bine la gasirea marcii in functie de label in metoda de editare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la gasirea marcii in functie de label in metoda de editare anvelope in servicii!");
                }

                if (marca.Entity == null && !string.IsNullOrEmpty(setAnv.Marca))
                {
                    marca = await _hotelRepository.AddMarcaAsync(new CaMarca() { Label = setAnv.Marca.ToUpper() });
                }

                if (!marca.Successful)
                {
                    Log.Error("Ceva nu a mers bine la adaugarea marcii in metoda de editare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la adaugarea marcii in metoda de editare anvelope in servicii!");
                }
                setAnv.MarcaId = marca.Entity.Id;

                var flota = await _hotelRepository.GetFlotaByLableAsync(setAnv.Flota);
                if (!flota.Successful)
                {
                    Log.Error("Ceva nu a mers bine la gasirea floyei in functie de label in metoda de editare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la gasirea flotei in functie de label in metoda de editare anvelope in servicii!");
                }

                if (flota.Entity == null && !string.IsNullOrEmpty(setAnv.Flota))
                {
                    flota = await _hotelRepository.AddFlotaAsync(new CaFlota() { Label = setAnv.Flota.ToUpper() });
                }

                if (!flota.Successful)
                {
                    Log.Error("Ceva nu a mers bine la adaugarea flotei in metoda de editare anvelope in servicii!");
                    throw new Exception("Ceva nu a mers bine la adaugarea flotei in metoda de editare anvelope in servicii!");
                }
                setAnv.FlotaId = flota.Entity.Id;

                var modelForDatabase = _mapper.Map<CaSetAnvelope>(setAnv);
                modelForDatabase.NumarInmatriculare = modelForDatabase.NumarInmatriculare.ToUpper();
                modelForDatabase.NumeClient = modelForDatabase.NumeClient.ToUpper();
                modelForDatabase.SerieSasiu = modelForDatabase.SerieSasiu.ToUpper();

                modelForDatabase.DataUltimaModificare = DateTime.Now;

                var value = await _hotelRepository.UpdateSetAnvelopeAsync(modelForDatabase);
                if (!value.Successful)
                {
                    return Result<SetAnvelopeDto>.ResultError(value.Error);
                }
                var returnModel = _mapper.Map<SetAnvelopeDto>(value.Entity);
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
                var value = await _hotelRepository.DeleteSetAnvelopeAsync(id);
                var model = _mapper.Map<SetAnvelopeDto>(value.Entity);

                return Result<SetAnvelopeDto>.ResultOk(model);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la stergerea setului de anvelope in servicii!");
                throw new Exception("Ceva nu a mers bine la stergerea setului de anvelope in servicii!", er);
            }
        }

        public async Task<Result<List<Position>>> GetAvailablePositionsAsync()
        {
            try
            {
                var positions = await _hotelRepository.GetAvailablePositionsAsync();

                return positions;
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la gasirea pozitiilor disponibile in hotel in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea pozitiilor disponibile in hotel in servicii!", er);
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
    }
}
