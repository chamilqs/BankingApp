using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Domain.Entities;
using BankingApp.Infrastructure.Persistence.Contexts;
using BankingApp.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Infrastucture.Persistence.Repositories
{
    public class SavingsAccountRepository : GenericRepository<SavingsAccount>, ISavingsAccountRepository
    {
        private readonly ApplicationContext _dbContext;
        public SavingsAccountRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<SavingsAccount> GetByAccountNumber(string accountNumber)
        {
            var savingsAccount = _dbContext.SavingsAccounts.FirstOrDefaultAsync(b => b.Id == accountNumber);

            if (savingsAccount == null)
            {
                return null;
            }

            return savingsAccount;
        }

        public Task<SavingsAccount> GetByAccountNumberLoggedUser(string accountNumber, int clientId)
        {
            var savingsAccount = _dbContext.SavingsAccounts.FirstOrDefaultAsync(b => b.Id == accountNumber && b.ClientId == clientId);

            if (savingsAccount == null)
            {
                return null;
            }

            return savingsAccount;

        }
    }
}
