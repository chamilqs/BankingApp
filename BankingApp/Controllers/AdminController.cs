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

namespace BankingApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly AuthenticationResponse authViewModel;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            authViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _userService = userService;
            _userManager = userManager;
        }

        /*
         The methods need to redirect to the Maintenance page when a process is finalized, like reseting a password or registering a new user.
         The EditProfile by POST method needs to be configured so the method recieves the ID of the user that is going to be edited

         
         */
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult ClientMaintenance()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            users = _userManager.Users.ToList();

            return View(users);
        }

        public IActionResult Register()
        {
            ViewBag.Roles = Get.ListRoles();
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            vm.ProfilePicture = UploadFile(vm.File, vm.Email);
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


        // needs mantainense
        public async Task<IActionResult> EditProfile(string userId)
        {
            ApplicationUser vm = await _userManager.FindByIdAsync(userId);
            SaveUserViewModel svm = new()
            {
                Name = vm.Name,
                LastName = vm.LastName,
                Username = vm.UserName,
                Email = vm.Email,
                Phone = vm.PhoneNumber,
                ProfilePicture = vm.ProfilePicture
            };
            return View("EditProfile", svm);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(SaveUserViewModel vm)
        {
            // Needs to be configured so the method recieves the ID from the Client that its going to be edited

            // ApplicationUser userVm = await _userManager.FindByIdAsync();

            if (!ModelState.IsValid)
            {
                return View("EditProfile", vm);
            }

            if (vm.Password.IsNullOrEmpty())
            {
                // vm.Password = userVm.PasswordHash;
            }

            if (vm.File != null)
            {
                // vm.ProfilePicture = UploadFile(vm.File, authViewModel.Email, true, userVm.ProfilePicture);
            }
            else
            {
                // vm.ProfilePicture = userVm.ProfilePicture;
            }

            await _userService.UpdateUserAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "ClientMaintenance" });
        }

        private string UploadFile(IFormFile file, string email, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Users/{email}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }

        public static class Get
        {
            public static IEnumerable<SelectListItem> ListRoles()
            {
                var selectedRoles = new List<Roles> { Roles.Client, Roles.Admin };  

                var roles = selectedRoles
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = Enum.GetName(typeof(Roles), e)
                    });

                return roles;
            }
        }


    }
}
