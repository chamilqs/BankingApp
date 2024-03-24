using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Domain.Entities;
using BankingApp.Infrastructure.Persistence.Contexts;
using BankingApp.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Infrastucture.Persistence.Repositories
{
    public class BeneficiaryRepository : GenericRepository<Beneficiary>, IBeneficiaryRepository
    {
        private readonly ApplicationContext _dbContext;
        public BeneficiaryRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<SavingsAccount> GetByAccountNumber(string accountNumber)
        {
            var sa = _dbContext.SavingsAccounts.FirstOrDefaultAsync(b => b.Id == accountNumber);
            if(sa == null)
            {
                return null;
            }

            return sa;

        }

        public Task<Beneficiary> GetBeneficiary(string accountNumber)
        {
            var beneficiary = _dbContext.Beneficiaries.FirstOrDefaultAsync(b => b.SavingsAccountId == accountNumber);
            if (beneficiary == null)
            {
                return null;
            }

            return beneficiary;

        }
    }
}
