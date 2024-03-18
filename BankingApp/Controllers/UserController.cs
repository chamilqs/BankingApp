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

        #region Login
        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> Index(bool hasError = false, string? message = null)
        {
            var login = new LoginViewModel();

            if (hasError)
            {
                login.HasError = hasError;
                login.Error = message;
            }

            return View(login);
        }

        public async Task<IActionResult> RedirectIndex(string? ReturnUrl)
        {
            return RedirectToRoute(new { controller = "User", action = "Index", hasError = true, message = "You don't have access to this section!" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }
        #endregion

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

