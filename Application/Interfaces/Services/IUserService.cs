using BankingApp.Core.Application.Dtos.Account;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.ViewModels.User;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm);
        Task<GenericResponse> UpdateUserAsync(SaveUserViewModel vm);
        Task SignOutAsync();
        Task<UserViewModel> GetByUsername(string username);
    }
}
