using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSearch.Models.ApiModels
{
    class ApiWorlds
    {
        public class Worlds
        {
            public List<World> allworlds { get; set; }
        }
        public Worlds worlds { get; set; }
    }
    public class World
    {
        public string name { get; set; }
        public int online { get; set; }
        public string location { get; set; }
        public string worldtype { get; set; }
        public string additional { get; set; }
    }
}
