﻿using BankingApp.Core.Application.Enums;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.SavingsAccount;
using BankingApp.Core.Application.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BankingApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SavingsAccountController : Controller
    {
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly IProductService _productService;
        private readonly IClientService _clientService;
        private readonly ITransfersService _transfersService;

        public SavingsAccountController(ISavingsAccountService savingsAccountService, IProductService productService, IClientService clientService, ITransfersService transfersService)
        {
            _savingsAccountService = savingsAccountService;
            _productService = productService;
            _clientService = clientService;
            _transfersService = transfersService;
        }

        #region Create
        [HttpPost]
        public async Task<IActionResult> Create(SaveSavingsAccountViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "SavingsAccount", action = "Create", hasError = true, message = "And error has ocurred trying to create a savings account: Not all the fields were correct." });
            }

            vm.Id = await _productService.GenerateProductNumber();
            await _savingsAccountService.Add(vm);

            var client = await _clientService.GetByIdSaveViewModel(vm.ClientId);

            return RedirectToRoute(new { controller = "Admin", action = "IndexProducts", userId = client.UserId });
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var savingsAccount = await _savingsAccountService.GetByAccountNumber(id);

            //Transfer
            var mainAccount = await _savingsAccountService.GetClientMainAccount(savingsAccount.ClientId);
            SaveTransactionViewModel saveTransaction = new()
            {
                Origin = savingsAccount.Id,
                Destination = mainAccount.Id,
                TransactionTypeId = (int)TransactionType.AccountTransfer,
                Amount = savingsAccount.Balance,
                Concept = $"Delete the savings account: {savingsAccount.Id}"
            };

            await _transfersService.Transfer(saveTransaction, TransactionType.AccountTransfer, false);

            await _savingsAccountService.DeleteProduct(id);

            return RedirectToRoute(new { controller = "Admin", action = "IndexProducts" });
        }
        #endregion
    }
}
