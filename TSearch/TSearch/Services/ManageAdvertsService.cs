using AutoMapper;
using Newtonsoft.Json;
using RestSharp;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TSearch.DTO;
using TSearch.Data;
using TSearch.Data.EFCore;
using TSearch.Models;
using TSearch.Models.ApiModels;
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

        public async Task<Result> Create(AdvertDTO model, ApplicationUser user)
        {
            
            Character character = GetCharacterDetailsIfExists(model.CharacterName);
            character = ChangeVocationNameToShortForm(character);
            if(String.IsNullOrEmpty(character.characters.error))
            {
                Advert ad = mapper.Map<Advert>(model);
                mapper.Map(character, ad);

                ad.ApplicationUser = user;
                ad.AuthorName = user.UserName;

                await advertRepository.Add(ad);

                return new Result { Succeeded = true };
            }
            else
            {
                return new Result { Succeeded = false };
            }
        }

        public Character ChangeVocationNameToShortForm(Character character)
        {
            string str = character.characters.data.vocation;
            string result = "";
            bool v = true;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    v = true;
                }
                else if (str[i] != ' ' && v == true)
                {
                    result += (str[i]);
                    v = false;
                }
            }
            character.characters.data.vocation = result;

            return character;
        }

        public Character GetCharacterDetailsIfExists(string charName)
        {
            var tibiaApiClient = new RestClient("https://api.tibiadata.com/v2/characters/");
            var characterRequest = new RestRequest(charName + ".json", DataFormat.Json);
            var characterResponse = tibiaApiClient.Get(characterRequest);
            Character characterApiResponse = JsonConvert.DeserializeObject<Character>(characterResponse.Content);

            return characterApiResponse;
        }
    }
}
