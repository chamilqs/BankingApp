using AutoMapper;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.CreditCard;
using BankingApp.Core.Application.ViewModels.Loan;
using BankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BankingApp.Core.Application.Services
{
    public class LoanService : GenericService<SaveLoanViewModel, LoanViewModel, Loan>, ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepository loanRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper
            ) : base(loanRepository, mapper)
        {
            _loanRepository = loanRepository;
            _httpContextAccessor = httpContextAccessor;

            _mapper = mapper;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }
        public async Task<Loan> GetByAccountNumber(string accountNumber)
        {
            var loan = await _loanRepository.GetByAccountNumber(accountNumber);
            if (loan == null)
            {
                return null;
            }

            return loan;
        }
        public async Task<List<LoanViewModel>> GetAllByClientId(int clientId)
        {
            var loans = await _loanRepository.GetAllAsync();

            var loanViewModels = new List<LoanViewModel>();

            foreach (var loan in loans.Where(b => b.ClientId == clientId))
            {


                var loanViewModel = new LoanViewModel
                {
                    ClientId = loan.ClientId,
                    Id = loan.Id,
                    Balance = loan.Balance,
                    DateCreated = loan.DateCreated,
                    Amount = loan.Amount

                };

                loanViewModels.Add(loanViewModel);
            }

            return loanViewModels;
        }
        public async Task UpdateLoan(double balance, double amount, string accountNumber, int clientId)
        {
            var loan = await GetByAccountNumberLoggedUser(accountNumber, clientId);

            SaveLoanViewModel vm = new SaveLoanViewModel
            {
                Id = loan.Id,
                ClientId = loan.ClientId,
                DateCreated = loan.DateCreated,
                Balance = balance,
                Amount = amount,
            };

            await base.UpdateProduct(vm, accountNumber);
        }

        public async Task<Loan> GetByAccountNumberLoggedUser(string accountNumber, int ClientId)
        {
            var loan = await _loanRepository.GetByAccountNumberLoggedUser(accountNumber, ClientId);
            if (loan == null)
            {
                return null;
            }

            return loan;
        }

    }
}
