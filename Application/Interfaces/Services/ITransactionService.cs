using BankingApp.Core.Application.ViewModels.Transaction;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface ITransactionService : IGenericService<SaveTransactionViewModel, TransactionViewModel, Beneficiary>
    {


    }
}
