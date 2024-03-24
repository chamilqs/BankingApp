using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        private readonly PaymentService paymentService;

        public PaymentController(IHttpContextAccessor httpContextAccessor, AuthenticationResponse user)
        {
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
