using BankingApp.Core.Application.ViewModels.CreditCard;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface ICreditCardService : IGenericService<SaveCreditCardViewModel, CreditCardViewModel, CreditCard>
    {


        Task<List<CreditCardViewModel>> GetAllByClientId(int clientId);

        Task<CreditCard> GetByAccountNumberLoggedUser(string accountNumber, int clientId);
        Task<CreditCard> GetByAccountNumber(string accountNumber);

        Task UpdateCreditCard(double balance, double debt, string accountNumber, int clientId);
    }
}
