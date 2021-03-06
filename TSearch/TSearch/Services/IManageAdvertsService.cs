﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TSearch.DTO;
using TSearch.Models;

namespace TSearch.Services
{
    public interface IManageAdvertsService
    {
        public Task<List<AdvertDTO>> GetAllAdverts();
        public Task<AdvertDTO> GetAdvert(int id);
        public List<AdvertDTO> GetFiltered(List<AdvertDTO> advertList, AdvertDTO filter);
        public Task<List<AdvertDTO>> GetUserAdverts(string userName);
        public bool CheckIfCharacterHasAdvert(int id);
        public Task<Result> Create(AdvertDTO advert, ApplicationUser user, int selectedCharacterId);
    }
}
