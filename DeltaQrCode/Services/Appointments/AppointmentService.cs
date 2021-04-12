using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.InteropServices.ComTypes;
    using System.Security.Cryptography.X509Certificates;

    using AutoMapper;

    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.Repositories;
    using DeltaQrCode.ViewModels;
    using DeltaQrCode.ViewModels.Appointments;
    using Microsoft.Extensions.Logging;

    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentsRepository _appointmentsRepository;

        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AppointmentService(IAppointmentsRepository appointmentsRepository, IMapper mapper, ILogger logger)
        {
            _appointmentsRepository = appointmentsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<AppointmentDto>> GetAppointmentByIdAsync(int id)
        {
            try
            {
                var value = await _appointmentsRepository.GetAppointmentByIdAsync(id);
                var model = _mapper.Map<AppointmentDto>(value.Entity);

                if (value.Entity.ServiciuId != null)
                {
                    var serviciu = await _appointmentsRepository.GetServiceTypeByIdAsync(value.Entity.ServiciuId.Value);
                    model.Serviciu = serviciu.Successful ? serviciu.Entity.Label : string.Empty;
                }

                return Result<AppointmentDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la gasirea programarii in functie de id in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea programarii in functie de id in servicii!", er);
            }
        }

        public async Task<Result<AppointmentDto>> AddAppointmentAsync(AppointmentDto appointment)
        {
            try
            {

                var serviciu = await _appointmentsRepository.GetServiceTypeByLableAsync(appointment.Serviciu);
                if (!serviciu.Successful)
                {
                    //_logger.LogError("Ceva nu a mers bine la gasirea tipului de serviciu in metoda de adaugare programare(services)!");
                    //throw new Exception("Ceva nu a mers bine la gasirea tipului de serviciu in metoda de adaugare programare(services)!");
                    return Result<AppointmentDto>.ResultError(serviciu.Error, "Problema la gasirea tipului de serviciu!");
                }

                if (serviciu.Entity == null && !string.IsNullOrEmpty(appointment.Serviciu))
                {
                    serviciu = await _appointmentsRepository.AddServiceTypeAsync(new CaServicetypes() { Label = appointment.Serviciu });
                }

                if (!serviciu.Successful)
                {
                    //_logger.LogError("Ceva nu a mers bine la adaugarea tipului de serviciu in metoda de adaugare programare(services)!");
                    //throw new Exception("Ceva nu a mers bine la adaugarea tipului de serviciu in metoda de adaugare programare(services)!");
                    return Result<AppointmentDto>.ResultError(serviciu.Error, "Problema la adaugareatipului de serviciu!");
                }

                appointment.ServiciuId = serviciu.Entity.Id;

                var app = _mapper.Map<CaAppointments>(appointment);
                app.Deleted = false;
                app.ConfirmedCode = GuidHelper.RandomGuid();
                app.DataIntroducere = DateTime.Now;

                var value = await _appointmentsRepository.AddAppointmentAsync(app);
                var model = _mapper.Map<AppointmentDto>(value.Entity);
                return Result<AppointmentDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la adaugarea programarii in servicii!");
                throw new Exception("Ceva nu a mers bine la adaugarea programarii in servicii!", er);
            }
        }

        public async Task<Result<AppointmentDto>> UpdateAppointmentAsync(AppointmentDto appt)
        {
            try
            {
                var serviciu = await _appointmentsRepository.GetServiceTypeByLableAsync(appt.Serviciu);
                if (!serviciu.Successful)
                {
                    //_logger.LogError("Ceva nu a mers bine la adaugarea tipului de serviciu in metoda de adaugare programare(services)!");
                    //throw new Exception("Ceva nu a mers bine la adaugarea tipului de serviciu in metoda de adaugare programare(services)!");
                    return Result<AppointmentDto>.ResultError(serviciu.Error, "Problema la gasirea tipului de serviciu!");
                }

                if (serviciu.Entity == null && !string.IsNullOrEmpty(appt.Serviciu))
                {
                    serviciu = await _appointmentsRepository.AddServiceTypeAsync(new CaServicetypes() { Label = appt.Serviciu });
                }

                if (!serviciu.Successful)
                {
                    //_logger.LogError("Ceva nu a mers bine la adaugarea tipului de serviciu in metoda de adaugare programare(services)!");
                    //throw new Exception("Ceva nu a mers bine la adaugarea tipului de serviciu in metoda de adaugare programare(services)!");
                    return Result<AppointmentDto>.ResultError(serviciu.Error, "Problema la adaugarea marcii!");
                }

                appt.ServiciuId = serviciu.Entity.Id;


                var confirmed = await ConfirmAppointmentAsync(appt.Id, appt.Confirmed);

                appt.LastModified = DateTime.Now;

                var app = _mapper.Map<CaAppointments>(appt);
                var value = await _appointmentsRepository.UpdateAppointmentAsync(app);
                var model = _mapper.Map<AppointmentDto>(value.Entity);
                return Result<AppointmentDto>.ResultOk(model);

            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la editarea programarii in servicii!");
                throw new Exception("Ceva nu a mers bine la editarea programarii in servicii!", er);
            }
        }

        public async Task<Result<AppointmentDto>> DeleteAppointmentAsync(int id)
        {
            try
            {
                var value = await _appointmentsRepository.DeleteAppointmentAsync(id);
                var model = _mapper.Map<AppointmentDto>(value.Entity);

                return Result<AppointmentDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la stergerea programarii in servicii!");
                throw new Exception("Ceva nu a mers bine la stergerea programarii in servicii!", er);
            }
        }

        public async Task<Result<AppointmentDto>> ConfirmAppointmentAsync(int id, bool confirm)
        {
            try
            {
                var value = await _appointmentsRepository.ConfirmAppointmentAsync(id, confirm);
                var model = _mapper.Map<AppointmentDto>(value.Entity);

                return Result<AppointmentDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la confirmarea programarii in servicii!");
                throw new Exception("Ceva nu a mers bine la confirmarea programarii in servicii!", er);
            }
        }

        public async Task<Result<List<AppointmentDto>>> GetAppointmentsAsync(DateTime date)
        {
            try
            {
                var result = await _appointmentsRepository.GetAppointmentsAsync(date);
                var model = new List<AppointmentDto>();
                if (result.Successful)
                {
                    model = _mapper.Map<List<AppointmentDto>>(result.Entity.OrderBy(x => x.OraInceput.TotalMinutes));

                    foreach (var item in model)
                    {
                        if (item.ServiciuId != null)
                        {
                            var marca = await _appointmentsRepository.GetServiceTypeByIdAsync(item.ServiciuId.Value);
                            item.Serviciu = marca.Successful ? marca.Entity.Label : string.Empty;
                        }
                    }

                }

                return Result<List<AppointmentDto>>.ResultOk(model);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la gasirea programarii in functie de data in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea programarii in functie de data in servicii!", er);
            }
        }

        public async Task<Result<List<CaServicetypes>>> GetServiceTypes()
        {
            try
            {
                var result = await _appointmentsRepository.GetServiceTypesAsync();
                return Result<List<CaServicetypes>>.ResultOk(result.Entity);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la gasirea tipului de serviciu in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea tipului de serviciu in servicii!", er);
            }
        }

        public async Task<Result<AvailableIntervalDto>> DateAndHourIsAvailable(DateTime selectedDate, TimeSpan selectedOraInceput, int selectedDurata, int selectedRampId)
        {
            try
            {
                var dbList = await _appointmentsRepository.GetAppointmentsAsync(selectedDate);
                var apptsList = dbList.Entity.Where(x => x.RampId == selectedRampId); // lista cu appt pt data si rampa

                if (!apptsList.Any())
                {
                    return Result<AvailableIntervalDto>.ResultOk(new AvailableIntervalDto(true, new List<TimeSpan>()));
                }
                var apptsDto = _mapper.Map<List<AppointmentDto>>(apptsList);
                var occupied = new List<Tuple<TimeSpan, TimeSpan>>();
                var free = new List<TimeSpan>();
                occupied.Add(new Tuple<TimeSpan, TimeSpan>(new TimeSpan(7, 30, 0), new TimeSpan(8, 0, 0)));
                foreach (var item in apptsDto)
                {
                    occupied.Add(new Tuple<TimeSpan, TimeSpan>(item.OraInceput, item.OraSfarsit));
                }

                occupied.Add(new Tuple<TimeSpan, TimeSpan>(new TimeSpan(18, 0, 0), new TimeSpan(18, 30, 0)));

                occupied.OrderBy(x => x.Item1.TotalMinutes);

                for (int i = 0; i < occupied.Count - 1; i++)
                {
                    var window = occupied[i + 1].Item1 - occupied[i].Item2;
                    if (window.TotalMinutes >= selectedDurata)
                    {
                        var totalMinutes = window.TotalMinutes - selectedDurata;
                        var step = selectedDurata + occupied[i].Item2.TotalMinutes;
                        while (step <= occupied[i + 1].Item1.TotalMinutes)
                        {

                            var freeTime = TimeSpan.FromMinutes(step - selectedDurata);
                            free.Add(freeTime);
                            step += 30;
                            //totalMinutes -= 30;
                            //var freeapptMinutes = occupied[i].Item2.TotalMinutes + count * selectedDurata;
                            //var freeTimeSpan = TimeSpan.FromMinutes(freeapptMinutes);
                            //free.Add(freeTimeSpan);
                            //count += 1;
                        }
                    }
                }

                var result = new AvailableIntervalDto(false, free);

                if (free.Contains(selectedOraInceput))
                {
                    result.SelectedIsAvailable = true;
                    return Result<AvailableIntervalDto>.ResultOk(result);
                }

                return Result<AvailableIntervalDto>.ResultOk(result);
            }

            catch (Exception er)
            {
                _logger.LogError(er, "Ceva nu a mers bine la extragerea orelor disponibile pentru programari in servicii!");
                throw new Exception("Ceva nu a mers bine la extragerea orelor disponibile pentru programari in servicii!", er);
            }


        }
    }
}
