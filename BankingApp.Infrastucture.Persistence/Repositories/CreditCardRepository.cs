using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Domain.Entities;
using BankingApp.Infrastructure.Persistence.Contexts;
using BankingApp.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Infrastucture.Persistence.Repositories
{
    public class CreditCardRepository : GenericRepository<CreditCard>, ICreditCardRepository
    {
        private readonly ApplicationContext _dbContext;
        public CreditCardRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<CreditCard> GetByAccountNumber(string accountNumber, int clientId)
        {
            var creditCard = _dbContext.CreditCards.FirstOrDefaultAsync(b => b.Id == accountNumber && b.ClientId == clientId);
            if (creditCard == null)
            {
                return null;
            }

            return creditCard;

        }
    }
}
