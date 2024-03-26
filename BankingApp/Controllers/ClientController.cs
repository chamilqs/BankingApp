using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Infrastructure.Identity.Entities;
using BankingApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BankingApp.Core.Application.Dtos.Account;
using BankingApp.Core.Application.Interfaces.Services;

namespace BankingApp.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IClientService _clientService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationResponse authViewModel;

        public ClientController(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IClientService clientService, IUserService userService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            authViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _clientService = clientService;
            _userService = userService;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard(string? userId = null, bool hasError = false, string? message = null)
        {
            try
            {
                GenericResponse response = new()
                {
                    HasError = hasError,
                };

                if (hasError)
                    response.Error = message;
                ViewBag.Response = response;

                return View(await _clientService.GetAllProducts(userId));
            }
            catch (Exception ex)
            {
                var user = await _userService.GetById(userId);

                return RedirectToRoute(new { controller = "Client", action = "Dashboard", userId = user.Id, hasError = true, message = $"An error has occured trying to get the products of the user: {user.Username}" });
            }
        }

    }
}
