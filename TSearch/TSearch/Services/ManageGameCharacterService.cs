using AutoMapper;
using Newtonsoft.Json;
using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TSearch.Data.EFCore;
using TSearch.DTO;
using TSearch.Models;
using TSearch.Models.ApiModels;

namespace TSearch.Services
{
    public class ManageGameCharacterService : IManageGameCharacterService
    {
        private readonly IMapper mapper;
        private readonly EfCoreGameCharacterRepository repository;
        RestClient tibiaApiClient = new RestClient("https://api.tibiadata.com/v2/characters/");
        

        public ManageGameCharacterService(IMapper mapper, EfCoreGameCharacterRepository repository)
        {
            tibiaApiClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task AddGameCharacterToDbAsync(GameCharacterDTO model)
        {
            GameCharacter character = new GameCharacter();
            mapper.Map(model, character);
            character.Vocation = ChangeVocationNameToShortForm(character.Vocation);

            await repository.Add(character);
        }

        public string ChangeVocationNameToShortForm(string str)
        {
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
            str = result;

            return str;
        }

        public bool CheckIfCharacterIsAlreadyOwned(string characterName)
        {
            GameCharacter character = repository.GetByCharacterName(characterName);
            if(character != null)
            {
                return true;
            }
            return false;
        }

        public Character GetCharacterDetailsIfExists(string charName)
        {
            var characterRequest = new RestRequest(charName + ".json", DataFormat.Json);
            var characterResponse = tibiaApiClient.Get(characterRequest);
            Character characterApiResponse = JsonConvert.DeserializeObject<Character>(characterResponse.Content);

            return characterApiResponse;
        }

        public List<GameCharacterDTO> GetUserCharacters(string userId)
        {
            List<GameCharacter> userCharacterList = repository.GetAll().Result.Where(x => x.ApplicationUserId == userId).ToList();
            return mapper.Map<List<GameCharacterDTO>>(userCharacterList);
        }

        public GameCharacterDTO GetCharacterById(int id)
        {
            GameCharacter character = repository.Get(id).Result;
            return mapper.Map<GameCharacterDTO>(character);
        }

        public GameCharacterDTO MapDetailsFromApi(Character src, GameCharacterDTO dest)
        {
            return mapper.Map(src, dest);
        }

        public bool VerifyToken(GameCharacterDTO model)
        {
            var character = GetCharacterDetailsIfExists(model.CharacterName);
            if(character.characters.data.comment != null)
            {
                if (character.characters.data.comment.Contains(model.VerificationToken))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
