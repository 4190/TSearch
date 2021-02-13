using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

using TSearch.AccountViewModels;
using TSearch.DTO;
using TSearch.Models;
using TSearch.Services;
using TSearch.ViewModels;

namespace TSearch.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IManageGameCharacterService characterService;
        private readonly ILogger _logger;

        public AccountController(
                    UserManager<ApplicationUser> userManager,
                    SignInManager<ApplicationUser> signInManager,
                    ILogger<AccountController> logger,
                    IManageGameCharacterService characterService)
        {
            this.characterService = characterService;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) { return Redirect(returnUrl); }
            else { return RedirectToAction(nameof(HomeController.Index), "Home"); }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                { 
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                ApplicationUser user = _userManager.FindByNameAsync(model.Username).Result;
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    // return RedirectToAction(nameof(Lockout));
                    return Content("User locked out");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        
        //================================================ 
        [Authorize]
        public IActionResult AddCharacter()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> ChangePassword(AccountProfileViewModel model)
        {
            ApplicationUser user = _userManager.FindByIdAsync(model.User.Id).Result;
            var result = await _userManager.ChangePasswordAsync(user, model.ChangePasswordModel.OldPassword, model.ChangePasswordModel.Password);
            if(result.Succeeded)
            {
                return RedirectToAction("Success");
            }
            return RedirectToAction("ProfileError");
        }
        [Authorize]
        public IActionResult MyCharacterList()
        {
            MyCharactersViewModel viewModel = new MyCharactersViewModel()
            {
                MyCharactersList = characterService.GetUserCharacters(
                    _userManager.GetUserAsync(HttpContext.User).Result.Id
                    )
            };
            return View(viewModel);
        }
        
        [Authorize]
        [Route("Account/GetPartial/{opt}/{id}")]
        public IActionResult GetPartial(string opt, string id)
        {
            AccountProfileViewModel viewModel = new AccountProfileViewModel()
            {
                User = _userManager.FindByIdAsync(id).Result
            }; 
            return PartialView("../../Views/Account/" + opt, viewModel);
        }

        [Authorize]
        public IActionResult Profile()
        {
            AccountProfileViewModel viewModel = new AccountProfileViewModel()
            {
                User = _userManager.GetUserAsync(HttpContext.User).Result
            };

            return View(viewModel);
        }

        public IActionResult ProfileError()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public async Task<IActionResult> Verify(GameCharacterDTO model)
        {
            var isTokenVerificationValid = characterService.VerifyToken(model);
            if(isTokenVerificationValid)
            {
                model.ApplicationUser = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
                await characterService.AddGameCharacterToDbAsync(model);
                return RedirectToAction("MyCharacterList", "Account");
            }
            return Content("Error");
        }

        [Authorize]
        public IActionResult VerifyCharacter(GameCharacterDTO model)
        {
            var character = characterService.GetCharacterDetailsIfExists(model.CharacterName);
            if (character.characters.error == null)
            {

                if (characterService.CheckIfCharacterIsAlreadyOwned(model.CharacterName))
                {
                    TempData["ErrorMessage"] = "This character was already assigned";
                    return RedirectToAction("AddCharacter");
                }

                model = characterService.MapDetailsFromApi(character, model);
                model.VerificationToken = Guid.NewGuid().ToString();
                return View(model);
            }

            TempData["ErrorMessage"] = "This character does not exist";
            return RedirectToAction("AddCharacter");
        }

        [Route("Account/VerifyCheck/{characterName}/{verificationToken}")]
        public IActionResult VerifyCheck(string characterName, string verificationToken)
        {
            GameCharacterDTO model = new GameCharacterDTO()
            {
                VerificationToken = verificationToken,
                CharacterName = characterName.Replace('_',' ')
            };
            if (characterService.VerifyToken(model))
                return Content("Token correct! Click verify to add this character to your account.");
            return Content("Check failed. Maybe your character info didn't yet update or you pasted wrong code. If pasted code is correct try again within 5 minutes. Last check: " + DateTime.Now.ToShortTimeString());
        }
    }
}