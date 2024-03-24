﻿using AutoMapper;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.CreditCard;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Services
{
    public class CreditCardService : GenericService<SaveCreditCardViewModel, CreditCardViewModel, CreditCard>, ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IMapper _mapper;

        public CreditCardService(ICreditCardRepository creditCardRepository, IMapper mapper) : base(creditCardRepository, mapper)
        {
            _creditCardRepository = creditCardRepository;
            _mapper = mapper;
        }


        public async Task<List<CreditCardViewModel>> GetAllByClientId(int clientId)
        {
            var creditCardList = await _creditCardRepository.GetAllAsync();
            return creditCardList.Where(c => c.ClientId == clientId).Select(c => new CreditCardViewModel
            {
                Id = c.Id,
                Balance = c.Balance,
                Debt = c.Debt,
                ClientId = c.ClientId,
                Limit = c.Limit,
                DateCreated = c.DateCreated,

            }).ToList();
        }

        public async Task<CreditCard> GetByAccountNumber(string accountNumber) { }

        public async Task<CreditCard> GetByAccountNumberLoggedUser(string accountNumber, int clientId)

        {
            var creditCard = await _creditCardRepository.GetByAccountNumberLoggedUser(accountNumber, clientId);
            if (creditCard == null)
            {
                return null;
            }

            return creditCard;
        }

        public async Task UpdateCreditCard(double balance, double debt, string accountNumber, int clientId)
        {
            var creditCard = await GetByAccountNumberLoggedUser(accountNumber, clientId);

            SaveCreditCardViewModel vm = new SaveCreditCardViewModel
            {
                Id = creditCard.Id,
                ClientId = creditCard.ClientId,
                DateCreated = creditCard.DateCreated,
                Balance = balance,
                Debt = debt,
                Limit = creditCard.Limit
            };

            await base.UpdateProduct(vm, accountNumber);
        }

    }
}
