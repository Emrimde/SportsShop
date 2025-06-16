using Entities.Models;

namespace ServiceContracts.Interfaces.IDrink
{
    public interface IDrinkGetterService
    {
        Task<List<Drink>> GetDrinks();
        Task<Drink> GetDrink(int id);
        Task<List<Drink>> FilterDrink(string flavor);
    }
}
