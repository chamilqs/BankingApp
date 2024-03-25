using Azure;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.SavingsAccount;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    public class SavingsAccountController : Controller
    {
        private readonly ISavingsAccountService _savingsAccountService;

        public SavingsAccountController(ISavingsAccountService savingsAccountService)
        {
            _savingsAccountService = savingsAccountService;
        }

        #region Create
        public async Task<IActionResult> Create(SaveSavingsAccountViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index", hasError = true, message = "And error has ocurred trying to create a savings account: Not all the fields were correct." });
            }

            await _savingsAccountService.Add(vm);

            return RedirectToRoute(new { controller = "Admin", action = "IndexProducts" });
        }
        #endregion
    }
}
