using BankingApp.Core.Application.Dtos.Account;
using BankingApp.Core.Application.ViewModels.User;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface IAdminService
    {
        Task<List<UserViewModel>> GetAllViewModel();
        Task<GenericResponse> UpdateUserStatus(string userId);
		Task<int> GetActiveUsersCount();
        Task<int> GetInactiveUsersCount();
        Task<int> GetTotalPaymentsCount();
        Task<int> GetTodayTotalPaymentsCount();
		Task<int> GetTotalTransactionsCount();
        Task<int> GetTodayTotalTransactionsCount();
		Task<int> GetTotalLoansCount();
        Task<int> GetTotalSavingsAccountsCount();
        Task<int> GetTotalCreditCardsCount();            

	}
}
