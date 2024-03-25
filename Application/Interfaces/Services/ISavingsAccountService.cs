using BankingApp.Core.Application.ViewModels.SavingsAccount;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface ISavingsAccountService : IGenericService<SaveSavingsAccountViewModel, SavingsAccountViewModel, SavingsAccount>
    {
        Task<SavingsAccount> GetByAccountNumberLoggedUser(string accountNumber, int ClientId);
        Task<SavingsAccount> GetByAccountNumber(string accountNumber);
        Task UpdateSavingsAccount(double balance, int ClientId, string id);
        Task<SavingsAccountViewModel> GetClientMainAccount(int ClientId);
    }
}
