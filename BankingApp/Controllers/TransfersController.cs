using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    public class TransfersController : Controller
    {
        private readonly ITransfersService _transfersService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        public TransfersController(ITransfersService transfersService, IHttpContextAccessor httpContextAccessor) 
        { 
            _transfersService = transfersService;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
