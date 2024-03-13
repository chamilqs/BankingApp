using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using BankingApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankingApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse authViewModel;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            authViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }

        public IActionResult Index()
        {
            var isAdmin = authViewModel != null ? authViewModel.Roles.Any(r => r == "Admin") : false;
            var isClient = authViewModel != null ? authViewModel.Roles.Any(r => r == "Client") : false;

            if (isAdmin)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else if (isClient)
            {
                return RedirectToAction("Dashboard", "Client");
            }

            return RedirectToAction("Login", "User");
        }

    }
}
