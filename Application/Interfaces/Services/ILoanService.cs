using BankingApp.Core.Application.ViewModels.Loan;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface ILoanService : IGenericService<SaveLoanViewModel, LoanViewModel, Loan>
    {

        Task<List<LoanViewModel>> GetAllByClientId(int clientId);
        Task<Loan> GetByAccountNumberLoggedUser(string accountNumber, int clientId);
        Task<Loan> GetByAccountNumber(string accountNumber);
        Task DeleteProduct(string id);
    }
}
