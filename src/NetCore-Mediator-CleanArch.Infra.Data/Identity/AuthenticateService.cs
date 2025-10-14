using Microsoft.AspNetCore.Identity;
using NetCore_Mediator_CleanArch.Domain.Account;

namespace NetCore_Mediator_CleanArch.Infra.Data.Identity;

public class AuthenticateService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthenticate
{
    public async Task<bool> Authenticate(string email, string password)
    {
        SignInResult result = await signInManager.PasswordSignInAsync(
            email,
            password,
            isPersistent: false,
            lockoutOnFailure: false
        );

        return result.Succeeded;
    }

    public async Task Logout() => await signInManager.SignOutAsync();

    public async Task<bool> RegisterUser(string email, string password)
    {
        ApplicationUser applicationUser = new()
        {
            UserName = email,
            Email = email
        };

        IdentityResult result = await userManager.CreateAsync(applicationUser, password);

        if (result.Succeeded)
            await signInManager.SignInAsync(applicationUser, isPersistent: false);

        return result.Succeeded;
    }
}
