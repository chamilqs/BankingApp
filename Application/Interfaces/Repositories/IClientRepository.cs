using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Interfaces.Repositories
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<Client> GetByAccountNumber(string accountNumber);
    }
}
