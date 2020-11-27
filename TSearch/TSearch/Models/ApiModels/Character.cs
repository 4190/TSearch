using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSearch.Models.ApiModels
{
    public class Character
    {
        public Characters characters { get; set; }
    }

    public class Characters
    {
        public string error { get; set; }
        public CharacterData data { get; set; }
    }

    public class CharacterData
    {
        public string vocation { get; set; }
        public int level { get; set; }
        public string world { get; set; }
    }
}
