using BankingApp.Core.Application.Dtos.Account;
using BankingApp.Core.Application.ViewModels.User;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface IAdminService
    {
        Task<List<UserViewModel>> GetAllViewModel();
        Task<GenericResponse> UpdateUserStatus(string userId);
    }
}
