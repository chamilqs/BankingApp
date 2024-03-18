using Microsoft.AspNetCore.Identity;
using BankingApp.Infrastructure.Identity.Entities;
using BankingApp.Core.Application.Enums;

namespace BankingApp.Infrastructure.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();
            defaultUser.UserName = "adminuser";
            defaultUser.Email = "adminuser@gmail.com";
            defaultUser.Name = "John";
            defaultUser.LastName = "Doe";            
            defaultUser.PhoneNumber = "(123) 456-7890";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;
            defaultUser.IdentificationNumber = "15354565478";
            defaultUser.IsActive = true;

            if(userManager.Users.All(u=> u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123P4$$w0rd!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }
         
        }
    }
}
