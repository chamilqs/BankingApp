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
        public Task<Beneficiary> GetByAccountNumber(string accountNumber)
        {
            var beneficiary = _dbContext.Beneficiaries.FirstOrDefaultAsync(b => b.SavingsAccountId == accountNumber);
            if(beneficiary == null)
            {
                return null;
            }

            return beneficiary;

        }
    }
}
