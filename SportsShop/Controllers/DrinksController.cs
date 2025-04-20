using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Interfaces;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class DrinksController : Controller
    {
        private readonly IDrinksService _drinksService;
        public DrinksController(IDrinksService drinksService)
        {
            _drinksService = drinksService;
        }
        public async Task<IActionResult> Index()
        {
            List<Drink> drinks =await _drinksService.GetDrinks();
            List<DrinksViewModel> drinksViewModels = new List<DrinksViewModel>();
            foreach (Drink drink in drinks)
            {
                drinksViewModels.Add(new DrinksViewModel()
                {
                    Id = drink.ProductId,
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
        public async Task<IActionResult> ShowDrink(int id)
        {
            Drink? drink = await _drinksService.GetDrink(id);
            if (drink == null)
            {
                return NotFound();
            }
            DrinksViewModel drinkViewModel = new DrinksViewModel()
            {
                Id = drink.ProductId,
                Description = drink.Product.Description,
                Name = drink.Product.Name,
                Price = drink.Product.Price,
                Producer = drink.Product.Producer,
                Volume = drink.Volume,
                VolumeUnit = drink.VolumeUnit,
                ImagePath = drink.ImagePath,
                Flavor = drink.Flavor
            };
            return View(drinkViewModel);
        }

        public async Task<IActionResult> FilterDrink(string flavor)
        {
            List<Drink> drinks = await _drinksService.FilterDrink(flavor);
            List<DrinksViewModel> drinksViewModels = new List<DrinksViewModel>();
            foreach (Drink drink in drinks)
            {
                drinksViewModels.Add(new DrinksViewModel()
                {
                    Id = drink.ProductId,
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
                return View("Index", drinksViewModels);
        }
    }
}
