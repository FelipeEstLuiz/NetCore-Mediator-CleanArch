using Microsoft.AspNetCore.Mvc;
using NetCore_Mediator_CleanArch.Domain.Account;
using NetCore_Mediator_CleanArch.WebUI.ViewModels;

namespace NetCore_Mediator_CleanArch.WebUI.Controllers;

public class AccountController(IAuthenticate authenticate) : Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel()
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        bool result = await authenticate.Authenticate(model.Email, model.Password);

        if (result)
        {
            if (string.IsNullOrEmpty(model.ReturnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(model.ReturnUrl);
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.(password must be strong).");
        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        bool result = await authenticate.RegisterUser(model.Email, model.Password);

        if (result)
            return Redirect("/");

        ModelState.AddModelError(string.Empty, "Invalid login attempt.(password must be strong).");
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await authenticate.Logout();
        return Redirect("/Account/Login");
    }
}
