using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TSearch.DTO;
using TSearch.Models;
using TSearch.Models.ApiModels;

namespace TSearch.ViewModels
{
    public class BoardAdvertViewModel
    {
        public List<AdvertDTO> AdsList { get; set; }
        public AdvertDTO FilterFormAdvert { get; set; }
        public List<World> WorldsList { get; set; }
        public List<Vocation> VocList { get; set; }
    }
}
