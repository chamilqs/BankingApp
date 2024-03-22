using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.ViewModels.User;
using BankingApp.Infrastructure.Identity.Entities;
using BankingApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAdmin.BankingApp.Middlewares;
using BankingApp.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankingApp.Core.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using System;

namespace BankingApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly AuthenticationResponse _authViewModel;

        public AdminController(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _userService = userService;
        }

        /*
         The methods need to redirect to the Maintenance page when a process is finalized, like reseting a password or registering a new user.
         The EditProfile by POST method needs to be configured so the method recieves the ID of the user that is going to be edited

         
         */
        public IActionResult Dashboard()
        {
            return View();
        }

        #region GetAllUsers
        public async Task<IActionResult> Index()
        {
            List<UserViewModel> users = await _userService.GetAllViewModel();
            ViewBag.User = _authViewModel;

            return View(users);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            ViewBag.Roles = Enum.GetNames(typeof(Roles));

            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var origin = Request.Headers["origin"];

            RegisterResponse response = await _userService.RegisterAsync(vm, origin);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            return RedirectToRoute(new { controller = "Admin", action = "ClientMaintenance" });
        }
        #endregion


        //// needs mantainense
        //public async Task<IActionResult> EditProfile(string userId)
        //{
        //    ApplicationUser vm = await _userManager.FindByIdAsync(userId);
        //    SaveUserViewModel svm = new()
        //    {
        //        Name = vm.Name,
        //        LastName = vm.LastName,
        //        Username = vm.UserName,
        //        Email = vm.Email,
        //        Phone = vm.PhoneNumber,
        //        IdentificationNumber = vm.IdentificationNumber,
        //        IsActive = vm.IsActive,
        //    };
        //    return View("EditProfile", svm);
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditProfile(SaveUserViewModel vm)
        //{
        //    // Needs to be configured so the method recieves the ID from the Client that its going to be edited
        //    // then you can search for the user to get the current data, just in case the user does not want to change
        //    // his password or others

        //    // ApplicationUser userVm = await _userManager.FindByIdAsync();

        //    if (!ModelState.IsValid)
        //    {
        //        return View("EditProfile", vm);
        //    }

        //    if (vm.Password.IsNullOrEmpty())
        //    {
        //        // vm.Password = userVm.PasswordHash;
        //    }

        //    await _userService.UpdateUserAsync(vm);
        //    return RedirectToRoute(new { controller = "Admin", action = "ClientMaintenance" });
        //}

    }
}
