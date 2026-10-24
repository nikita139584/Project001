
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Project001.Models;
using Project001.Services;
using System.Security.Claims;


namespace Project001.Controllers;

/// <summary>Страница регистрации: https://localhost:7114/Form</summary>
public class FormController : Controller
{
    private static readonly string[] AllowedCountries =
    {
        "Украина", "Польша", "Германия", "Чехия", "Литва", "Другая"
    };

    private readonly IRegistrationStore _store;
    private readonly ILogger<FormController> _logger;

    public FormController(
        IRegistrationStore store,
        ILogger<FormController> logger)
    {
        _store = store;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
        => View("Form", new RegistrationViewModel());

    [HttpPost("Form")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(
        RegistrationViewModel model,
        CancellationToken ct)
    {
        if (!AllowedCountries.Contains(model.Country))
            ModelState.AddModelError(
                nameof(model.Country),
                "Выберите страну из списка");

        if (model.BirthDate is { } bd &&
            (bd > DateTime.Today || bd.Year < 1900))
        {
            ModelState.AddModelError(
                nameof(model.BirthDate),
                "Некорректная дата рождения");
        }

        if (!ModelState.IsValid)
            return View("Form", model);



        try
        {
            if (await _store.EmailExistsAsync(model.Email, ct))
            {
                ModelState.AddModelError(
                    nameof(model.Email),
                    "Этот email уже зарегистрирован");

                return View("Form", model);
            }
            var id = await _store.SaveAsync(model, ct);

            _logger.LogInformation(
                "Registration saved to Firestore: {Id}", id);

            // Сразу входим под новым пользователем: после регистрации
            // человек должен увидеть приветствие, а не форму входа.
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, id),
                new(ClaimTypes.Name, model.Name.Trim()),
                new(ClaimTypes.Email, model.Email.Trim())
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties { IsPersistent = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex, "Не удалось сохранить регистрацию в Firestore");

            ModelState.AddModelError(
                string.Empty,
                "Не удалось сохранить данные. Попробуйте ещё раз позже.");

            return View("Form", model);
        }

        TempData["RegisteredName"] = model.Name.Trim();

        return RedirectToAction("Index", "Home");
    }
}
