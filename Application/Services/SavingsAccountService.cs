﻿using AutoMapper;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.Loan;
using BankingApp.Core.Application.ViewModels.SavingsAccount;
using BankingApp.Core.Application.ViewModels.Transaction;
using BankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BankingApp.Core.Application.Services
{
    public class SavingsAccountService : GenericService<SaveSavingsAccountViewModel, SavingsAccountViewModel, SavingsAccount>, ISavingsAccountService
    {
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;

        public SavingsAccountService(ISavingsAccountRepository savingsAccountRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper,
            IAccountService accountService) : base(savingsAccountRepository, mapper)
        {
            _savingsAccountRepository = savingsAccountRepository;
            _mapper = mapper;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        #region Delete
        public async Task Delete(string id)
        {
            var savingsAccount = await GetByAccountNumber(id);

            await _savingsAccountRepository.DeleteAsync(savingsAccount);
        }
        #endregion

        public async Task<List<SavingsAccountViewModel>> GetAllByClientId(int clientId)
        {
            var sas = await _savingsAccountRepository.GetAllAsync();

            var saViewModels = new List<SavingsAccountViewModel>();

            foreach (var sa in sas.Where(b => b.ClientId == clientId))
            {

                var saViewModel = new SavingsAccountViewModel
                {
                    ClientId = sa.ClientId,
                    Id = sa.Id,
                    Balance = sa.Balance,
                    DateCreated = sa.DateCreated,
                    IsMainAccount = sa.IsMainAccount



                };

                saViewModels.Add(saViewModel);
            }

            return saViewModels;
        }

        public async Task<SavingsAccount> GetByAccountNumber(string accountNumber)
        {
            var savingsAccount = await _savingsAccountRepository.GetByAccountNumber(accountNumber);
            if (savingsAccount == null)
            {
                return null;
            }

            return savingsAccount;
        }

        public async Task<SavingsAccount> GetByAccountNumberLoggedUser(string accountNumber, int ClientId)
        {
            var savingsAccount = await _savingsAccountRepository.GetByAccountNumberLoggedUser(accountNumber, ClientId);
            if (savingsAccount == null)
            {
                return null;
            }

            return savingsAccount;
        }

        public async Task UpdateSavingsAccount(double balance, int clientId, string id)
        {
            var savingsAccount = await GetByAccountNumberLoggedUser(id, clientId);

            SaveSavingsAccountViewModel vm = new SaveSavingsAccountViewModel
            {
                Id = savingsAccount.Id,
                ClientId = clientId,
                DateCreated = savingsAccount.DateCreated,
                Balance = balance,
                IsMainAccount = savingsAccount.IsMainAccount
            };

            await base.UpdateProduct(vm, id);
        }

        #region GetClientMainAccount
        public async Task<SavingsAccountViewModel> GetClientMainAccount(int clientId)
        {
            var savingsAccountList = await base.GetAllViewModel();

            var savingsAccount = savingsAccountList.FirstOrDefault(sa => sa.ClientId == clientId && sa.IsMainAccount == true);

            return savingsAccount;
        }
        #endregion
    }
}
