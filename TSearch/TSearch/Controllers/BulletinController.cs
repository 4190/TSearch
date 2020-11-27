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
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IManageAdvertsService _advertsService;

        public BulletinController(UserManager<ApplicationUser> userManager, ApplicationDbContext _context, IManageAdvertsService advertsService)
        {
            _userManager = userManager;
            _advertsService = advertsService;
            this._context = _context;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertViewModel model)
        {
            
            ApplicationUser user = _userManager.GetUserAsync(HttpContext.User).Result;
            var result = await _advertsService.Create(model.Ad, user);
            if (result.Succeeded)
            {
                return RedirectToAction("Board");
            }

            ModelState.AddModelError(string.Empty, "This character does not exist");
            return View(model);
            
        }

        public IActionResult MyAdverts()
        {
            return Content("");
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

