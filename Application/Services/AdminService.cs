using AutoMapper;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.User;

namespace BankingApp.Core.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AdminService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        #region GetAllViewModel
        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var userList = await _accountService.GetAllUserAsync();

            return _mapper.Map<List<UserViewModel>>(userList);
        }
        #endregion

        #region UpdateUserStatus
        public async Task UpdateUserStatus(string userId)
        {
            await _accountService.UpdateUserStatusAsync(userId);
        }
        #endregion
    }
}
