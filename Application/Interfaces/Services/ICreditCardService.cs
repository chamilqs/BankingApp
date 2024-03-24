﻿using BankingApp.Core.Application.ViewModels.CreditCard;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface ICreditCardService : IGenericService<SaveCreditCardViewModel, CreditCardViewModel, CreditCard>
    {
        Task<CreditCard> GetByAccountNumber(string accountNumber, int clientId);
        Task UpdateCreditCard(double balance, double debt, string accountNumber, int clientId);
    }
}