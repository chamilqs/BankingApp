using Microsoft.AspNetCore.Mvc;
using BankingApp.Core.Application.ViewModels.User;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.DTOs.Account;
using WebAdmin.BankingApp.Middlewares;
using Microsoft.IdentityModel.Tokens;
using BankingApp.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebAdmin.BankingApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly AuthenticationResponse authViewModel;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            authViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                if (userVm.Roles.Contains("Admin"))
                {
                    HttpContext.Session.Set("user", userVm);
                }
                else
                {
                    //var client = "await _userService.GetByIdSaveViewModel(userVm.Id)";
                    //HttpContext.Session.Set("client", client);

                    HttpContext.Session.Set("user", userVm);

                }
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Login" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}

