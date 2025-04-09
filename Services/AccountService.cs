using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using ServiceContracts.Interfaces;
using ServiceContracts.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;



namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly SportsShopDbContext _context;
        private readonly PasswordHasher<User> passwordHasher;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AccountService(SportsShopDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            passwordHasher = new PasswordHasher<User>();
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //public async Task<bool> AddAddress(AddressDTO model, string UserId)
        //{
        //    User? user = await _userManager.FindByIdAsync(UserId.ToString());

        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    Address address = new Address
        //    {
        //        UserId = user.Id,
        //        Country = model.Country,
        //        City = model.City,
        //        Street = model.Street,
        //        ZipCode = model.ZipCode,
        //        CreatedDate = DateTime.Now,
        //        IsActive = true
        //    };

        //    _context.Addresses.Add(address);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<bool> DeleteAddress(int id)
        //{
        //    Address? address = await _context.Addresses.FindAsync(id);
        //    if (address == null)
        //    {
        //        return false;
        //    }
        //    address.IsActive = false;
        //    address.DeleteDate = DateTime.UtcNow;
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<bool> EditAddress(AddressDTO model)
        //{
        //    Address? address = await GetAddress(model.Id);
        //    if (address == null)
        //    {
        //        return false;
        //    }
        //    address.Country = model.Country;
        //    address.City = model.City;
        //    address.Street = model.Street;
        //    address.ZipCode = model.ZipCode;
        //    address.EditDate = DateTime.UtcNow;
        //    _context.Addresses.Update(address);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<Address> GetAddress(int id)
        //{
        //    Address? address = await _context.Addresses.FirstOrDefaultAsync(item => item.Id == id);
        //    if (address == null)
        //    {
        //        return null!;
        //    }
        //    return address;


        //}

        public async Task<IdentityResult> RegisterAsync(RegisterDTO model)
        {
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
            if (!result.Succeeded)
            {
                return result;
            }

            await _signInManager.SignInAsync(user, isPersistent: true);
            var cart = new Cart
            {
                UserId = user.Id,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return result;
        }

       

        public async Task<SignInResult> SignInAsync(SignInDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: true);
            return result;
        }
    }
}
