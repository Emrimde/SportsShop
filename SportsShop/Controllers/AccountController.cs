using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using SportsShop.Models;
using SportsShop.ViewModels;
using Entities.Models;
using Entities.DatabaseContext;
using ServiceContracts.DTO;
using ServiceContracts.Interfaces;
using System.Security.Claims;
using System.Net;



namespace SportsShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly SportsShopDbContext DatabaseContext;
        private readonly PasswordHasher<User> passwordHasher;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountService _accountService;
        public AccountController(SportsShopDbContext databaseContext, UserManager<User> userManager, SignInManager<User> signInManager, IAccountService accountService)
        {
            DatabaseContext = databaseContext;
            passwordHasher = new PasswordHasher<User>();
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDTO model)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(model);
            }

            var result = await _accountService.SignInAsync(model);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Login", "Invalid email or password");
            return View(model);
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IdentityResult result = await _accountService.RegisterAsync(model);
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
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Settings()
        {
            return View();
        }

        public async Task<IActionResult> AddAddress(AddressDTO model)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _accountService.AddAddress(model, user);

            if (result)
            {
                return RedirectToAction("ShowAddresses");

            }

            return Unauthorized();
        }

        public async Task<IActionResult> ShowAddresses()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            List<Address> addresses = await _accountService.ShowAddresses(user.Id);
            List<AddressViewModel> addressesViewModel = addresses.Select(item => new AddressViewModel()
            {
                Id = item.Id,
                Country = item.Country,
                City = item.City,
                Street = item.Street,
                ZipCode = item.ZipCode
            }).ToList();

            return View(addressesViewModel);
        }


        public async Task<IActionResult> DeleteAddress(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            bool result = await _accountService.DeleteAddress(id);
            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("ShowAddresses");
        }


        public async Task<IActionResult> EditAddress(int id)
        {
            Address? address = await _accountService.GetAddress(id);
            if (address == null)
            {
                return NotFound();
            }
            AddressViewModel addressViewModel = new AddressViewModel
            {
                Id = address.Id,
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                ZipCode = address.ZipCode
            };
            return View("EditAddress", addressViewModel);
        }

        public async Task<IActionResult> EditAddresss(AddressViewModel addressViewModel)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            Address? address = await _accountService.GetAddress(addressViewModel.Id);

            if (address == null)
            {
                return NotFound();
            }
            await _accountService.EditAddress(new AddressDTO
            {
                Id = addressViewModel.Id,
                Country = addressViewModel.Country,
                City = addressViewModel.City,
                Street = addressViewModel.Street,
                ZipCode = addressViewModel.ZipCode
            });

            return RedirectToAction("ShowAddresses");
        }
    }
}
