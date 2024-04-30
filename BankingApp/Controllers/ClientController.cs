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

        private readonly AuthenticationResponse _authViewModel;

        public ClientController(IHttpContextAccessor httpContextAccessor, IClientService clientService, IUserService userService)
        {

            _httpContextAccessor = httpContextAccessor;
            _authViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _clientService = clientService;
            _userService = userService;

        }

        
        public async Task<IActionResult> Dashboard()
        {

            return View(await _clientService.GetAllProducts(_authViewModel.Id));

        }

    }
}
