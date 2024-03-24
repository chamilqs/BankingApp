using AutoMapper;
using BankingApp.Core.Application.Dtos.Account;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Enums;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.User;

namespace BankingApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        #region Login & Logout
        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }
        #endregion

        #region Register
        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);

            if (vm.Role == (int)Roles.Admin)
            {
                registerRequest.Role = Roles.Admin.ToString();                
            }
            else if (vm.Role == (int)Roles.Client)
            {
                registerRequest.Role = Roles.Client.ToString();
            }

            return await _accountService.RegisterUserAsync(registerRequest);
        }
        #endregion

        #region Update
        public async Task<GenericResponse> UpdateUserAsync(SaveUserViewModel vm)
        {
            UpdateUserRequest updateRequest = _mapper.Map<UpdateUserRequest>(vm);

            return await _accountService.UpdateUserAsync(updateRequest);
        }
        #endregion

        #region GetUserByUsername
        public async Task<UserViewModel> GetByUsername(string username)
        {
            UserDTO userDTO = await _accountService.FindByUsernameAsync(username);

            UserViewModel vm = _mapper.Map<UserViewModel>(userDTO);

            return vm;
        }
        #endregion
    }
}
