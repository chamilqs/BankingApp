using BankingApp.Core.Application.ViewModels.SavingsAccount;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface ISavingsAccountService : IGenericService<SaveSavingsAccountViewModel, SavingsAccountViewModel, SavingsAccount>
    {
        Task<SavingsAccount> GetByAccountNumber(string accountNumber, int ClientId);
        Task UpdateSavingsAccount(double balance, int ClientId, string id);
    }
}
