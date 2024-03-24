using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Interfaces.Repositories
{
    public interface IBeneficiaryRepository : IGenericRepository<Beneficiary>
    {
        Task<SavingsAccount> GetByAccountNumber(string accountNumber);
        Task<Beneficiary> GetBeneficiary(string accountNumber);

    }
}
