using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Interfaces.Repositories
{
    public interface ICreditCardRepository : IGenericRepository<CreditCard>
    {
        Task<CreditCard> GetByAccountNumberLoggedUser(string accountNumber, int clientId);
        Task<CreditCard> GetByAccountNumber(string accountNumber);
    }
}
