using Azure;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.SavingsAccount;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    public class SavingsAccountController : Controller
    {
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly IProductService _productService;
        private readonly IClientService _clientService;

        public SavingsAccountController(ISavingsAccountService savingsAccountService, IProductService productService, IClientService clientService)
        {
            _savingsAccountService = savingsAccountService;
            _productService = productService;
            _clientService = clientService;
        }

        #region Create
        [HttpPost]
        public async Task<IActionResult> Create(SaveSavingsAccountViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index", hasError = true, message = "And error has ocurred trying to create a savings account: Not all the fields were correct." });
            }

            vm.Id = await _productService.GenerateProductNumber();
            await _savingsAccountService.Add(vm);

            var client = await _clientService.GetByIdSaveViewModel(vm.ClientId);

            return RedirectToRoute(new { controller = "Admin", action = "IndexProducts", userId = client.UserId });
        }
        #endregion

        #region Create
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            return RedirectToRoute(new { controller = "Admin", action = "IndexProducts" });
        }
        #endregion
    }
}
