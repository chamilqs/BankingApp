﻿using AutoMapper;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.Loan;
using BankingApp.Core.Application.ViewModels.Transaction;
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

        public LoanService(ILoanRepository loanRepository, IMapper mapper) : base(loanRepository, mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        #region Delete
        public async Task DeleteProduct(string id)
        {
            var loan = await GetByAccountNumber(id);

            await _loanRepository.DeleteAsync(loan);
        }
        #endregion

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
            var loanList = await _loanRepository.GetAllAsync();
            return loanList.Where(l => l.ClientId == clientId).Select(l => new LoanViewModel
            {
                Id = l.Id,
                ClientId = l.ClientId,
                Amount = l.Amount,
                Balance = l.Balance,
                DateCreated = l.DateCreated



            }).ToList();
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
