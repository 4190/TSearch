using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSearch.Data;

namespace TSearch.Models
{
    public class GameCharacter : IEntity
    {
        public int Id { get; set; }
        public string CharacterName { get; set; }
        public string Vocation { get; set; }
        public int Level { get; set; }
        public string World { get; set; }
        public string VerificationToken { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
