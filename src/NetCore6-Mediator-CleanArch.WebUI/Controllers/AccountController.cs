using Microsoft.AspNetCore.Mvc;
using NetCore6_Mediator_CleanArch.Domain.Account;
using NetCore6_Mediator_CleanArch.WebUI.ViewModels;

namespace NetCore6_Mediator_CleanArch.WebUI.Controllers;

public class AccountController : Controller
{
    private readonly IAuthenticate _authenticate;

    public AccountController(IAuthenticate authenticate)
    {
        _authenticate = authenticate;
    }

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
        bool result = await _authenticate.Authenticate(model.Email, model.Password);

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
        bool result = await _authenticate.RegisterUser(model.Email, model.Password);

        if (result)
            return Redirect("/");

        ModelState.AddModelError(string.Empty, "Invalid login attempt.(password must be strong).");
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _authenticate.Logout();
        return Redirect("/Account/Login");
    }
}
