using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using SportsShop.Models;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly SportsShopDbContext DatabaseContext;
        private readonly PasswordHasher<User> passwordHasher;
        public UsersController(SportsShopDbContext databaseContext)
        {
            DatabaseContext = databaseContext;
            passwordHasher = new PasswordHasher<User>();

        }
        public IActionResult SignIn()
        {
            return View();
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


        //[HttpPost]
        //public async Task<IActionResult> CreateUser([Bind("Id,Email,FirstName,LastName")] User user, string Password)
        //// string password - nazwa taka jak w formularzu
        //// zeby hashowac trzeba miec obiekt klasy PasswordHasher<> typ generyczyny odpowiada na pytanie gdzie jest rzecz 
        //// ktora chcemy zahashowac

        //{
        //    if (ModelState.IsValid) //is valid sprawdza czy wszystkie pola z Bind sa wypelnione. te pola
        //                            // to nazwy inputow z formularza
        //    {
        //        user.PasswordHash = passwordHasher.HashPassword(user, Password);// hashujemy
        //        // pod polem Password Hash przypisujemy zahashowane haslo
        //        user.CreatedDate = DateTime.Now;
        //        user.IsActive = true;
        //        user.Cart = new Cart
        //        {
        //            UserId = user.Id,
        //            IsActive = true,
        //            CreatedDate = DateTime.Now
        //        };
        //        DatabaseContext.Users.Add(user); // user to model ktory jest przekazywany z formularza
        //        await DatabaseContext.SaveChangesAsync(); // wykonuje zapytanie sql

        //        DatabaseContext.Carts.Add(user.Cart);
        //        await DatabaseContext.SaveChangesAsync();

        //        return RedirectToAction("Index", "Home"); // przekierowuje na strone glowna jesli sie udalo

        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        foreach (var modelError in ModelState)
        //        {
        //            foreach (var error in modelError.Value.Errors)
        //            {
        //                Console.WriteLine($"Pole: {modelError.Key}, Błąd: {error.ErrorMessage}");
        //            }
        //        }
        //    }
        //    return View();// zwracam widok ten sam co byl
        //}
    }
}
