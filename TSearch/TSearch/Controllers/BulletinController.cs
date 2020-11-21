using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TSearch.Data;
using TSearch.Models;
using TSearch.Models.ApiModels;
using TSearch.Services;
using TSearch.ViewModels;
using RestSharp;
using Newtonsoft.Json;

namespace TSearch.Controllers
{
    public class BulletinController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IManageAdvertsService _advertsService;

        public BulletinController(ApplicationDbContext _context, IManageAdvertsService advertsService)
        {
            _advertsService = advertsService;
            this._context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Board(BoardAdvertViewModel model)
        {
            List<Advert> adsList = _advertsService.GetAllAdverts().Result;

            BoardAdvertViewModel viewModel = new BoardAdvertViewModel()
            {
                AdsList = adsList,
                FilterFormAdvert = model.FilterFormAdvert,
                VocList = GetVocationList(),
                WorldsList = GetGameWorldsList().worlds.allworlds
            };
            if (viewModel.FilterFormAdvert == null)
            {
                return View(viewModel);
            }
            else
            {
                adsList = _advertsService.GetFiltered(adsList, model.FilterFormAdvert);

                viewModel.AdsList = adsList;
                return View(viewModel);
            }
        }

        public IActionResult Create()
        {
            var worldsListApiResponse = GetGameWorldsList();
            List<Vocation> vocations = GetVocationList();
            
            CreateAdvertViewModel viewModel = new CreateAdvertViewModel
            {
                WorldsList = worldsListApiResponse.worlds.allworlds,
                VocList = vocations
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(Advert ad)
        {
            _advertsService.Create(ad);
            return RedirectToAction("Board");
        }

        private ApiWorlds GetGameWorldsList()
        {
            var tibiaApiClient = new RestClient("https://api.tibiadata.com/v2/");
            var worldsRequest = new RestRequest("worlds.json", DataFormat.Json);
            var worldsResponse = tibiaApiClient.Get(worldsRequest);
            ApiWorlds worldsListApiResponse = JsonConvert.DeserializeObject<ApiWorlds>(worldsResponse.Content);

            return worldsListApiResponse;
        }

        private List<Vocation> GetVocationList()
        {
            return new List<Vocation>()
            {
                new Vocation { Name = "EK"},
                new Vocation { Name = "RP"},
                new Vocation { Name = "MS"},
                new Vocation { Name = "ED"}
            };
        }
    }
}

/*
var countriesApiClient = new RestClient("https://restcountries.eu/rest/v2/");
var countriesRequest = new RestRequest("all", DataFormat.Json);
var countriesResponse = countriesApiClient.Get(countriesRequest);
var countriesListApiResonse = JsonConvert.DeserializeObject<List<Country>>(countriesResponse.Content);
*/
