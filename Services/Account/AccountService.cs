using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using ServiceContracts.DTO.AccountDto;
using ServiceContracts.Interfaces.Account;

namespace Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly SportsShopDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        
        public AccountService(SportsShopDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.FirstName,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
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

        public async Task<SignInResult> SignInAsync(SignInDto signInDto)
        {
            var result = await _signInManager.PasswordSignInAsync(signInDto.Email, signInDto.Password, isPersistent: false, lockoutOnFailure: true);
            return result;
        }
    }
}
