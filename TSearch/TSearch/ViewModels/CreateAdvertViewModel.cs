using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TSearch.DTO;
using TSearch.Models;
using TSearch.Models.ApiModels;


namespace TSearch.ViewModels
{
    public class CreateAdvertViewModel
    {
        public AdvertDTO Ad { get; set; }
        public List<World> WorldsList { get; set; }
        public List<Vocation> VocList { get; set; }  
    }

    public class Vocation
    {
        public string Name { get; set; }
    }
}
