﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using SportsShop.DTO;
using SportsShop.Models;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly SportsShopDbContext DatabaseContext;
        private readonly PasswordHasher<User> passwordHasher;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(SportsShopDbContext databaseContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            DatabaseContext = databaseContext;
            passwordHasher = new PasswordHasher<User>();
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: true);

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
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                UserName = model.FirstName + " " + model.LastName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);


                var cart = new Cart
                {
                    UserId = user.Id,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };
                DatabaseContext.Carts.Add(cart);
                await DatabaseContext.SaveChangesAsync();
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
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            Address address = new Address
            {
                UserId = user.Id,
                Country = model.Country,
                City = model.City,
                Street = model.Street,
                ZipCode = model.ZipCode,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            DatabaseContext.Addresses.Add(address);
            await DatabaseContext.SaveChangesAsync();
            return RedirectToAction("ShowAddresses");
        }
        
        public async Task<IActionResult> ShowAddresses()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            List<Address> addresses = await DatabaseContext.Addresses.Where(item => item.UserId == user.Id && item.IsActive).ToListAsync();
            List<AddressViewModel> addressesViewModel = new List<AddressViewModel>();
            foreach (Address address in addresses)
            {
                addressesViewModel.Add(new AddressViewModel
                {
                    Id = address.Id,
                    Country = address.Country,
                    City = address.City,
                    Street = address.Street,
                    ZipCode = address.ZipCode
                });
            }

            return View(addressesViewModel);
        }

        
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            Address? address = await DatabaseContext.Addresses.FirstOrDefaultAsync(item => item.Id == id && item.UserId == user.Id);
            if (address == null)
            {
                return NotFound();
            }
            address.IsActive = false;
            address.DeleteDate = DateTime.Now;
            await DatabaseContext.SaveChangesAsync();
            return RedirectToAction("ShowAddresses");
        }

        
        public async Task<IActionResult> EditAddress(int id)
        {
            Address? address = await DatabaseContext.Addresses.FirstOrDefaultAsync(item => item.Id == id);
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
            Address? address = await DatabaseContext.Addresses.FirstOrDefaultAsync(a =>  a.UserId == user.Id && a.Id == addressViewModel.Id );

            if (address == null)
            {
                return NotFound();
            }
            address.Country = addressViewModel.Country;
            address.City = addressViewModel.City;
            address.Street = addressViewModel.Street;
            address.ZipCode = addressViewModel.ZipCode;
            address.EditDate = DateTime.Now;

            DatabaseContext.Update(address);
            await DatabaseContext.SaveChangesAsync();

            return RedirectToAction("ShowAddresses");
        }
    }
}
