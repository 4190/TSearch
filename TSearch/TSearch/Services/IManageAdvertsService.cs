using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TSearch.DTO;
using TSearch.Models;
using TSearch.Models.ApiModels;

namespace TSearch.Services
{
    public interface IManageAdvertsService
    {
        public Task<List<AdvertDTO>> GetAllAdverts();
        public List<AdvertDTO> GetFiltered(List<AdvertDTO> advertList, AdvertDTO filter);
        public Task<Result> Create(AdvertDTO advert, ApplicationUser user);
    }
}
