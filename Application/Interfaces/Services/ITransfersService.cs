using BankingApp.Core.Application.ViewModels.Transaction;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface ITransfersService
    {
        Task<SaveTransactionViewModel> Transfer(SaveTransactionViewModel vm, Enums.TransactionType transactionType, bool isCashAdvance);
        Task<bool> AddMoneyToAccount(string accountNumberDestination, string accountNumberOrigin, double amount);
        Task<bool> AddMoneyToAccountCreditCard(string accountNumberOrigin, string accountNumberDestination, double amount);
        Task<string> CashAdvance(SaveTransactionViewModel vm);
    }
}
