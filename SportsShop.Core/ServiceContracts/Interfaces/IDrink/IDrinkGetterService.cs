using SportsShop.Core.ServiceContracts.DTO.DrinkDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.IDrink;
public interface IDrinkGetterService
{
    Task<IEnumerable<DrinkResponse>> GetAllDrinks();
    Task<DrinkResponse> GetDrinkById(int id);
    Task<List<DrinkResponse>> FilterDrinks(string flavor);
}
