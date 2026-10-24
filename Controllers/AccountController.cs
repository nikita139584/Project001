using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Project001.Models;
using Project001.Services;
using System.Security.Claims;

namespace Project001.Controllers;

/// <summary>Вход и выход. Регистрация живёт в FormController.</summary>
public class AccountController : Controller
{
    private readonly IRegistrationStore _store;

    public AccountController(IRegistrationStore store)
    {
        _store = store;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
        => View(new LoginViewModel { ReturnUrl = returnUrl });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(
        LoginViewModel model,
        CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _store.FindByEmailAsync(model.Email, ct);

        // Одно и то же сообщение для неверной почты и неверного пароля:
        // иначе форма подсказывает, какие адреса зарегистрированы.
        if (user is null || !PasswordHasher.Verify(model.Password, user.PasswordHash))
        {
            ModelState.AddModelError(string.Empty, "Неверный email или пароль");
            return View(model);
        }

        await SignInAsync(user.Id, user.Name, user.Email, model.RememberMe);

        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        {
            return Redirect(model.ReturnUrl);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }

    /// <summary>Регистрация — это форма по адресу /Form.</summary>
    [HttpGet]
    public IActionResult Register()
        => RedirectToAction("Index", "Form");

    private Task SignInAsync(
        string id,
        string name,
        string email,
        bool persistent)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, id),
            new(ClaimTypes.Name, string.IsNullOrWhiteSpace(name) ? email : name),
            new(ClaimTypes.Email, email)
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        return HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties { IsPersistent = persistent });
    }
}
