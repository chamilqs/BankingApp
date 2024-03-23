using Microsoft.AspNetCore.Identity;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.DTOs.Email;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.User;
using BankingApp.Infrastructure.Identity.Entities;
using BankingApp.Core.Application.Enums;
using Microsoft.EntityFrameworkCore;
using Azure;

namespace BankingApp.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Login & Logout
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No accounts registered with the username: {request.Username}.";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Username}.";
                return response;
            }

            if (!user.IsActive)
            {
                response.HasError = true;
                response.Error = $"Account disactive for {request.Username}, please contact the administrator.";
                return response;
            }

            response.Id = user.Id;
            response.Name = user.Name;
            response.LastName = user.LastName;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.IdentificationNumber = user.IdentificationNumber;
            response.IsActive = user.IsActive;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        #endregion

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
            userVm.IdentificationNumber = vm.IdentificationNumber;
            userVm.IsActive = vm.IsActive;
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

        #region Register
        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request)
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

            var user = new ApplicationUser
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                IdentificationNumber = request.IdentificationNumber,
                IsActive = request.IsActive
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
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred while trying to register the user.";
                return response;
            }

            return response;
        }
        #endregion

        #region GetAllUserAsync
        public async Task<List<UserDTO>> GetAllUserAsync()
        {
            var userList = await _userManager.Users.ToListAsync();

            var userDTOList = userList.Select(user => new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Name = user.Name,
                LastName = user.LastName,
                IdentificationNumber = user.IdentificationNumber,
                IsActive = user.IsActive
            }).ToList();

            foreach (var userDTO in userDTOList)
            {
                var user = await _userManager.FindByIdAsync(userDTO.Id);

                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

                userDTO.Role = rolesList.ToList()[0];
            }

            return userDTOList;
        }
        #endregion

        #region GetByUsername & GetById
        public async Task<UserDTO> FindByUsernameAsync(string username)
        {
            UserDTO userDTO = new();

            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                userDTO.Id = user.Id;
                userDTO.Username = user.UserName;
                userDTO.Name = user.Name;
                userDTO.LastName = user.LastName;
                userDTO.IdentificationNumber = user.IdentificationNumber;

                return userDTO;
            }

            return null;
        }
        #endregion
    }

}
