using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using TSearch.Models;

namespace TSearch.DTO
{
    public class AdvertDTO
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }

        [Range(0, 9999, ErrorMessage = "Value must be numeric from {1} to {2}")]
        [Required]
        [Display(Name = "Minimum teammate level")]
        public int MinLevel { get; set; }

        [Range(0, 9999, ErrorMessage = "Value must be numeric from {1} to {2}")]
        [Required]
        [Display(Name = "Maximum teammate level")]
        public int MaxLevel { get; set; }

        [Display(Name = "Description")]
        public string Text { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public GameCharacterDTO GameCharacter { get; set; }
        public string ApplicationUserId { get; set; }
        public int GameCharacterId { get; set; }
    }
}
