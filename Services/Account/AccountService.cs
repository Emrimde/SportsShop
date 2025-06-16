using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using ServiceContracts.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Interfaces.Account;



namespace Services.Account
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


        public async Task<IdentityResult> RegisterAsync(RegisterDTO model)
        {
            var user = new User
            {
                UserName = model.FirstName,
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
