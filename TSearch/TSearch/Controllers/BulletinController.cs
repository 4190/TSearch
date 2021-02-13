using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using TSearch.Data;
using TSearch.DTO;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IManageAdvertsService _advertsService;
        private readonly IManageGameCharacterService _charactersService;

        public BulletinController(UserManager<ApplicationUser> userManager, 
            IManageAdvertsService advertsService,
            IManageGameCharacterService characterService)
        {
            _charactersService = characterService;
            _userManager = userManager;
            _advertsService = advertsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Board(BoardAdvertViewModel model)
        {
            List<AdvertDTO> adsList = _advertsService.GetAllAdverts().Result;

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

        [Authorize]
        public IActionResult Create()
        {
            CreateAdvertViewModel viewModel = new CreateAdvertViewModel()
            {
                OwnedCharactersList = _charactersService.GetUserCharacters(_userManager.GetUserAsync(User).Result.Id)
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertViewModel model)
        {
            
            ApplicationUser user = _userManager.GetUserAsync(HttpContext.User).Result;

            if(_advertsService.CheckIfCharacterHasAdvert(model.SelectedCharacterId))
            {
                ModelState.AddModelError(string.Empty, "This character has advert already");
                model.OwnedCharactersList = _charactersService.GetUserCharacters(_userManager.GetUserAsync(User).Result.Id);
                return View(model);
            }

            var result = await _advertsService.Create(model.Ad, user, model.SelectedCharacterId);
            if (result.Succeeded)
            {
                return RedirectToAction("MyAdverts", new { userName = user.UserName });
            }

            ModelState.AddModelError(string.Empty, "This character does not exist");
            return View(model);
        }

        [Route("Bulletin/Details/{id}")]
        public IActionResult Details(int id)
        {
            AdvertDTO model = _advertsService.GetAdvert(id).Result;

            return View(model);
        }

        [Authorize]
        public IActionResult MyAdverts(string userName)
        {
            MyAdvertsViewModel model = new MyAdvertsViewModel()
            {
                MyAdvertsList = _advertsService.GetUserAdverts(userName).Result
            };
            return View(model);
        }

        private ApiWorlds GetGameWorldsList()
        {
            var tibiaApiClient = new RestClient("https://api.tibiadata.com/v2");
            tibiaApiClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
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

