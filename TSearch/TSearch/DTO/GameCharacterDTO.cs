using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TSearch.Models;

namespace TSearch.DTO
{
    public class GameCharacterDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter character name")]
        public string CharacterName { get; set; }
        public string Vocation { get; set; }
        public int Level { get; set; }
        public string World { get; set; }
        public string VerificationToken { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
