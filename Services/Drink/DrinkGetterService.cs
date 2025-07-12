using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts.DTO.DrinkDto;
using ServiceContracts.Interfaces.IDrink;

namespace Services
{
    public class DrinkGetterService : IDrinkGetterService
    {
        private readonly IDrinkRepository _drinkRepository;
        public DrinkGetterService(IDrinkRepository drinkRepository)
        {
            _drinkRepository = drinkRepository;
        }

        public async Task<List<DrinkResponse>> FilterDrinks(string flavor)
        {
            IQueryable<DrinkResponse> drinks = _drinkRepository.FilterDrinks(flavor).Select(item => item.ToDrinkResponse());
            if (flavor != "select")
            {
                drinks = drinks.Where(item => item.Flavor == flavor);
            }
            return await drinks.ToListAsync();
        }

        public async Task<DrinkResponse> GetDrinkById(int id)
        {
            Drink? drink = await _drinkRepository.GetDrinkById(id);

            if (drink == null)
            {
                return null!;
            }

            return drink.ToDrinkResponse();
        }

        public List<DrinkResponse> GetAllDrinks()
        {
            return _drinkRepository.GetAllDrinks().Select(item => item.ToDrinkResponse()).ToList();
        }
    }
}
