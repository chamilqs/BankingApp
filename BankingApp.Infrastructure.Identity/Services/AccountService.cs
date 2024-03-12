using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.DTOs.Email;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.User;
using BankingApp.Infrastructure.Identity.Entities;
using BankingApp.Core.Application.Enums;

namespace BankingApp.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No accounts registered with the email: {request.Email}.";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Email}.";
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account not confirmed for {request.Email}, please contact an administrator.";
                return response;
            }

            response.Id = user.Id;
            response.Name = user.Name;
            response.LastName = user.LastName;
            response.ProfilePicture = user.ProfilePicture;
            response.Phone = user.PhoneNumber;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SaveUserViewModel> UpdateUserAsync(SaveUserViewModel vm)
        {
            // needs to recieve the ID of the user by parameter
            ApplicationUser userVm = await _userManager.FindByIdAsync("");

            if (userVm == null)
            {
                Console.WriteLine("User not found.");
                return null;
            }

            userVm.Name = vm.Name;
            userVm.LastName = vm.LastName;
            userVm.UserName = vm.Username;
            userVm.PhoneNumber = vm.Phone;
            userVm.ProfilePicture = vm.ProfilePicture;
            userVm.Email = vm.Email;

            var updateResult = await _userManager.UpdateAsync(userVm);

            if (!updateResult.Succeeded)
            {
                Console.WriteLine("Error updating user.");
                return null;
            }

            if (!string.IsNullOrEmpty(vm.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(userVm);
                var passwordChangeResult = await _userManager.ResetPasswordAsync(userVm, token, vm.Password);

                if (!passwordChangeResult.Succeeded)
                {
                    Console.WriteLine("Error changing password.");
                    return null;
                }
            }

            return vm;
        }


        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"Username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"This email '{request.Email}' is already registered.";
                return response;
            }

            var user = new ApplicationUser
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.Phone,
                ProfilePicture = request.ProfilePicture,
                UserName = request.UserName
                
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                if (request.Role == Roles.Admin.ToString())
                {
                    await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                } 
                else if(request.Role == Roles.Client.ToString())
                {
                    await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
                }                

                await _emailService.SendAsync(new EmailRequest()
                {
                    To = user.Email,
                    Subject = "Welcome to RoyalBank, your bank.",
                    Body = $"Thanks for trust in us to be your bank."

                });
            }

            else
            {
                response.HasError = true;
                response.Error = $"An error occurred while trying to register the user.";
                return response;
            }

            return response;
        }        
    
    }

}
