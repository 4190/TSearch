using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSearch.Models;

namespace TSearch.Services
{
    public interface IManageAdvertsService
    {
        public Task<List<Advert>> GetAllAdverts();
        public List<Advert> GetFiltered(List<Advert> advertList, Advert filter);
        public void Create(Advert advert);
    }
}
