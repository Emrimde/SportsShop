using Microsoft.AspNetCore.Mvc;
using SportsShop.Core.ServiceContracts.DTO.DrinkDto;
using SportsShop.Core.ServiceContracts.Interfaces.IDrink;

namespace SportsShop.UI.Controllers;
    public class DrinksController : Controller
    {
        private readonly IDrinkGetterService _drinkGetterService;
        private readonly ILogger<DrinksController> _logger;
        public DrinksController(IDrinkGetterService drinkGetterService, ILogger<DrinksController> logger)
        {
            _drinkGetterService = drinkGetterService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("Index action method. Displays all drinks");
            IEnumerable<DrinkResponse> drinks = await _drinkGetterService.GetAllDrinks();

            return View(drinks);
        }
        public async Task<IActionResult> ShowDrink(int id)
        {
            _logger.LogDebug("ShowDrink action method. Parameter: id: {id}", id);
            
            DrinkResponse? drink = await _drinkGetterService.GetDrinkById(id);

            if (drink == null)
            {
                _logger.LogError("Drink not found. ShowDrink action");
                return NotFound();
            }

            return View(drink);
        }

        public async Task<IActionResult> FilterDrink(string flavor)
        {
            _logger.LogDebug("FilterDrink action method. Parameter: flavor: {flavor}", flavor);

            List<DrinkResponse> drinks = await _drinkGetterService.FilterDrinks(flavor);
            return View("Index", drinks);
        }
    }

