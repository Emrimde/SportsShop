using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsShop.Models;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class DrinksController : Controller
    {
        private readonly SportsShopDbContext DatabaseContext;
        public DrinksController(SportsShopDbContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }
        public IActionResult Index()
        {
            List<Drink> drinks = DatabaseContext.Drinks.Include(item => item.Product).Where(item=>item.Product.IsActive).ToList();
            List<DrinksViewModel> drinksViewModels = new List<DrinksViewModel>();
            foreach (Drink drink in drinks)
            {
                drinksViewModels.Add(new DrinksViewModel()
                {
                    Code = drink.Product.Code ?? "Code hidden",
                    Description = drink.Product.Description,
                    Name = drink.Product.Name,
                    Price = drink.Product.Price,
                    Producer = drink.Product.Producer,
                    Volume = drink.Volume,
                    VolumeUnit = drink.VolumeUnit,
                    ImagePath = drink.ImagePath,
                    Flavor = drink.Flavor

                });
            }

            return View(drinksViewModels);
        }
    }
}
