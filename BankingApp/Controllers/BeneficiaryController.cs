using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.ViewModels.Beneficiary;
using BankingApp.Core.Domain.Entities;
using static BankingApp.Controllers.AdminController;

namespace BankingApp.Controllers
{
    public class BeneficiaryController : Controller
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly IClientService _clientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        public BeneficiaryController(IBeneficiaryService beneficiaryService, IHttpContextAccessor httpContextAccessor, IClientService clientService,IBeneficiaryRepository beneficiaryRepository)
        {
            _beneficiaryService = beneficiaryService;
            _httpContextAccessor = httpContextAccessor;
            _beneficiaryRepository = beneficiaryRepository;
            _clientService = clientService;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<IActionResult> Index()
        {
            return View(await _beneficiaryService.GetAllViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddBeneficiaryBySearch(string accountNumber)
        {
            var beneficiary = await _beneficiaryService.GetByAccountNumber(accountNumber);          
            if (beneficiary == null)
            {
                return BadRequest(new { message = "Account not found." });
            }

            var client = await _clientService.GetByUserIdViewModel(user.Id);

            var beneficiaries = await _beneficiaryService.GetAllViewModel();
            if (beneficiaries.Any(f => (f.ClientId == client.Id && f.BeneficiaryAccountNumber == accountNumber)))
            {
                return BadRequest(new { message = "This person is already a beneficiary." });
            }

            SaveBeneficiaryViewModel vm = new();
            await _beneficiaryService.AddBeneficiary(vm, accountNumber);
            return Ok(new { message = "Beneficiary added successfully." });
        }

        public async Task<IActionResult> DeleteBeneficiary(string accountNumber)
        {
            return View(await _beneficiaryService.GetByAccountNumber(accountNumber));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBeneficiaryPost(string SavingsAccountId)
        {
            await _beneficiaryService.DeleteBeneficiary(SavingsAccountId);
            return RedirectToRoute(new { controller = "Beneficiary", action = "Index" });

        }

    }
}
