using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.ViewModels.User;
using BankingApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using BankingApp.Core.Application.Dtos.Account;
using Azure;
using BankingApp.Core.Application.ViewModels.SavingsAccount;

namespace BankingApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IClientService _clientService;
        private readonly IAdminService _adminService;
        private readonly AuthenticationResponse _authViewModel;

        public AdminController(IHttpContextAccessor httpContextAccessor, IUserService userService, IClientService clientService, IAdminService adminService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _userService = userService;
            _clientService = clientService;
            _adminService = adminService;
        }

        #region Dashboard
        public IActionResult Dashboard()
        {
            return View();
        }
        #endregion

        #region GetAllUsers
        public async Task<IActionResult> Index(bool hasError = false, string? message = null)
        {
            List<UserViewModel> users = await _adminService.GetAllViewModel();
            ViewBag.User = _authViewModel;
            GenericResponse response = new() 
            { 
                HasError = hasError,
            };
            
            if (hasError)
                response.Error = message;
            ViewBag.Response = response;

            return View(users);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            return View("SaveUser", new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveUser", vm);
            }

            RegisterResponse response = new();

            if (vm.Role == (int)Roles.Admin)
            {
               response = await _userService.RegisterAsync(vm);
            }
            else if (vm.Role == (int)Roles.Client)
            {
                response = await _clientService.RegisterAsync(vm);
            }

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View("SaveUser", vm);
            }

            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }
        #endregion

        #region Active & Unactive User
        [HttpPost]
        public async Task<IActionResult> UpdateUserStatus(string username)
        {
            var response = await _adminService.UpdateUserStatus(username);

            if (response.HasError)
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index", hasError = response.HasError, message = response.Error });
            }

            return RedirectToRoute(new { controller = "Admin", action = "Index",  });
        }
        #endregion

        #region Edit User
        public async Task<IActionResult> Edit(string username)
        {
            SaveUserViewModel vm = await _userService.GetUpdateUserAsync(username);

            return View("SaveUser", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveUser", vm);
            }

            GenericResponse response = new();

            if (vm.Role == (int)Roles.Admin)
            {
                response = await _userService.UpdateUserAsync(vm);
            }
            else if (vm.Role == (int)Roles.Client)
            {
                response = await _clientService.UpdateAsync(vm);
            }

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View("SaveUser", vm);
            }

            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }
        #endregion

        #region Products
        public async Task<IActionResult> IndexProducts(string? userId = null, bool hasError = false, string? message = null)
        {
            try
            {
                GenericResponse response = new()
                {
                    HasError = hasError,
                };

                if (hasError)
                    response.Error = message;
                ViewBag.Response = response;

                return View(await _clientService.GetAllProducts(userId));
            }
            catch (Exception ex)
            {
                var user = await _userService.GetById(userId);

                return RedirectToRoute(new { controller = "Admin", action = "Index", userId = user.Id, hasError = true, message = $"An error has occured trying to get the products of the user: {user.Username}" });
            }
        }
        #endregion



    }
}
