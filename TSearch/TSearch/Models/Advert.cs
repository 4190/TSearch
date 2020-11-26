using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using TSearch.Data;

namespace TSearch.Models
{
    public class Advert : IEntity
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Enter character name")]
        [Display(Name = "Character Name")]
        public string CharacterName { get; set; }

        [Required(ErrorMessage = "Choose a game server")]
        [Display(Name = "Game World")]
        public string ServerName { get; set; }

        [Required]
        public string Vocation { get; set; }

        [Range(0,9999, ErrorMessage ="Value must be numeric from {1} to {2}")]
        [Required]
        [Display(Name = "Minimum teammate level")]
        public int MinLevel { get; set; }

        [Range(0, 9999, ErrorMessage = "Value must be numeric from {1} to {2}")]
        [Required]
        [Display(Name = "Maximum teammate level")]
        public int MaxLevel { get; set; }

        [Display(Name = "Description")]
        public string Text { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
