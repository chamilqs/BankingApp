using Microsoft.AspNetCore.Identity;
using BankingApp.Infrastructure.Identity.Entities;
using BankingApp.Core.Application.Enums;
using BankingApp.Core.Application.ViewModels.Client;
using BankingApp.Core.Application.ViewModels.SavingsAccount;
using BankingApp.Core.Application.Services;

namespace BankingApp.Infrastructure.Identity.Seeds
{
    public static class DefaultClientUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ClientService clientService, SavingsAccountService savingsAccountService)
        {
            ApplicationUser defaultUser = new();
            defaultUser.UserName = "clientuser";
            defaultUser.Email = "clientuser@gmail.com";
            defaultUser.Name = "Heung Min";
            defaultUser.LastName = "Son";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;
            defaultUser.IdentificationNumber = "215-3254478-5";
            defaultUser.IsActive = true;

            if(userManager.Users.All(u=> u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByNameAsync(defaultUser.UserName);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123P4$$w0rd!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Client.ToString());
                }

                SaveClientViewModel saveClientViewModel = new()
                {
                    UserId = user.Id,
                    DateCreated = DateTime.UtcNow,
                };

                var client = await clientService.Add(saveClientViewModel);

                SaveSavingsAccountViewModel savingsAccountViewModel = new()
                {
                    Id = "111222333",
                    ClientId = client.Id,
                    Balance = 0.00,
                    DateCreated = DateTime.UtcNow,
                    IsMainAccount = true
                };

                await savingsAccountService.Add(savingsAccountViewModel);
            }
        }
    }
}
