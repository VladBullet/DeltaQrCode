﻿namespace DeltaQrCode.HelpersAndExtensions
{
    using AutoMapper;

    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.ViewModels.Appointments;
    using DeltaQrCode.ViewModels.HotelAnvelope;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<CaAnvelopa, AnvelopaDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Dimensiuni, m => m.MapFrom(s => s.Dimensiuni.ToDimensiuniFromJsonString()))
                .ForMember(d => d.DimensiuniString, m => m.MapFrom(s => s.Dimensiuni))
                //.ForMember(d => d.Uzura, m => m.MapFrom(s => s.Uzura.ToUzuraFromJsonString()))
                //.ForMember(d => d.UzuraString, m => m.MapFrom(s => s.Uzura))
                ;
            CreateMap<AnvelopaDto, CaAnvelopa>()

                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Dimensiuni, m => m.MapFrom(s => s.Dimensiuni.ToJson()))
                //.ForMember(d => d.Uzura, m => m.MapFrom(s => s.Uzura.ToJson()))
                //.ForMember(d => d.Rand, m => m.MapFrom(s => s.Position.Rand))
                //.ForMember(d => d.Pozitie, m => m.MapFrom(s => s.Position.Poz))
                //.ForMember(d => d.Interval, m => m.MapFrom(s => s.Position.Interval));
                ;



            CreateMap<AnvelopaVM, AnvelopaDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Dimensiuni, m => m.MapFrom(s => new Dimensiuni(s.Diametru, s.Latime, s.Inaltime, s.Dot)))
                .ForMember(d => d.OldDimensiuni, m => m.MapFrom(s => new Dimensiuni(s.OldDiametru, s.OldLatime, s.OldInaltime, s.OldDot)))
                //.ForMember(d => d.Uzura, m => m.MapFrom(s => new Uzura(s.StangaFata, s.StangaSpate, s.DreaptaFata, s.DreaptaSpate,s.Optional1,s.Optional2)))
                //.ForMember(d => d.OldUzura, m => m.MapFrom(s => new Uzura(s.OldStangaFata, s.OldStangaSpate, s.OldDreaptaFata, s.OldDreaptaSpate)))
                .ForMember(d => d.DimensiuniString, m => m.MapFrom(s => s.DimensiuniString));
            //.ForMember(d => d.Pozitie, m => m.MapFrom(s => s.PozitieInRaft.to()))

            CreateMap<AnvelopaDto, AnvelopaVM>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))

                .ForMember(d => d.Diametru, m => m.MapFrom(s => s.Dimensiuni.Diam))
                .ForMember(d => d.Latime, m => m.MapFrom(s => s.Dimensiuni.Lat))
                .ForMember(d => d.Inaltime, m => m.MapFrom(s => s.Dimensiuni.H))
                .ForMember(d => d.Dot, m => m.MapFrom(s => s.Dimensiuni.Dot))

                .ForMember(d => d.OldDiametru, m => m.MapFrom(s => s.Dimensiuni.Diam))
                .ForMember(d => d.OldLatime, m => m.MapFrom(s => s.Dimensiuni.Lat))
                .ForMember(d => d.OldInaltime, m => m.MapFrom(s => s.Dimensiuni.H))
                .ForMember(d => d.OldDot, m => m.MapFrom(s => s.Dimensiuni.Dot))

                //.ForMember(d => d.StangaFata, m => m.MapFrom(s => s.Uzura.StF))
                //.ForMember(d => d.DreaptaFata, m => m.MapFrom(s => s.Uzura.DrF))
                //.ForMember(d => d.StangaSpate, m => m.MapFrom(s => s.Uzura.StS))
                //.ForMember(d => d.DreaptaSpate, m => m.MapFrom(s => s.Uzura.DrS))
                //.ForMember(d => d.Optional1, m => m.MapFrom(s => s.Uzura.Opt1))
                //.ForMember(d => d.Optional2, m => m.MapFrom(s => s.Uzura.Opt2))

                //.ForMember(d => d.OldStangaFata, m => m.MapFrom(s => s.Uzura.StF))
                //.ForMember(d => d.OldDreaptaFata, m => m.MapFrom(s => s.Uzura.DrF))
                //.ForMember(d => d.OldStangaSpate, m => m.MapFrom(s => s.Uzura.StS))
                //.ForMember(d => d.OldDreaptaSpate, m => m.MapFrom(s => s.Uzura.DrS))
                .ForMember(d => d.PozitieInRaft, m => m.MapFrom(s => s.Pozitie.ToDisplayString()));

            CreateMap<AppointmentDto, CaAppointments>().ReverseMap();
            CreateMap<AppointmentVM, AppointmentDto>();
            CreateMap<AppointmentDto, AppointmentVM>().ForMember(d => d.StartTime_Hour, m => m.MapFrom(s => s.OraInceput.Hours))
                .ForMember(d => d.StartTime_Minutes, m => m.MapFrom(s => s.OraInceput.Minutes));

            CreateMap<CaHotelPositions, HotelPositionsDto>();
            CreateMap<HotelPositionsDto, CaHotelPositions>();
            
            CreateMap<CaOperatiuneSchimbAnvelope, SchimbAnvelopeDto>();
            CreateMap<SchimbAnvelopeDto, CaOperatiuneSchimbAnvelope>();

            CreateMap<CaMasina, MasinaDto>();
            CreateMap<MasinaDto, CaMasina>();
            
            CreateMap<CaClientHotel, ClientHotelDto>();
            CreateMap<ClientHotelDto, CaClientHotel>();
            
            CreateMap<CaSetAnvelope, SetAnvelopeDto>();
            CreateMap<SetAnvelopeDto, CaSetAnvelope>();

            CreateMap<SetAnvelopeDto, HotelAnvelopaVm>()
                .ForMember(d => d.SetAnvelope, m => m.MapFrom(s => s));
            CreateMap<HotelAnvelopaVm, SetAnvelopeDto>();
            

            CreateMap<MasinaDto, MasinaVM>();
            CreateMap<MasinaVM, MasinaDto>();
            
            CreateMap<ClientHotelDto, ClientHotelVM>();
            CreateMap<ClientHotelVM, ClientHotelDto>();
            
            CreateMap<SetAnvelopeDto, SetAnvelopeVM>();
            CreateMap<SetAnvelopeVM, SetAnvelopeDto>();




        }
    }
}
