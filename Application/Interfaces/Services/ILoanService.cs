using BankingApp.Core.Application.ViewModels.CreditCard;
using BankingApp.Core.Application.ViewModels.Loan;
using BankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface ILoanService : IGenericService<SaveLoanViewModel, LoanViewModel, Loan>
    {

        Task<List<LoanViewModel>> GetAllByClientId(int clientId);
        Task<Loan> GetByAccountNumberLoggedUser(string accountNumber, int clientId);
        Task<Loan> GetByAccountNumber(string accountNumber);

        Task UpdateLoan(double balance, double amount, string accountNumber, int clientId);
    }
}
