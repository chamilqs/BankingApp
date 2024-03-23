using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.ViewModels.User;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
        Task<SaveUserViewModel> UpdateUserAsync(SaveUserViewModel vm);
        Task SignOutAsync();
        Task<List<UserDTO>> GetAllUserAsync();
    }
}