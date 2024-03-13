using Microsoft.AspNetCore.Identity;
using BankingApp.Infrastructure.Identity.Entities;
using BankingApp.Core.Application.Enums;

namespace BankingApp.Infrastructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();
            defaultUser.UserName = "clientuser";
            defaultUser.Email = "clientuser@email.com";
            defaultUser.Name = "Heung Min";
            defaultUser.LastName = "Son";      
            defaultUser.PhoneNumber = "(123) 456-7890";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;
            defaultUser.IdentificationNumber = "21532544785";
            defaultUser.IsActive = true;

            if(userManager.Users.All(u=> u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Client.ToString());
                }
            }
         
        }
    }
}
