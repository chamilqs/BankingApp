using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Core.Application.ViewModels.Transaction;

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
        public IActionResult TransferBetweenAccounts()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TransferBetweenAccounts(SaveTransactionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("TransferBetweenAccounts", vm);
            }
            
            var transfer = await _transfersService.Transfer(vm, Core.Application.Enums.TransactionType.AccountTransfer,false);
            if(transfer == null)
            {
                ModelState.AddModelError("Amount", "There was an error with the transaction");
                return View("TransferBetweenAccounts", vm);
            }

            return RedirectToRoute(new { controller = "Transfers", action = "TransferBetweenAccounts" });
        }
    }
}
