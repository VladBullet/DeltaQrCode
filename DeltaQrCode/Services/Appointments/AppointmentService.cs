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
                var model = _mapper.Map<AppointmentDto>(value);
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
                var app = _mapper.Map<CaAppointments>(appointment);
                app.Deleted = false;
                app.ConfirmedCode = GuidHelper.RandomGuid();
                //var marca = await _appointmentsRepository.GetMarcaByLableAsync(setAnv.Marca);
                //if (!marca.Successful)
                //{
                //    return Result<SetAnvelopeDto>.ResultError(marca.Error, "Problema la gasirea marcii!");
                //}

                //if (marca.Entity == null && !string.IsNullOrEmpty(setAnv.Marca))
                //{
                //    marca = await _appointmentsRepository.AddMarcaAsync(new CaMarca() { Label = setAnv.Marca });
                //}

                //if (!marca.Successful)
                //{
                //    return Result<SetAnvelopeDto>.ResultError(marca.Error, "Problema la adaugarea marcii!");
                //}
                //setAnv.MarcaId = marca.Entity.Id;

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
                var value = await _appointmentsRepository.GetAppointmentsAsync(date);
                var model = new List<AppointmentDto>();
                model = _mapper.Map<List<AppointmentDto>>(value.Entity);

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

    }
}
