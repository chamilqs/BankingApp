﻿using AutoMapper;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.Transaction;
using Microsoft.AspNetCore.Http;
using System.Transactions;

namespace BankingApp.Core.Application.Services
{
    public class TransfersService : ITransfersService
    {
        private readonly ITransactionService _transactionService;
        private readonly IClientService _clientService;
        private readonly ICreditCardService _creditCardService;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        private readonly IMapper _mapper;

        public TransfersService(ITransactionService transactionService, ISavingsAccountService savingsAccountService, IMapper mapper, IClientService clientService,
        IHttpContextAccessor httpContextAccessor, ICreditCardService creditCardService) 
        {
            _transactionService = transactionService;
            _savingsAccountService = savingsAccountService;
            _mapper = mapper;
            _clientService = clientService;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _creditCardService = creditCardService;
        }

        public async Task<SaveTransactionViewModel> Transfer(SaveTransactionViewModel vm, Enums.TransactionType transactionType, bool isCashAdvance)
        {
            vm.TransactionTypeId = (int)transactionType;
            vm.DateCreated = DateTime.Now;

            if (isCashAdvance)
            {
                var makeAdvance = await AddMoneyToAccountCreditCard(vm.Origin, vm.Destination, vm.Amount);
                if (!makeAdvance)
                {
                    return null;
                }

            }
            else
            {
                var addMoney = await AddMoneyToAccount(vm.Origin, vm.Destination, vm.Amount);
                if(!addMoney)
                {
                    return null;
                }

            }

            if(vm.Concept == null || vm.Concept.Length == 0)
            {
                vm.Concept = "Transfer";                
            }

            return await _transactionService.Add(vm); 
        }

        public async Task<bool> AddMoneyToAccount(string accountNumberOrigin, string accountNumberDestination, double amount)
        {
            // Get products where the ClientId is the same as the logged user ClientId
            var client = await _clientService.GetByUserIdViewModel(user.Id);

            var destinyAccount = await _savingsAccountService.GetByAccountNumberLoggedUser(accountNumberDestination, client.Id);
            var originAccount = await _savingsAccountService.GetByAccountNumberLoggedUser(accountNumberOrigin, client.Id);

            if(destinyAccount != null && originAccount != null)
            {
                double destinyBalance = destinyAccount.Balance + amount;
                double originBalance = originAccount.Balance - amount;

                await _savingsAccountService.UpdateSavingsAccount(destinyBalance, client.Id, destinyAccount.Id);
                await _savingsAccountService.UpdateSavingsAccount(originBalance, client.Id, originAccount.Id);
                return true;
            }
            else
            {
                throw new Exception("Operation failed.");
            }

        }

        public async Task<bool> AddMoneyToAccountCreditCard(string accountNumberOrigin, string accountNumberDestination, double amount)
        {
            // Get products where the ClientId is the same as the logged user ClientId
            var client = await _clientService.GetByUserIdViewModel(user.Id);

            var destinyAccount = await _savingsAccountService.GetByAccountNumberLoggedUser(accountNumberDestination, client.Id);
            var originAccount = await _creditCardService.GetByAccountNumberLoggedUser(accountNumberOrigin, client.Id);

            if (destinyAccount != null && originAccount != null)
            {
                double destinyBalance = destinyAccount.Balance + amount;

                await _savingsAccountService.UpdateSavingsAccount(destinyBalance, client.Id, destinyAccount.Id);
                return true;
            }
            else
            {
                throw new Exception("Operation failed."); 
            }

        }

        public async Task<bool> CashAdvance(SaveTransactionViewModel vm)
        {
            // Get credit card and verify if the limit is enough to make the cash advance
            var client = await _clientService.GetByUserIdViewModel(user.Id);
            var creditCard = await _creditCardService.GetByAccountNumberLoggedUser(vm.Origin, client.Id);
            if (creditCard == null)
            {
                throw new Exception("This credit card doesn't exist.");
            }

            if (vm.Amount > creditCard.Limit || vm.Amount > creditCard.Balance)
            {
                throw new Exception("You can't do this transaction, the amount is higher than the limit or the balance.");
            }

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    double interest = 0.0625;
                    double debt = vm.Amount + (vm.Amount * interest);
                    double balance = creditCard.Balance - vm.Amount;

                    await _creditCardService.UpdateCreditCard(balance, debt, creditCard.Id, client.Id);
                    vm.TransactionTypeId = (int)Enums.TransactionType.CashAdvance;

                    if (vm.Concept == null || vm.Concept.Length == 0)
                    {
                        vm.Concept = "Cash advance";
                    }

                    var transferResult = await Transfer(vm, Enums.TransactionType.CashAdvance, true);

                    if (transferResult != null)
                    {
                        transactionScope.Complete();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Cash advance failed.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception("An error occurred during cash advance.");
                }
            }
        }

        //try
        //{
        //    double interest = 0.0625;
        //    double debt = vm.Amount + (vm.Amount * interest);
        //    double balance = creditCard.Balance - vm.Amount;

        //    await _creditCardService.UpdateCreditCard(balance, debt, creditCard.Id, client.Id);
        //    vm.TransactionTypeId = (int)Enums.TransactionType.CashAdvance;

        //    if (vm.Concept == null || vm.Concept.Length == 0)
        //    {
        //        vm.Concept = "Cash advance";
        //    }
        //    await Transfer(vm, Enums.TransactionType.CashAdvance, true);

        //    return "Cash advance done successfully.";

        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex.Message);
        //}

        //return "Cash advance done successfully.";
    }
}

