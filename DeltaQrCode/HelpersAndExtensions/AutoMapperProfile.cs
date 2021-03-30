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
                .ForMember(d => d.Dimensiuni, m => m.MapFrom(s => s.Dimensiuni.ToDimensiuniFromJsonString()))
                .ForMember(d => d.DimensiuniString, m => m.MapFrom(s => s.Dimensiuni))
                .ForMember(d => d.Uzura, m => m.MapFrom(s => s.Uzura.ToUzuraFromJsonString()))
                .ForMember(d => d.UzuraString, m => m.MapFrom(s => s.Uzura))
                .ForMember(d => d.Position, m => m.MapFrom(s => new Position(s.Rand, s.Pozitie, s.Interval)));
            CreateMap<SetAnvelopeDto, CaSetAnvelope>()
                .ForMember(d => d.Dimensiuni, m => m.MapFrom(s => s.Dimensiuni.ToCustomString().ToJson()))
                .ForMember(d => d.Uzura, m => m.MapFrom(s => s.Uzura.ToCustomString().ToJson()))
                .ForMember(d => d.Rand, m => m.MapFrom(s => s.Position.Rand))
                .ForMember(d => d.Pozitie, m => m.MapFrom(s => s.Position.Poz))
                .ForMember(d => d.Interval, m => m.MapFrom(s => s.Position.Interval));


            CreateMap<CaAppointment, AppointmentVM>().ReverseMap();

            CreateMap<AddEditSetAnvelopeVM, SetAnvelopeDto>()
            .ForMember(d => d.Dimensiuni, m => m.Ignore())
            .ForMember(d => d.Uzura, m => m.Ignore())
            .ForMember(d => d.DimensiuniString, m => m.Ignore())
            .ForMember(d => d.UzuraString, m => m.Ignore())
            .ForMember(d => d.Position, m => m.MapFrom(s => s.PozitieInRaft.ToPosition())).ReverseMap();

        }
    }
}
