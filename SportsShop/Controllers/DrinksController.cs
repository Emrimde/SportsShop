using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsShop.Models;
using SportsShop.ViewModels;
using Entities.Models;
using Entities.DatabaseContext;
using ServiceContracts.Interfaces;

namespace SportsShop.Controllers
{
    public class DrinksController : Controller
    {
        private readonly IDrinksService _drinksService;
        public DrinksController(IDrinksService drinksService)
        {
            _drinksService = drinksService;
        }
        public IActionResult Index()
        {
            List<Drink> drinks = _drinksService.GetDrinks();
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
