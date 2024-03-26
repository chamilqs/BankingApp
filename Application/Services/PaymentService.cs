using AutoMapper;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.Payment;
using BankingApp.Core.Application.ViewModels.Transaction;
using Microsoft.AspNetCore.Http;

namespace BankingApp.Core.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ITransactionService _transactionService;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly ILoanService _loanService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        private readonly IMapper _mapper;

        public PaymentService(IHttpContextAccessor httpContextAccessor, IMapper mapper, ITransactionService transactionService,
            ISavingsAccountService savingsAccountService, ICreditCardService creditCardService, ILoanService loanService)
        {

            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
            _transactionService = transactionService;
            _savingsAccountService = savingsAccountService;
            _creditCardService = creditCardService;
            _loanService = loanService;

        }

        public async Task<ExpressPaymentViewModel> ExpressPayment(ExpressPaymentViewModel vm)
        {
            var destination = await _savingsAccountService.GetByAccountNumber(vm.Destination);

            if (destination == null)
            {
                throw new Exception("Account Number is not valid or does not exist");
            }

            var amount = vm.Amount;

            if (amount <= 0)
            {
                throw new Exception("Amount is not valid");
            }

            var origin = await _savingsAccountService.GetByAccountNumber(vm.Origin);

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

            await _savingsAccountService.UpdateSavingsAccount(origin.Balance, origin.ClientId, origin.Id);
            await _savingsAccountService.UpdateSavingsAccount(destination.Balance, destination.ClientId, destination.Id);



            var paymentRecord = new SaveTransactionViewModel
            {
                Origin = origin.Id,
                Destination = destination.Id,
                Amount = amount,
                TransactionTypeId = 3,
                Concept = $"{origin.Id} to {destination.Id}"

            };

            await _transactionService.Add(paymentRecord);

            return vm;
        }

        public async Task<CreditCardPaymentViewModel> CreditCardPayment(CreditCardPaymentViewModel vm)
        {
            var destination = await _creditCardService.GetByAccountNumber(vm.Destination);

            if (destination == null)
            {
                throw new Exception("Credit Card Number is not valid or does not exist");
            }

            var amount = vm.Amount;

            if (amount <= 0)
            {
                throw new Exception("Amount is not valid");
            }

            var origin = await _savingsAccountService.GetByAccountNumber(vm.Origin);

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


            await _savingsAccountService.UpdateSavingsAccount(origin.Balance, origin.ClientId, origin.Id);
            await _creditCardService.UpdateCreditCard(destination.Balance, destination.Debt, destination.Id, destination.ClientId);



            var paymentRecord = new SaveTransactionViewModel
            {
                Origin = origin.Id,
                Destination = destination.Id,
                Amount = amount,
                TransactionTypeId = 4,
                Concept = $"{origin.Id} to {destination.Id}"

            };

            await _transactionService.Add(paymentRecord);

            return vm;
        }

        public async Task<LoanPaymentViewModel> LoanPayment(LoanPaymentViewModel vm)
        {

            var destination = await _loanService.GetByAccountNumber(vm.Destination);

            if (destination == null)
            {
                throw new Exception("Loan is not valid or does not exist");
            }

            var amount = vm.Amount;

            if (amount <= 0)
            {
                throw new Exception("Amount is not valid");
            }

            var origin = await _savingsAccountService.GetByAccountNumber(vm.Origin);

            if (origin == null)
            {
                throw new Exception("Account does not exist");
            }
            if (origin.Balance < amount)
            {
                throw new Exception("Insufficient balance ");
            }

            origin.Balance -= amount;
            destination.Balance -= amount;

            if (amount > destination.Balance)
            {
                origin.Balance += amount - destination.Balance;
            }


            await _savingsAccountService.UpdateSavingsAccount(origin.Balance, origin.ClientId, origin.Id);
            await _loanService.UpdateLoan(destination.Balance, destination.Amount, destination.Id, destination.ClientId);


            var paymentRecord = new SaveTransactionViewModel
            {

                Origin = origin.Id,
                Destination = destination.Id,
                Amount = amount,
                TransactionTypeId = 2,
                Concept = $"{origin.Id} to {destination.Id}"
            };

            await _transactionService.Add(paymentRecord);

            return vm;
        }

        public async Task<BeneficiaryPaymentViewModel> BeneficiaryPayment(BeneficiaryPaymentViewModel vm)
        {
            var destination = await _savingsAccountService.GetByAccountNumber(vm.Destination);

            if (destination == null)
            {
                throw new Exception("Beneficiary is not valid or does not exist");
            }

            var amount = vm.Amount;

            if (amount <= 0)
            {
                throw new Exception("Amount is not valid");
            }

            var origin = await _savingsAccountService.GetByAccountNumber(vm.Origin);

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

            await _savingsAccountService.UpdateSavingsAccount(origin.Balance, origin.ClientId, origin.Id);
            await _savingsAccountService.UpdateSavingsAccount(destination.Balance, destination.ClientId, destination.Id);



            var paymentRecord = new SaveTransactionViewModel
            {
                Origin = origin.Id,
                Destination = destination.Id,
                Amount = amount,
                TransactionTypeId = 6,
                Concept = $"{origin.Id} to {destination.Id}"

            };

            await _transactionService.Add(paymentRecord);

            return vm;
        }


    }
}
