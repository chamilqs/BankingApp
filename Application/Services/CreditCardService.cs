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

        public Task<CreditCardViewModel> GetByAccountNumber(string accountNumber)
        {
            throw new NotImplementedException();
        }
    }
}
