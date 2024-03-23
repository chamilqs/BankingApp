using AutoMapper;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.Transaction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public TransfersService(ITransactionService transactionService, IMapper mapper, IClientService clientService,
        IHttpContextAccessor httpContextAccessor, ICreditCardService creditCardService) 
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _clientService = clientService;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _creditCardService = creditCardService;
        }
        public async Task<SaveTransactionViewModel> Transfer(SaveTransactionViewModel vm, Enums.TransactionType transactionType, bool isCashAdvance)
        {
            vm.TransactionTypeId = (int)transactionType;

            if (isCashAdvance)
            {
                await AddMoneyToAccountCreditCard(vm.Origin, vm.Destination, vm.Amount);
            }
            else
            {
                await AddMoneyToAccount(vm.Destination, vm.Origin, vm.Amount);
            }

            return await _transactionService.Add(vm);
        }

        public async Task<bool> AddMoneyToAccount(string accountNumberOrigin, string accountNumberDestination, double amount)
        {
            // Get products where the ClientId is the same as the logged user ClientId
            var client = await _clientService.GetByUserIdViewModel(user.Id);

            // Destination account
            var destinyAccount = await _savingsAccountService.GetByAccountNumber(accountNumberDestination, client.Id);

            // Origin account
            var originAccount = await _savingsAccountService.GetByAccountNumber(accountNumberOrigin, client.Id);

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
                return false;
            }

        }

        public async Task<bool> AddMoneyToAccountCreditCard(string accountNumberOrigin, string accountNumberDestination, double amount)
        {
            // Get products where the ClientId is the same as the logged user ClientId
            var client = await _clientService.GetByUserIdViewModel(user.Id);

            // Destination account
            var destinyAccount = await _savingsAccountService.GetByAccountNumber(accountNumberDestination, client.Id);

            // Origin account
            var originAccount = await _creditCardService.GetByAccountNumber(accountNumberOrigin, client.Id);

            if (destinyAccount != null && originAccount != null)
            {
                double destinyBalance = destinyAccount.Balance + amount;

                await _savingsAccountService.UpdateSavingsAccount(destinyBalance, client.Id, destinyAccount.Id);
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<string> CashAdvance(SaveTransactionViewModel vm)
        {
            // Get credit card and verify if the limit is enough to make the cash advance
            var client = await _clientService.GetByUserIdViewModel(user.Id);
            var creditCard = await _creditCardService.GetByAccountNumber(vm.Origin, client.Id);
            if (creditCard == null)
            {
                return "This credit card doesn't exist.";
            }

            if (vm.Amount > creditCard.Limit || vm.Amount > creditCard.Balance)
            {
                return "You can't do this transaction, the amount is higher than the limit or the balance.";
            }

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
                await Transfer(vm, Enums.TransactionType.CashAdvance, true);

                return "Cash advance done successfully.";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return "Cash advance done successfully.";
        }
    }
}
