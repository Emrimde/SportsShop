using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.DrinkDto;
using ServiceContracts.Interfaces.IDrink;

namespace SportsShop.Controllers
{
    public class DrinksController : Controller
    {
        private readonly IDrinkGetterService _drinkGetterService;
        public DrinksController(IDrinkGetterService drinkGetterService)
        {
            _drinkGetterService = drinkGetterService;
        }
        public IActionResult Index()
        {
            List<DrinkResponse> drinks = _drinkGetterService.GetAllDrinks();
            return View(drinks);
        }
        public async Task<IActionResult> ShowDrink(int id)
        {
            DrinkResponse? drink = await _drinkGetterService.GetDrinkById(id);

            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        public async Task<IActionResult> FilterDrink(string flavor)
        {
            List<DrinkResponse> drinks = await _drinkGetterService.FilterDrinks(flavor);
            return View("Index", drinks);
        }
    }
}
