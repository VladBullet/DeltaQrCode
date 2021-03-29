using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services
{
    using AutoMapper;

    using DeltaQrCode.Models;
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

        public async Task<Result<AppointmentsVM>> GetAppointmentByIdAsync(int id)
        {
            try
            {
                var value = await _appointmentsRepository.GetAppointmentByIdAsync(id);
                var model = _mapper.Map<AppointmentsVM>(value);
                return Result<AppointmentsVM>.ResultOk(model);
            }
            catch (Exception er)
            {
                return Result<AppointmentsVM>.ResultError(null, er, "Ceva nu a mers bine la gasirea programarii!");
            }
        }

        public async Task<Result<AppointmentsVM>> AddAppointmentAsync(AppointmentsVM appointment)
        {
            try
            {
                var app = _mapper.Map<CaAppointment>(appointment);
                var value = await _appointmentsRepository.AddAppointmentAsync(app);
                var model = _mapper.Map<AppointmentsVM>(value);
                return Result<AppointmentsVM>.ResultOk(model);

            }
            catch (Exception er)
            {
                return Result<AppointmentsVM>.ResultError(null, er, "Ceva nu a mers bine la adaugarea programarii!");
            }
        }

        public async Task<Result<AppointmentsVM>> UpdateAppointmentAsync(AppointmentsVM setAnv)
        {
            try
            {
                var app = _mapper.Map<CaAppointment>(setAnv);
                var value = await _appointmentsRepository.UpdateAppointmentAsync(app);
                var model = _mapper.Map<AppointmentsVM>(value);
                return Result<AppointmentsVM>.ResultOk(model);

            }
            catch (Exception er)
            {
                return Result<AppointmentsVM>.ResultError(null, er, "Ceva nu a mers bine la modificarea programarii!");
            }
        }

        public async Task<Result<AppointmentsVM>> CancelAppointmentAsync(int id)
        {
            try
            {
                var value = await _appointmentsRepository.CancelAppointmentAsync(id);
                var model = _mapper.Map<AppointmentsVM>(value);

                return Result<AppointmentsVM>.ResultOk(model);
            }
            catch (Exception e)
            {
                return Result<AppointmentsVM>.ResultError(e, "Ceva nu a mers bine la anularea programarii!");
            }
        }

        public async Task<Result<AppointmentsVM>> ConfirmAppointmentAsync(int id)
        {
            try
            {
                var value = await _appointmentsRepository.ConfirmAppointmentAsync(id);
                var model = _mapper.Map<AppointmentsVM>(value);

                return Result<AppointmentsVM>.ResultOk(model);
            }
            catch (Exception e)
            {
                return Result<AppointmentsVM>.ResultError(e, "Ceva nu a mers bine la confirmarea programarii!");
            }
        }

        public async Task<Result<List<AppointmentsVM>>> GetAppointmentsAsync(DateTime date)
        {
            try
            {
                var value = await _appointmentsRepository.GetAppointmentsAsync(date);
                var model = _mapper.Map<List<AppointmentsVM>>(value);

                return Result<List<AppointmentsVM>>.ResultOk(model);
            }
            catch (Exception e)
            {
                return Result<List<AppointmentsVM>>.ResultError(e, "Ceva nu a mers bine la gasirea programarilor pentru data ceruta!");
            }
        }

    }
}
