using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.ServiceContracts.DTO.AccountDto;
using SportsShop.Core.ServiceContracts.DTO.CountryDto;
using SportsShop.Core.ServiceContracts.Interfaces.IAccount;
using SportsShop.Core.ServiceContracts.Interfaces.ICountry;

namespace SportsShop.UI.Controllers;
public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;
    private readonly ICountryGetterService _countryGetterService;

    public AccountController(SignInManager<User> signInManager, IAccountService accountService, ILogger<AccountController> logger, ICountryGetterService countryGetterService)
    {
        _signInManager = signInManager;
        _accountService = accountService;
        _countryGetterService = countryGetterService;
        _logger = logger;
    }

    public IActionResult SignIn()
    {
        _logger.LogDebug("SignIn action method returns SignIn view");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn(SignInDto signInDto)
    {
        _logger.LogDebug("[HttpPost]SignIn action method started. Parameters signInDto: {signInDto}", signInDto.ToString());
        
        if (!ModelState.IsValid)
        {
            return View(signInDto);
        }

        var result = await _accountService.SignInAsync(signInDto);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("Password", "Invalid Password");
        return View(signInDto);
    }
    public IActionResult CreateUser()
    {
        _logger.LogDebug("CreateUser action method returns CreateUser view");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(RegisterDto registerDto)
    {
        _logger.LogDebug("[HttpPost]CreateUser action method started. Parameter: registerDto: {dto}", registerDto.ToString());

        if (!ModelState.IsValid)
        {
            return View(registerDto);
        }

        IdentityResult result = await _accountService.RegisterAsync(registerDto);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(registerDto);
        }
    }

    public async Task<IActionResult> Logout()
    {
        _logger.LogDebug("Logout action method logging out user");

        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Settings()
    {
        _logger.LogDebug("Settings action method returns SettingsView");
        IEnumerable<CountryResponse> countries = await _countryGetterService.GetAllCountries();
        ViewBag.Countries = new SelectList(countries, "Id", "Name");
        return View();
    }
}
