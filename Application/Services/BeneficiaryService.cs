﻿using AutoMapper;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.Beneficiary;
using BankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BankingApp.Core.Application.Services
{
    public class BeneficiaryService : GenericService<SaveBeneficiaryViewModel, BeneficiaryViewModel, Beneficiary>, IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly IClientService _clientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        private readonly IMapper _mapper;
        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IHttpContextAccessor httpContextAccessor, IClientService clientService,IMapper mapper) : base(beneficiaryRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _beneficiaryRepository = beneficiaryRepository;
            _clientService = clientService;
            _mapper = mapper;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<SaveBeneficiaryViewModel> AddBeneficiary(SaveBeneficiaryViewModel vm, string AccountNumber)
        {
            var userId = user.Id;
            var client = await _clientService.GetByUserIdViewModel(userId);

            vm.ClientId = client.Id;
            vm.AccountNumber = AccountNumber;

            return await base.Add(vm);

        }

        public async Task<SavingsAccount> GetByAccountNumber(string accountNumber)
        {
            var sa = await _beneficiaryRepository.GetByAccountNumber(accountNumber);
            if(sa == null)
            {
                return null;
            }

            return sa;
        }

        public async Task DeleteBeneficiary(string accountNumber)
        {
            var beneficiary = await _beneficiaryRepository.GetBeneficiary(accountNumber);
            await base.Delete(beneficiary.Id);
        }

        public async Task<Beneficiary> GetBeneficiary(string accountNumber)
        {
            var beneficiary = await _beneficiaryRepository.GetBeneficiary(accountNumber);
            if (beneficiary == null)
            {
                return null;
            }

            return beneficiary;
        }
    }
}
