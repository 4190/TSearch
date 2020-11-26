using AutoMapper;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TSearch.DTO;
using TSearch.Data;
using TSearch.Data.EFCore;
using TSearch.Models;
using TSearch.ViewModels;


namespace TSearch.Services
{
    public class ManageAdvertsService : IManageAdvertsService
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext _context;
        private readonly EfCoreAdvertRepository advertRepository;
        
        public ManageAdvertsService(ApplicationDbContext _context, EfCoreAdvertRepository advertRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this._context = _context;
            this.advertRepository = advertRepository;
        }

        public async Task<List<AdvertDTO>> GetAllAdverts()
        {
            List<Advert> adList =  await advertRepository.GetAll();

            return mapper.Map<List<Advert>, List<AdvertDTO>>(adList);

        }

        public List<AdvertDTO> GetFiltered(List<AdvertDTO> advertList, AdvertDTO filter)
        {
            return advertList
                    .Where(x => filter.ServerName != null ? x.ServerName == filter.ServerName : true)
                    .Where(x => filter.Vocation != null ? x.Vocation == filter.Vocation : true)
                    .ToList();

        }

        public async Task Create(AdvertDTO model, ApplicationUser user)
        {
            Advert ad = mapper.Map<Advert>(model);
            ad.ApplicationUser = user;
            ad.Country = user.Country;
            ad.AuthorName = user.UserName;

            await advertRepository.Add(ad);
        }
    }
}
