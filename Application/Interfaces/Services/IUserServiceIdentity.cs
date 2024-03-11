﻿using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.ViewModels.User;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface IUserServiceIdentity
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginIdentityViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveIdentityUserViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task<SaveIdentityUserViewModel> UpdateUserAsync(SaveIdentityUserViewModel vm);
        Task SignOutAsync();
    }
}
