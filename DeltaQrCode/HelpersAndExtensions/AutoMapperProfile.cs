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
            //CreateMap<SetAnvelopeVM, CaSetAnvelope>().ReverseMap();
            CreateMap<SetAnvelopeDto, CaSetAnvelope>().ReverseMap();
            CreateMap<CaAppointment, AppointmentVM>().ReverseMap();

            //CreateMap<SetAnvelopeDto, AddEditSetAnvelopeVM>();

            CreateMap<AddEditSetAnvelopeVM, SetAnvelopeDto>()
            .ForMember(d => d.Dimensiuni.Diam, m => m.MapFrom(s =>
                s.Diametru))
            .ForMember(d => d.Dimensiuni.H, m => m.MapFrom(s =>
                s.Inaltime))
            .ForMember(d => d.Dimensiuni.Lat, m => m.MapFrom(s =>
                s.Latime))
            .ForMember(d => d.Uzura.DrF, m => m.MapFrom(s =>
                s.DreaptaFata))
            .ForMember(d => d.Uzura.DrS, m => m.MapFrom(s =>
                s.DreaptaSpate))
            .ForMember(d => d.Uzura.StF, m => m.MapFrom(s =>
                s.StangaFata))
            .ForMember(d => d.Uzura.StS, m => m.MapFrom(s =>
                s.StangaSpate))
            //.ForMember(d => d.PositionString, m => m.MapFrom(s =>
            //    ))
            //.ForMember(d => d.PositionString, m => m.MapFrom(s =>
            //    s.StangaSpate))
            .ReverseMap();


        }
    }
}
