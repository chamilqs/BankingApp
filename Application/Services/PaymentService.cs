using AutoMapper;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.Payment;
using BankingApp.Core.Application.ViewModels.Transaction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.Application.Services
{
    public class PaymentService // : IPaymentService
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly ILoanService _loanService;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        private readonly IMapper _mapper;

        public PaymentService(IHttpContextAccessor httpContextAccessor, IMapper mapper, ITransactionService transactionService)
        {
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
            _transactionService = transactionService;
        }
        /* 
         public async Task<PaymentViewModel> ExpressPayment(PaymentViewModel vm)
         {
             var destination = await _savingsAccountService.GetByIdSaveViewModel(vm.Destination);

             if (destination == null)
             {
                 throw new Exception("Account Number is not valid or does not exist");
             }

             var amount = vm.Amount;

             if (amount <= 0)
             {
                 throw new Exception("Amount is not valid");
             }

             var origin = await _savingsAccountService.GetByIdSaveViewModel(vm.Origin);

             if (origin == null)
             {
                 throw new Exception("Account does not exist");
             }
             if (origin.Balance < amount)
             {
                 throw new Exception("Insufficient balance ");
             }


             origin.Balance -= amount;

             destination.Balance += amount;

             var send = await _savingsAccountService.Update(origin, origin.Id);
             var recieve = await _savingsAccountService.Update(destination, destination.Id);

             if (send == null || recieve == null)
             {
                 throw new Exception("There was an error during the transaction");
             }

             var paymentRecord = new SaveTransactionViewModel
             {
                 Origin = origin.Id,
                 Destination = destination.Id,
                 Amount = amount,
                 TransactionTypeId = 3

             };

             await _transactionService.Add(paymentRecord);

             return vm;
         }

         public async Task<PaymentViewModel> CreditCardPayment(PaymentViewModel vm)
         {
             var destination = await _creditCardService.GetByIdSaveViewModel(vm.Destination);

             if (destination == null)
             {
                 throw new Exception("Credit Card Number is not valid or does not exist");
             }

             var amount = vm.Amount;

             if (amount <= 0)
             {
                 throw new Exception("Amount is not valid");
             }

             var origin = await _savingsAccountService.GetByIdSaveViewModel(vm.Origin);

             if (origin == null)
             {
                 throw new Exception("Account does not exist");
             }
             if (origin.Balance < amount)
             {
                 throw new Exception("Insufficient balance ");
             }

             origin.Balance -= amount;
             destination.Debt -= amount;

             if (amount > destination.Debt)
             {
                 origin.Balance += amount - destination.Debt;
             }


             var send = await _savingsAccountService.Update(origin, origin.Id);
             var recieve = await _creditCardService.Update(destination, destination.Id);

             if (send == null || recieve == null)
             {
                 throw new Exception("There was an error during the transaction");
             }

             var paymentRecord = new SaveTransactionViewModel
             {
                 Origin = origin.Id,
                 Destination = destination.Id,
                 Amount = amount,
                 TransactionTypeId = 4

             };

             await _transactionService.Add(paymentRecord);

             return vm;
         }

         public async Task<PaymentViewModel> LoanPayment(PaymentViewModel vm)
         {
             var destination = await _loanService.GetByIdSaveViewModel(vm.Destination);

             if (destination == null)
             {
                 throw new Exception("Loan is not valid or does not exist");
             }

             var amount = vm.Amount;

             if (amount <= 0)
             {
                 throw new Exception("Amount is not valid");
             }

             var origin = await _savingsAccountService.GetByIdSaveViewModel(vm.Origin);

             if (origin == null)
             {
                 throw new Exception("Account does not exist");
             }
             if (origin.Balance < amount)
             {
                 throw new Exception("Insufficient balance ");
             }

             origin.Balance -= amount;
             destination.Debt -= amount;

             if (amount > destination.Debt)
             {
                 origin.Balance += amount - destination.Debt;
             }


             var send = await _savingsAccountService.Update(origin, origin.Id);
             var recieve = await _loanService.Update(destination, destination.Id);

             if (send == null || recieve == null)
             {
                 throw new Exception("There was an error during the transaction");
             }

             var paymentRecord = new SaveTransactionViewModel
             {
                 Origin = origin.Id,
                 Destination = destination.Id,
                 Amount = amount,
                 TransactionTypeId = 2

             };

             await _transactionService.Add(paymentRecord);

             return vm;
         }

         public async Task<PaymentViewModel> BeneficiaryPayment(PaymentViewModel vm)
         {
             var destination = await _savingsAccountService.GetByIdSaveViewModel(vm.Destination);

             if (destination == null)
             {
                 throw new Exception("Beneficiary is not valid or does not exist");
             }

             var amount = vm.Amount;

             if (amount <= 0)
             {
                 throw new Exception("Amount is not valid");
             }

             var origin = await _savingsAccountService.GetByIdSaveViewModel(vm.Origin);

             if (origin == null)
             {
                 throw new Exception("Account does not exist");
             }
             if (origin.Balance < amount)
             {
                 throw new Exception("Insufficient balance ");
             }


             origin.Balance -= amount;

             destination.Balance += amount;

             var send = await _savingsAccountService.Update(origin, origin.Id);
             var recieve = await _savingsAccountService.Update(destination, destination.Id);

             if (send == null || recieve == null)
             {
                 throw new Exception("There was an error during the transaction");
             }

             var paymentRecord = new SaveTransactionViewModel
             {
                 Origin = origin.Id,
                 Destination = destination.Id,
                 Amount = amount,
                 TransactionTypeId = 6

             };

             await _transactionService.Add(paymentRecord);

             return vm;
         }
        */

    }
}
