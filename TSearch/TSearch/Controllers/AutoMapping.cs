using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSearch.Models;
using TSearch.Models.ApiModels;
using TSearch.DTO;
using TSearch.ViewModels;

namespace TSearch.Controllers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Advert, AdvertDTO>().ReverseMap();
            CreateMap<Character, Advert>()
                .ForMember(dest => dest.CharacterLevel, opt => opt.MapFrom(src => src.characters.data.level))
                .ForMember(dest => dest.Vocation, opt => opt.MapFrom(src => src.characters.data.vocation))
                .ForMember(dest => dest.ServerName, opt => opt.MapFrom(src => src.characters.data.world));
        }
    }
}
