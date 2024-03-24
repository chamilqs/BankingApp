using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Interfaces.Repositories
{
    public interface ISavingsAccountRepository : IGenericRepository<SavingsAccount>
    {
        Task<SavingsAccount> GetByAccountNumberLoggedUser(string accountNumber, int clientId);
        Task<SavingsAccount> GetByAccountNumber(string accountNumber);
    }
}
