using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Domain.Entities;
using BankingApp.Infrastructure.Persistence.Contexts;
using BankingApp.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Infrastucture.Persistence.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly ApplicationContext _dbContext;
        public ClientRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> GetByAccountNumber(string accountNumber)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.SavingsAccounts.Any(s => s.Id == accountNumber));

            return client;
        }


    }
}
