using Microsoft.AspNetCore.Identity;
using NetCore6_Mediator_CleanArch.Domain.Account;

namespace NetCore6_Mediator_CleanArch.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(
                email,
                password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            ApplicationUser applicationUser = new()
            {
                UserName = email,
                Email = email
            };

            IdentityResult result = await _userManager.CreateAsync(applicationUser, password);

            if (result.Succeeded)
                await _signInManager.SignInAsync(applicationUser, isPersistent: false);

            return result.Succeeded;
        }
    }
}
