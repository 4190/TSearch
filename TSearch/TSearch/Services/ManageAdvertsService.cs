using AutoMapper;
using Newtonsoft.Json;
using RestSharp;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using TSearch.DTO;
using TSearch.Data;
using TSearch.Data.EFCore;
using TSearch.Models;
using TSearch.Models.ApiModels;


namespace TSearch.Services
{
    public class ManageAdvertsService : IManageAdvertsService
    {
        private readonly IMapper mapper;
        private readonly IManageGameCharacterService characterService;
        private readonly EfCoreAdvertRepository advertRepository;
        
        public ManageAdvertsService(EfCoreAdvertRepository advertRepository, IMapper mapper, IManageGameCharacterService characterService)
        {
            this.characterService = characterService;
            this.mapper = mapper;
            this.advertRepository = advertRepository;
        }

        public async Task<List<AdvertDTO>> GetAllAdverts()
        {
            List<Advert> adList = await advertRepository.GetAll();

            return mapper.Map<List<Advert>, List<AdvertDTO>>(adList);
        }

        public async Task<AdvertDTO> GetAdvert(int id)
        {
            Advert ad = await advertRepository.Get(id);
            return mapper.Map<AdvertDTO>(ad);
        }

        public async Task<List<AdvertDTO>> GetUserAdverts(string userName)
        {
            List<Advert> adList = await advertRepository.GetAll();
            List<AdvertDTO> model = mapper.Map<List<AdvertDTO>>(adList).Where(x => x.AuthorName == userName).ToList();

            return model;
        }

        public List<AdvertDTO> GetFiltered(List<AdvertDTO> advertList, AdvertDTO filter)
        {
            return advertList
                    .Where(x => filter.GameCharacter.World != null ? x.GameCharacter.World == filter.GameCharacter.World : true)
                    .Where(x => filter.GameCharacter.Vocation != null ? x.GameCharacter.Vocation == filter.GameCharacter.Vocation : true)
                    .ToList();
        }

        public bool CheckIfCharacterHasAdvert(int id)
        {
            Advert ad = advertRepository.GetByGameCharacterId(id);
            if(ad != null)
                return true;

            return false;
        }

        public async Task<Result> Create(AdvertDTO model, ApplicationUser user, int selectedCharacterId)
        {
            
            Advert ad = mapper.Map<Advert>(model);
            ad.ApplicationUser = user;
            ad.AuthorName = user.UserName;
            ad.GameCharacterId = selectedCharacterId;

            await advertRepository.Add(ad);

            return new Result { Succeeded = true };
        }
    }
}
