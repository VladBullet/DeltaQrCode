using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services
{
    using System.Data;
    using System.Globalization;
    using System.Text;
    using AutoMapper;
    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.Repositories;
    using DeltaQrCode.Services.Mail;

    using MimeKit.Text;

    using Serilog;

    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly IHttpHelper _httpHelper;


        public AppointmentService(IAppointmentsRepository appointmentsRepository, IMapper mapper, IMailService mailService, IHttpHelper helper)
        {
            _appointmentsRepository = appointmentsRepository;
            _mapper = mapper;
            _mailService = mailService;
            _httpHelper = helper;
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
                Log.Error(er, "Ceva nu a mers bine la gasirea programarii in functie de id in servicii!");
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
                    Log.Error("Ceva nu a mers bine la gasirea tipului de serviciu in metoda de adaugare programare(services)!");
                    throw new Exception("Ceva nu a mers bine la gasirea tipului de serviciu in metoda de adaugare programare(services)!");
                }

                if (serviciu.Entity == null && !string.IsNullOrEmpty(appointment.Serviciu))
                {
                    serviciu = await _appointmentsRepository.AddServiceTypeAsync(new CaServicetypes() { Label = appointment.Serviciu });
                }

                if (!serviciu.Successful)
                {
                    Log.Error("Ceva nu a mers bine la adaugarea tipului de serviciu in metoda de adaugare programare(services)!");
                    throw new Exception("Ceva nu a mers bine la adaugarea tipului de serviciu in metoda de adaugare programare(services)!");
                }

                appointment.ServiciuId = serviciu.Entity?.Id;

                var app = _mapper.Map<CaAppointments>(appointment);

                app.Deleted = false;
                app.ConfirmedCode = GuidHelper.RandomGuid();
                app.DataIntroducere = DateTime.Now;
                app.NumeClient = app.NumeClient.ToUpper();
                app.NumarInmatriculare = app.NumarInmatriculare.ToUpper();
                app.LastModified = DateTime.Now;

                var value = await _appointmentsRepository.AddAppointmentAsync(app);
                var model = _mapper.Map<AppointmentDto>(value.Entity);
                

                if (!string.IsNullOrEmpty(model.EmailClient))
                {
                    var clientUrl = _httpHelper.GetGuestClientLink(model.ConfirmedCode);
                    StringBuilder emailMessage = new StringBuilder("<div>")
                        .AppendLine("Buna ziua!")
                        .AppendLine("</br>")
                        .AppendLine("Va informam ca ati facut o programare pentru data de: ")
                        .Append(model.DataAppointment.ToString("dddd", new CultureInfo("ro-RO")))
                        .Append(", " + model.DataAppointment.ToString("d", new CultureInfo("ro-RO")))
                        .Append(", la ora : " + model.OraInceput.Hours + ":" + (model.OraInceput.Minutes == 0 ? "00" : model.OraInceput.Minutes.ToString()))
                        .Append(", pentru serviciul de " + appointment.Serviciu.ToString() + " in incinta firmei Delta.")
                        .AppendLine("</br>")
                        .AppendLine("Puteti confirma sau anula programarea dvs. folosind urmatorul link : ").Append("<a href=\"" + clientUrl + "\">Accesati programarea dvs.</a>")
                        .AppendLine("</br>")
                        .AppendLine("Accesati adresa service-ului " + "<a href=\"https://goo.gl/maps/eQ3rRpN9bqDmErE58\">aici</a>")
                        .Append("</div>");

                    var sent = await _mailService.SendEmail(model.EmailClient, emailMessage.ToString(), "Delta Auto - Programarea dvs. la service", TextFormat.Html);
                }

                return Result<AppointmentDto>.ResultOk(model);
            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la adaugarea programarii in servicii!");
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
                    Log.Error("Ceva nu a mers bine la gasirea tipului de serviciu in metoda de editare programare(services)!");
                    throw new Exception("Ceva nu a mers bine la gasirea tipului de serviciu in metoda de editare programare(services)!");
                }

                if (serviciu.Entity == null && !string.IsNullOrEmpty(appt.Serviciu))
                {
                    serviciu = await _appointmentsRepository.AddServiceTypeAsync(new CaServicetypes() { Label = appt.Serviciu });
                }

                if (!serviciu.Successful)
                {
                    Log.Error("Ceva nu a mers bine la adaugarea tipului de serviciu in metoda de editare programare(services)!");
                    throw new Exception("Ceva nu a mers bine la adaugarea tipului de serviciu in metoda de editare programare(services)!");
                }

                appt.ServiciuId = serviciu.Entity?.Id;
                appt.LastModified = DateTime.Now;
                appt.NumeClient = appt.NumeClient.ToUpper();
                appt.NumarInmatriculare = appt.NumarInmatriculare.ToUpper();
                appt.LastModified = DateTime.Now;

                var app = _mapper.Map<CaAppointments>(appt);
                var value = await _appointmentsRepository.UpdateAppointmentAsync(app);
                var model = _mapper.Map<AppointmentDto>(value.Entity);
                
                return Result<AppointmentDto>.ResultOk(model);

            }
            catch (Exception er)
            {
                Log.Error(er, "Ceva nu a mers bine la editarea programarii in servicii!");
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
                Log.Error(er, "Ceva nu a mers bine la stergerea programarii in servicii!");
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
                Log.Error(er, "Ceva nu a mers bine la confirmarea programarii in servicii!");
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
                Log.Error(er, "Ceva nu a mers bine la gasirea programarii in functie de data in servicii!");
                throw new Exception("Ceva nu a mers bine la gasirea programarii in functie de data in servicii!", er);
            }
        }


        public async Task<Result<AvailableIntervalDto>> DateAndHourIsAvailable(DateTime selectedDate, TimeSpan selectedOraInceput, int selectedDurata, int selectedRampId, int? apptId = null)
        {
            try
            {
                var dbList = await _appointmentsRepository.GetAppointmentsAsync(selectedDate, selectedRampId);
                var apptsList = dbList.Entity;

                Result<CaAppointments> appt = null;

                if (apptId != null)
                {
                    appt = await _appointmentsRepository.GetAppointmentByIdAsync(apptId.Value);
                }

                if (!apptsList.Any())
                {
                    return Result<AvailableIntervalDto>.ResultOk(new AvailableIntervalDto(true, new List<TimeSpan>()));
                }

                var apptsDto = _mapper.Map<List<AppointmentDto>>(apptsList);
                var occupied = new List<Tuple<TimeSpan, TimeSpan, int>>();
                var free = new List<TimeSpan>();

                occupied.Add(new Tuple<TimeSpan, TimeSpan, int>(new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), 0));

                foreach (var item in apptsDto.OrderBy(x => x.OraInceput.Ticks))
                {
                    occupied.Add(new Tuple<TimeSpan, TimeSpan, int>(item.OraInceput, item.OraSfarsit, item.Id));
                }

                occupied.Add(new Tuple<TimeSpan, TimeSpan, int>(new TimeSpan(18, 0, 0), new TimeSpan(24, 0, 0), 0));
                var occ = new List<Tuple<TimeSpan, TimeSpan, int>>();
                occ.AddRange(occupied);

                if (appt != null && appt.Entity.RampId == selectedRampId)
                {
                    var editedOcc = occupied.FirstOrDefault(x => x.Item3 == apptId);
                    occupied.Remove(editedOcc);
                }

                occupied = occupied.OrderBy(x => x.Item1.Ticks).ToList();

                for (int i = 0; i < occupied.Count - 1; i++)
                {
                    var window = occupied[i + 1].Item1 - occupied[i].Item2;
                    if (window.TotalMinutes >= selectedDurata)
                    {
                        var step = selectedDurata + occupied[i].Item2.TotalMinutes;
                        while (step <= occupied[i + 1].Item1.TotalMinutes)
                        {
                            var freeTime = TimeSpan.FromMinutes(step - selectedDurata);
                            free.Add(freeTime);
                            step += 30;
                        }
                    }
                }

                if (appt != null && appt.Entity.RampId == selectedRampId)
                {
                    var selectedOccIndex = occ.FindIndex(x => x.Item3 == apptId);
                    var prevOcc = occ[selectedOccIndex - 1];
                    var nextOcc = occ[selectedOccIndex + 1];
                    var oraSfarsitSelectata = selectedOraInceput.Add(TimeSpan.FromMinutes(selectedDurata));
                    if (prevOcc.Item2 <= selectedOraInceput && oraSfarsitSelectata <= nextOcc.Item1)
                    {
                        free.Add(selectedOraInceput);
                    }
                }

                free = free.OrderBy(x => x.Ticks).ToList();
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
                Log.Error(er, "Ceva nu a mers bine la extragerea orelor disponibile pentru programari in servicii!");
                throw new Exception("Ceva nu a mers bine la extragerea orelor disponibile pentru programari in servicii!", er);
            }
        }


        public async Task<List<DataTable>> GenerateDataForExcel(DateTime? date = null)
        {
            var list = new List<DataTable>();
            for (int i = 1; i <= 4; i++)
            {
                var rampId = i;

                DataTable dt = new DataTable("Ramp"+rampId);
                dt.Columns.AddRange(new DataColumn[15] {

                                            new DataColumn("NumeClient"),
                                            new DataColumn("NumarInmatriculare"),
                                            new DataColumn("NumarTelefon"),
                                            new DataColumn("EmailClient"),
                                            new DataColumn("DataAppointment"),
                                            new DataColumn("DataIntroducere"),
                                            new DataColumn("OraInceput"),
                                            new DataColumn("DurataInMinute"),
                                            new DataColumn("Serviciu"),
                                            new DataColumn("Observatii"),
                                            new DataColumn("Deleted"),
                                            new DataColumn("Confirmed"),
                                            new DataColumn("ConfirmedDate"),
                                            new DataColumn("LastModified"),
                                            new DataColumn("ChangedByClient") });

                var allAppts = new Result<List<CaAppointments>>();

                if (date == null)
                {
                    allAppts = await _appointmentsRepository.GetAppointmentsAsync();
                } else
                {
                    allAppts = await _appointmentsRepository.GetAppointmentsForDateAsync(date.Value);
                }
                var model = _mapper.Map<List<AppointmentDto>>(allAppts.Entity);


                var appointment = from appts in model
                                  select appts;

                foreach (var appts in appointment.Where(x=>x.RampId == rampId))
                {
                    dt.Rows.Add(appts.NumeClient,
                        appts.NumarInmatriculare,
                        appts.NumarTelefon,
                        appts.EmailClient,
                        appts.DataAppointment,
                        appts.DataIntroducere,
                        appts.OraInceput,
                        appts.DurataInMinute,
                        appts.Serviciu,
                        appts.Observatii,
                        appts.Deleted,
                        appts.Confirmed,
                        appts.ConfirmedDate,
                        appts.LastModified,
                        appts.ChangedByClient);
                }
                list.Add(dt);
            }
            return list;
        }

    }
}
