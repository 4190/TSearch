using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSearch.Data;
using TSearch.Data.EFCore;
using TSearch.Models;


namespace TSearch.Services
{
    public class ManageAdvertsService : IManageAdvertsService
    {
        private readonly ApplicationDbContext _context;
        private readonly EfCoreAdvertRepository advertRepository;
        
        public ManageAdvertsService(ApplicationDbContext _context, EfCoreAdvertRepository advertRepository)
        {
            this._context = _context;
            this.advertRepository = advertRepository;
        }

        public async Task<List<Advert>> GetAllAdverts()
        {
            return await advertRepository.GetAll();

        }

        public List<Advert> GetFiltered(List<Advert> advertList, Advert filter)
        {
            return advertList
                    .Where(x => filter.ServerName != null ? x.ServerName == filter.ServerName : true)
                    .Where(x => filter.Vocation != null ? x.Vocation == filter.Vocation : true)
                    .ToList();

        }

        public async void Create(Advert ad)
        {
            await advertRepository.Add(ad);
        }
    }
}
