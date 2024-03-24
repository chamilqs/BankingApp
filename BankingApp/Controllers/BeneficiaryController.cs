using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.ViewModels.Beneficiary;

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
            var vms = await _beneficiaryService.GetAllViewModel();

            // Where the id of the client is not equal to his own id client
            var userId = user.Id;
            var client = await _clientService.GetByUserIdViewModel(userId);

            // Here goes the method to get the user's account number
            vms.Where(f => f.ClientId != client.Id);

            return View(vms);
        }

        [HttpPost]
        public async Task<IActionResult> AddBeneficiaryBySearch(string accountNumber)
        {
            // Check that the account number is not the same as the user's account number

            // Here goes the method to get the user's account number
            /*if( == accountNumber)
            {
                ModelState.AddModelError("AccountNumber", "You cannot add yourself as a beneficiary.");
                return View("Index", await _beneficiaryService.GetAllViewModel());
            }*/

            var beneficiary = await _beneficiaryService.GetByAccountNumber(accountNumber);          
            if (beneficiary == null)
            {
                ModelState.AddModelError("AccountNumber", "Account not found.");
                return View("Index", await _beneficiaryService.GetAllViewModel());
            }

            var client = await _clientService.GetByUserIdViewModel(user.Id);

            var beneficiaries = await _beneficiaryService.GetAllViewModel();
            if (beneficiaries.Any(f => (f.ClientId == client.Id && f.BeneficiaryAccountNumber == accountNumber)))
            {
                ModelState.AddModelError("AccountNumber", "This person is already a beneficiary.");
                return View("Index", await _beneficiaryService.GetAllViewModel());
            }

            SaveBeneficiaryViewModel vm = new();
            await _beneficiaryService.AddBeneficiary(vm, accountNumber);
            return View("Index", await _beneficiaryService.GetAllViewModel());
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
