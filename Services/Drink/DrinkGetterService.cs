using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.DrinkDto;
using ServiceContracts.Interfaces.IDrink;

namespace Services
{
    public class DrinkGetterService : IDrinkGetterService
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly ILogger<DrinkGetterService> _logger;

        public DrinkGetterService(IDrinkRepository drinkRepository, ILogger<DrinkGetterService> logger)
        {
            _drinkRepository = drinkRepository;
            _logger = logger;
        }

        public async Task<List<DrinkResponse>> FilterDrinks(string flavor)
        {
            _logger.LogDebug("FilterDrinks service method. Parameter: flavor: {flavor}", flavor);

            IQueryable<DrinkResponse> drinks = _drinkRepository.FilterDrinks(flavor).Select(item => item.ToDrinkResponse());
            if (flavor != "select")
            {
                drinks = drinks.Where(item => item.Flavor == flavor);
            }
            return await drinks.ToListAsync();
        }

        public async Task<DrinkResponse> GetDrinkById(int id)
        {
            _logger.LogDebug("GetDrinkById service method. Parameter: id: {id}", id);

            Drink? drink = await _drinkRepository.GetDrinkById(id);

            if (drink == null)
            {
                return null!;
            }

            return drink.ToDrinkResponse();
        }

        public List<DrinkResponse> GetAllDrinks()
        {
            _logger.LogDebug("GetAllDrinks service method");
            return _drinkRepository.GetAllDrinks().Select(item => item.ToDrinkResponse()).ToList();
        }
    }
}
