using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSearch.Models;
using TSearch.DTO;

namespace TSearch.Controllers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Advert, AdvertDTO>().ReverseMap();
        }
    }
}
