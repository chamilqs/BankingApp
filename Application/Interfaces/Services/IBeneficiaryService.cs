﻿using BankingApp.Core.Application.ViewModels.Beneficiary;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface IBeneficiaryService : IGenericService<SaveBeneficiaryViewModel, BeneficiaryViewModel, Beneficiary>
    {
        Task<SaveBeneficiaryViewModel> AddBeneficiary(SaveBeneficiaryViewModel vm, string AccountNumber);
        Task<Beneficiary> GetByAccountNumber(string accountNumber);
        Task DeleteBeneficiary(string AccountNumber);
    }
}