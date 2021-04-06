using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.HelpersAndExtensions
{
    using AutoMapper;

    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.ViewModels;
    using DeltaQrCode.ViewModels.Appointments;
    using DeltaQrCode.ViewModels.HotelAnvelope;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<CaSetAnvelope, SetAnvelopeDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Dimensiuni, m => m.MapFrom(s => s.Dimensiuni.ToDimensiuniFromJsonString()))
                .ForMember(d => d.DimensiuniString, m => m.MapFrom(s => s.Dimensiuni))
                .ForMember(d => d.Uzura, m => m.MapFrom(s => s.Uzura.ToUzuraFromJsonString()))
                .ForMember(d => d.UzuraString, m => m.MapFrom(s => s.Uzura))
                .ForMember(d => d.Position, m => m.MapFrom(s => new Position(s.Rand, s.Pozitie, s.Interval)))
                ;
            CreateMap<SetAnvelopeDto, CaSetAnvelope>()

                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Dimensiuni, m => m.MapFrom(s => s.Dimensiuni.ToJson()))
                .ForMember(d => d.Uzura, m => m.MapFrom(s => s.Uzura.ToJson()))
                .ForMember(d => d.Rand, m => m.MapFrom(s => s.Position.Rand))
                .ForMember(d => d.Pozitie, m => m.MapFrom(s => s.Position.Poz))
                .ForMember(d => d.Interval, m => m.MapFrom(s => s.Position.Interval));


            CreateMap<CaAppointments, AppointmentVM>().ReverseMap();

            CreateMap<AddEditSetAnvelopeVM, SetAnvelopeDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Dimensiuni, m => m.MapFrom(s => new Dimensiuni(s.Diametru, s.Latime, s.Inaltime)))
                .ForMember(d => d.Uzura, m => m.MapFrom(s => new Uzura(s.StangaFata, s.StangaSpate, s.DreaptaFata, s.DreaptaSpate)))
                .ForMember(d => d.DimensiuniString, m => m.MapFrom(s => s.DimensiuniString))
                .ForMember(d => d.Position, m => m.MapFrom(s => s.PozitieInRaft.ToPosition()));

            CreateMap<SetAnvelopeDto, AddEditSetAnvelopeVM>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Diametru, m => m.MapFrom(s => s.Dimensiuni.Diam))
                .ForMember(d => d.Latime, m => m.MapFrom(s => s.Dimensiuni.Lat))
                .ForMember(d => d.Inaltime, m => m.MapFrom(s => s.Dimensiuni.H))

                .ForMember(d => d.StangaFata, m => m.MapFrom(s => s.Uzura.StF))
                .ForMember(d => d.DreaptaFata, m => m.MapFrom(s => s.Uzura.DrF))
                .ForMember(d => d.StangaSpate, m => m.MapFrom(s => s.Uzura.StS))
                .ForMember(d => d.DreaptaSpate, m => m.MapFrom(s => s.Uzura.DrS))

                .ForMember(d => d.PozitieInRaft, m => m.MapFrom(s => s.Position.PositionString))


                ;


        }
    }
}
