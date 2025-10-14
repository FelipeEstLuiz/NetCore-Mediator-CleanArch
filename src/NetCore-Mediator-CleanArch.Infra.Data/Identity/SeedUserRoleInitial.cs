using Microsoft.AspNetCore.Identity;
using NetCore_Mediator_CleanArch.Domain.Account;

namespace NetCore_Mediator_CleanArch.Infra.Data.Identity;

public class SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : ISeedUserRoleInitial
{
    public void SeedUsers()
    {
        if (userManager.FindByNameAsync("usuario@localhost").Result == null)
        {
            ApplicationUser user = new()
            {
                UserName = "usuario@localhost",
                Email = "usuario@localhost",
                NormalizedUserName = "USUARIO@LOCALHOST",
                NormalizedEmail = "USUARIO@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = userManager.CreateAsync(user, "Numsey#2022").Result;

            if (result.Succeeded)
                userManager.AddToRoleAsync(user, "User").Wait();
        }

        if (userManager.FindByNameAsync("admin@localhost").Result == null)
        {
            ApplicationUser user = new()
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                NormalizedUserName = "ADMIN@LOCALHOST",
                NormalizedEmail = "ADMIN@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = userManager.CreateAsync(user, "Numsey#2022").Result;

            if (result.Succeeded)
                userManager.AddToRoleAsync(user, "Admin").Wait();
        }
    }

    public void SeedRoles()
    {
        if (!roleManager.RoleExistsAsync("User").Result)
        {
            IdentityRole role = new()
            {
                Name = "User",
                NormalizedName = "USER"
            };
            roleManager.CreateAsync(role).Wait();
        }

        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            roleManager.CreateAsync(role).Wait();
        }
    }
}
