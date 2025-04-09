using Entities.Models;

namespace ServiceContracts.Interfaces
{
    public interface IDrinksService
    {
        Task<List<Drink>> GetDrinks();
        Task<Drink> GetDrink(int id);
    }
}
