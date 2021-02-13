using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSearch.Models;

namespace TSearch.Data.EFCore
{
    public class EfCoreGameCharacterRepository : EfCoreRepository<GameCharacter, ApplicationDbContext>
    {
        public EfCoreGameCharacterRepository(ApplicationDbContext context) : base(context)
        {

        }

        public GameCharacter GetByCharacterName(string characterName)
        {
            return  context.Characters.SingleOrDefault(x => x.CharacterName == characterName);
        }
    }
}
