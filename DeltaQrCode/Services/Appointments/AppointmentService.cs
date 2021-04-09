using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services
{
    using AutoMapper;

    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.Repositories;
    using DeltaQrCode.ViewModels;
    using DeltaQrCode.ViewModels.Appointments;

    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IMapper _mapper;
        public AppointmentService(IAppointmentsRepository appointmentsRepository, IMapper mapper)
        {
            _appointmentsRepository = appointmentsRepository;
            _mapper = mapper;
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
                return Result<AppointmentDto>.ResultError(null, er, "Ceva nu a mers bine la gasirea programarii!");
            }
        }

        public async Task<Result<AppointmentDto>> AddAppointmentAsync(AppointmentDto appointment)
        {
            try
            {

                var serviciu = await _appointmentsRepository.GetServiceTypeByLableAsync(appointment.Serviciu);
                if (!serviciu.Successful)
                {
                    return Result<AppointmentDto>.ResultError(serviciu.Error, "Problema la gasirea tipului de serviciu!");
                }

                if (serviciu.Entity == null && !string.IsNullOrEmpty(appointment.Serviciu))
                {
                    serviciu = await _appointmentsRepository.AddServiceTypeAsync(new CaServicetypes() { Label = appointment.Serviciu });
                }

                if (!serviciu.Successful)
                {
                    return Result<AppointmentDto>.ResultError(serviciu.Error, "Problema la adaugarea marcii!");
                }
                appointment.ServiciuId = serviciu.Entity.Id;

                var app = _mapper.Map<CaAppointments>(appointment);
                app.Deleted = false;
                app.ConfirmedCode = GuidHelper.RandomGuid();

                var value = await _appointmentsRepository.AddAppointmentAsync(app);
                var model = _mapper.Map<AppointmentDto>(value.Entity);
                return Result<AppointmentDto>.ResultOk(model);

            }
            catch (Exception er)
            {
                return Result<AppointmentDto>.ResultError(null, er, "Ceva nu a mers bine la adaugarea programarii!");
            }
        }

        public async Task<Result<AppointmentDto>> UpdateAppointmentAsync(AppointmentDto appt)
        {
            try
            {
                var serviciu = await _appointmentsRepository.GetServiceTypeByLableAsync(appt.Serviciu);
                if (!serviciu.Successful)
                {
                    return Result<AppointmentDto>.ResultError(serviciu.Error, "Problema la gasirea tipului de serviciu!");
                }

                if (serviciu.Entity == null && !string.IsNullOrEmpty(appt.Serviciu))
                {
                    serviciu = await _appointmentsRepository.AddServiceTypeAsync(new CaServicetypes() { Label = appt.Serviciu });
                }

                if (!serviciu.Successful)
                {
                    return Result<AppointmentDto>.ResultError(serviciu.Error, "Problema la adaugarea marcii!");
                }
                appt.ServiciuId = serviciu.Entity.Id;


                var app = _mapper.Map<CaAppointments>(appt);
                var value = await _appointmentsRepository.UpdateAppointmentAsync(app);
                var model = _mapper.Map<AppointmentDto>(value.Entity);
                return Result<AppointmentDto>.ResultOk(model);

            }
            catch (Exception er)
            {
                return Result<AppointmentDto>.ResultError(null, er, "Ceva nu a mers bine la modificarea programarii!");
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
            catch (Exception e)
            {
                return Result<AppointmentDto>.ResultError(e, "Ceva nu a mers bine la anularea programarii!");
            }
        }

        public async Task<Result<AppointmentDto>> ConfirmAppointmentAsync(int id)
        {
            try
            {
                var value = await _appointmentsRepository.ConfirmAppointmentAsync(id);
                var model = _mapper.Map<AppointmentDto>(value.Entity);

                return Result<AppointmentDto>.ResultOk(model);
            }
            catch (Exception e)
            {
                return Result<AppointmentDto>.ResultError(e, "Ceva nu a mers bine la confirmarea programarii!");
            }
        }

        public async Task<Result<List<AppointmentDto>>> GetAppointmentsAsync(DateTime date)
        {
            try
            {
                var result = await _appointmentsRepository.GetAppointmentsAsync(date);
                var model = new List<AppointmentDto>();
                if(result.Successful)
                {
                    model = _mapper.Map<List<AppointmentDto>>(result.Entity);

                    foreach(var item in model)
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
            catch (Exception e)
            {
                return Result<List<AppointmentDto>>.ResultError(e, "Ceva nu a mers bine la gasirea programarilor pentru data ceruta!");
            }
        }

        public async Task<Result<List<CaServicetypes>>> GetServiceTypes()
        {
            try
            {
                var result = await _appointmentsRepository.GetServiceTypesAsync();
                return Result<List<CaServicetypes>>.ResultOk(result.Entity);
            }
            catch (Exception e)
            {
                return Result<List<CaServicetypes>>.ResultError(e);
            }
        }

        public async Task<Result<AppointmentDto>> DateAndHourIsAvailable(DateTime selectedDate, TimeSpan selectedOraInceput, int selectedDurata, int selectedRampId)
        {
            try
            {
                var result = await _appointmentsRepository.GetAppointmentsAsync(selectedDate);
                var apptsList = result.Entity.Where(x => x.RampId == selectedRampId); // lista cu appt pt data si rampa

                var list = new List<AppointmentDto>();
                var model = _mapper.Map<List<AppointmentDto>>(list);
                for (int i = 1; i < 22; i++)
                {
                    list.Where(x => x.Id == i) =
                }



            }

            catch (Exception e)
            {
                return Result<AppointmentDto>.ResultError(e, "Ceva nu a mers bine la gasirea programarilor pentru data ceruta!");
            }


        }
    }
