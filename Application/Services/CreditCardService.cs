using AutoMapper;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.Beneficiary;
using BankingApp.Core.Application.ViewModels.CreditCard;
using BankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BankingApp.Core.Application.Services
{
    public class CreditCardService : GenericService<SaveCreditCardViewModel, CreditCardViewModel, CreditCard>, ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        private readonly IMapper _mapper;

        public CreditCardService(ICreditCardRepository creditCardRepository, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(creditCardRepository, mapper)
        {
            _creditCardRepository = creditCardRepository;
            _httpContextAccessor = httpContextAccessor;

            _mapper = mapper;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        #region Delete
        public async Task DeleteProduct(string id)
        {
            var crediCard = await GetByAccountNumber(id);

            await _creditCardRepository.DeleteAsync(crediCard);
        }
        #endregion

        public async Task<CreditCard> GetByAccountNumber(string accountNumber)
        {
            var creditCard = await _creditCardRepository.GetByAccountNumber(accountNumber);
            if (creditCard == null)
            {
                return null;
            }

            return creditCard;
        }

        public async Task<List<CreditCardViewModel>> GetAllByClientId(int clientId)
        {
            var creditCards = await _creditCardRepository.GetAllAsync();

            var creditCardsViewModels = new List<CreditCardViewModel>();

            foreach (var creditCard in creditCards.Where(b => b.ClientId == clientId))
            {

                var creditCardViewModel = new CreditCardViewModel
                {
                    ClientId = creditCard.ClientId,
                    Id = creditCard.Id,
                    Balance = creditCard.Balance,
                    DateCreated = creditCard.DateCreated,
                    Debt = creditCard.Debt,
                    Limit = creditCard.Limit,
                };

                creditCardsViewModels.Add(creditCardViewModel);
            }

            return creditCardsViewModels;
        }

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
