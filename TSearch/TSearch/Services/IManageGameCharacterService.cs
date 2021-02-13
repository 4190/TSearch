using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TSearch.DTO;
using TSearch.Models;
using TSearch.Models.ApiModels;

namespace TSearch.Services
{
    public interface IManageGameCharacterService
    {
        public Task AddGameCharacterToDbAsync(GameCharacterDTO model);
        public string ChangeVocationNameToShortForm(string str);
        public bool CheckIfCharacterIsAlreadyOwned(string characterName);
        public GameCharacterDTO GetCharacterById(int id);
        public Character GetCharacterDetailsIfExists(string charName);
        public List<GameCharacterDTO> GetUserCharacters(string userId);
        public GameCharacterDTO MapDetailsFromApi(Character source, GameCharacterDTO dest);
        public bool VerifyToken(GameCharacterDTO model);
    }
}
