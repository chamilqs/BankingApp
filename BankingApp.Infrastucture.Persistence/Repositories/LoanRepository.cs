using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Domain.Entities;
using BankingApp.Infrastructure.Persistence.Contexts;
using BankingApp.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Infrastucture.Persistence.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        private readonly ApplicationContext _dbContext;

        public LoanRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Loan> GetByAccountNumber(string accountNumber)
        {
            var loan = _dbContext.Loans.FirstOrDefaultAsync(b => b.Id == accountNumber);

            if (loan == null)
            {
                return null;
            }

            return loan;
        }

        public Task<Loan> GetByAccountNumberLoggedUser(string accountNumber, int clientId)
        {
            var loan = _dbContext.Loans.FirstOrDefaultAsync(b => b.Id == accountNumber && b.ClientId == clientId);

            if (loan == null)
            {
                return null;
            }

            return loan;

        }
    }
}
