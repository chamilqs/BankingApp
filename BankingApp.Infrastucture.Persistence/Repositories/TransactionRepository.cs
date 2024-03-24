using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Domain.Entities;
using BankingApp.Infrastructure.Persistence.Contexts;
using BankingApp.Infrastructure.Persistence.Repository;

namespace BankingApp.Infrastucture.Persistence.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly ApplicationContext _dbContext;
        public TransactionRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
