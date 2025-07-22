using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.ServiceContracts.DTO.DrinkDto;
using SportsShop.Core.ServiceContracts.Interfaces.IDrink;

namespace SportsShop.Core.Services.DrinkServices;
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

    public async Task<IEnumerable<DrinkResponse>> GetAllDrinks()
    {
        IEnumerable<Drink> drinks = await _drinkRepository.GetAllDrinks();
        return drinks.Select(item => item.ToDrinkResponse());
    }
}
