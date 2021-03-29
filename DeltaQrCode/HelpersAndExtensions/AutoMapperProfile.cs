using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.HelpersAndExtensions
{
    using AutoMapper;

    using DeltaQrCode.Models;
    using DeltaQrCode.ViewModels;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Add as many of these lines as you need to map your objects
            //CreateMap<SetAnvelopeVM, CaSetAnvelope>().ReverseMap();
            CreateMap<SetAnvelopeVM, CaSetAnvelope>()
                .ForMember(d => d.NumarInmatriculare, m => m.MapFrom(s =>
                    s.NumarInmatriculare))
                .ForMember(d => d.NrBucati, m => m.MapFrom(s =>
                    s.NrBucati));


        }
    }
}
