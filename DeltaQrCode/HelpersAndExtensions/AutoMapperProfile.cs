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
                .ForMember(d => d.Dimensiuni, opt =>opt.Ignore())
                .ForMember(d => d.Uzura, opt =>opt.Ignore())
                .ForMember(d => d.Dimensiuni, opt =>opt.Ignore())

                .ReverseMap();
            CreateMap<CaAppointment, AppointmentVM>().ReverseMap();

            CreateMap<AddEditSetAnvelopeVM, SetAnvelopeDto>()
            .ForMember(d => d.Dimensiuni, m => m.MapFrom(s => new Dimensiuni()))
            .ForMember(d => d.Uzura, m => m.MapFrom(s => new Uzura()))
            .ForMember(d => d.Position, m => m.MapFrom(s => s.PozitieInRaft.ToPosition()));

            CreateMap<SetAnvelopeDto, AddEditSetAnvelopeVM>()
                .ForMember(d => d.Diametru, opt => opt.Ignore())
                .ForMember(d => d.Latime, opt => opt.Ignore())
                .ForMember(d => d.Inaltime, opt => opt.Ignore())
                .ForMember(d => d.StangaSpate, opt => opt.Ignore())
                .ForMember(d => d.StangaFata, opt => opt.Ignore())
                .ForMember(d => d.DreaptaFata, opt => opt.Ignore())
                .ForMember(d => d.DreaptaSpate, opt => opt.Ignore());
        }
    }
}
