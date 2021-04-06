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

        public async Task<Result<AppointmentVM>> GetAppointmentByIdAsync(int id)
        {
            try
            {
                var value = await _appointmentsRepository.GetAppointmentByIdAsync(id);
                var model = _mapper.Map<AppointmentVM>(value);
                return Result<AppointmentVM>.ResultOk(model);
            }
            catch (Exception er)
            {
                return Result<AppointmentVM>.ResultError(null, er, "Ceva nu a mers bine la gasirea programarii!");
            }
        }

        public async Task<Result<AppointmentVM>> AddAppointmentAsync(AppointmentVM appointment)
        {
            try
            {
                var app = _mapper.Map<CaAppointments>(appointment);
                app.Deleted = false;
                var value = await _appointmentsRepository.AddAppointmentAsync(app);
                var model = _mapper.Map<AppointmentVM>(value);
                return Result<AppointmentVM>.ResultOk(model);

            }
            catch (Exception er)
            {
                return Result<AppointmentVM>.ResultError(null, er, "Ceva nu a mers bine la adaugarea programarii!");
            }
        }

        public async Task<Result<AppointmentVM>> UpdateAppointmentAsync(AppointmentVM setAnv)
        {
            try
            {
                var app = _mapper.Map<CaAppointments>(setAnv);
                var value = await _appointmentsRepository.UpdateAppointmentAsync(app);
                var model = _mapper.Map<AppointmentVM>(value);
                return Result<AppointmentVM>.ResultOk(model);

            }
            catch (Exception er)
            {
                return Result<AppointmentVM>.ResultError(null, er, "Ceva nu a mers bine la modificarea programarii!");
            }
        }

        public async Task<Result<AppointmentVM>> DeleteAppointmentAsync(int id)
        {
            try
            {
                var value = await _appointmentsRepository.DeleteAppointmentAsync(id);
                var model = _mapper.Map<AppointmentVM>(value);

                return Result<AppointmentVM>.ResultOk(model);
            }
            catch (Exception e)
            {
                return Result<AppointmentVM>.ResultError(e, "Ceva nu a mers bine la anularea programarii!");
            }
        }

        public async Task<Result<AppointmentVM>> ConfirmAppointmentAsync(int id)
        {
            try
            {
                var value = await _appointmentsRepository.ConfirmAppointmentAsync(id);
                var model = _mapper.Map<AppointmentVM>(value);

                return Result<AppointmentVM>.ResultOk(model);
            }
            catch (Exception e)
            {
                return Result<AppointmentVM>.ResultError(e, "Ceva nu a mers bine la confirmarea programarii!");
            }
        }

        public async Task<Result<List<AppointmentVM>>> GetAppointmentsAsync(DateTime date)
        {
            try
            {
                var value = await _appointmentsRepository.GetAppointmentsAsync(date);
                var model = _mapper.Map<List<AppointmentVM>>(value);

                return Result<List<AppointmentVM>>.ResultOk(model);
            }
            catch (Exception e)
            {
                return Result<List<AppointmentVM>>.ResultError(e, "Ceva nu a mers bine la gasirea programarilor pentru data ceruta!");
            }
        }

    }
}
