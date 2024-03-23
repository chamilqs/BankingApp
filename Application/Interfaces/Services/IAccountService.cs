using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.ViewModels.User;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request);
        Task<SaveUserViewModel> UpdateUserAsync(SaveUserViewModel vm);
        Task SignOutAsync();
        Task<List<UserDTO>> GetAllUserAsync();
        Task<UserDTO> FindByUsernameAsync(string username);
    }
}