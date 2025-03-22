using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using SportsShop.Models;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly SportsShopDbContext DatabaseContext;
        private readonly PasswordHasher<User> passwordHasher;
        public AccountController(SportsShopDbContext databaseContext)
        {
            DatabaseContext = databaseContext;
            passwordHasher = new PasswordHasher<User>();

        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = await DatabaseContext.Users.Where(item => item.IsActive && item.Email == model.Email).FirstOrDefaultAsync();
                if (user != null)
                {
                    var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
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
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = passwordHasher.HashPassword(null!, model.Password),
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            // Dodajemy użytkownika do bazy danych
            DatabaseContext.Users.Add(user);
            await DatabaseContext.SaveChangesAsync();

            // Tworzymy Cart po dodaniu Usera
            var cart = new Cart
            {
                UserId = user.Id, // Upewnij się, że UserId jest poprawnie przypisane
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            DatabaseContext.Carts.Add(cart);
            await DatabaseContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }



    }
}
