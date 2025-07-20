using ServiceContracts.DTO.DrinkDto;

namespace ServiceContracts.Interfaces.IDrink
{
    public interface IDrinkGetterService
    {
        Task<IEnumerable<DrinkResponse>> GetAllDrinks();
        Task<DrinkResponse> GetDrinkById(int id);
        Task<List<DrinkResponse>> FilterDrinks(string flavor);
    }
}
