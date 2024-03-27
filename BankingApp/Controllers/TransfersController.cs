using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Core.Application.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;

namespace BankingApp.Controllers
{
	[Authorize(Roles = "Client")]
	public class TransfersController : Controller
    {
        private readonly ITransfersService _transfersService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly IClientService _clientService;
        private readonly AuthenticationResponse user;

        public TransfersController(ITransfersService transfersService, IHttpContextAccessor httpContextAccessor, 
            ISavingsAccountService savingsAccountService, IClientService clientService, ICreditCardService creditCardService)
        { 
            _transfersService = transfersService;
            _savingsAccountService = savingsAccountService;
            _clientService = clientService;
            _creditCardService = creditCardService;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        
        }
        public async Task<IActionResult> TransferBetweenAccounts(string? errorMessage = null)
        {

            ViewBag.Error = "";
            if (errorMessage != null)
            {
                ViewBag.Error = errorMessage;
            }

            var client = await _clientService.GetByUserIdViewModel(user.Id);

            var accounts = await _savingsAccountService.GetAllViewModel();
            accounts = accounts.Where(x => x.ClientId == client.Id).ToList();

            ViewBag.ClientAccounts = accounts;  
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TransferBetweenAccounts(SaveTransactionViewModel vm)
        {
            try
            {
                var transfer = await _transfersService.Transfer(vm, Core.Application.Enums.TransactionType.AccountTransfer,false);
                if(transfer == null)
                {
                    return RedirectToRoute(new { controller = "Transfers", action = "TransferBetweenAccounts", errorMessage = "There was an error during the transfer." });
                }                

            }
            catch (Exception ex)
            {

                return RedirectToRoute(new { controller = "Transfers", action = "TransferBetweenAccounts", errorMessage = ex.Message.ToString() });

            }

            return RedirectToRoute(new { controller = "Transfers", action = "TransferBetweenAccounts" });
        }
    
        public async Task<IActionResult> CashAdvance(string? errorMessage = null)
        {
            ViewBag.Error = "";
            if (errorMessage != null)
            {
                ViewBag.Error = errorMessage;
            }

            var client = await _clientService.GetByUserIdViewModel(user.Id);

            var creditCards = await _creditCardService.GetAllViewModel();
            creditCards = creditCards.Where(x => x.ClientId == client.Id).ToList();

            var accounts = await _savingsAccountService.GetAllViewModel();
            accounts = accounts.Where(x => x.ClientId == client.Id).ToList();

            ViewBag.ClientAccounts = accounts;
            ViewBag.ClientCreditCards = creditCards;  

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CashAdvance(SaveTransactionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CashAdvance");
            }
            try
            {
                var transfer = await _transfersService.CashAdvance(vm);
                if (!transfer)
                {
                    return RedirectToRoute(new { controller = "Transfers", action = "CashAdvance", errorMessage = "There was an error with the transaction" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToRoute(new { controller = "Transfers", action = "CashAdvance", errorMessage = ex.Message.ToString() });
            }

            return RedirectToRoute(new { controller = "Transfers", action = "CashAdvance" });
        }
    
    }
}
