using Entities.Models;

namespace RepositoryContracts
{
    public interface IDrinkRepository
    {
        IQueryable<Drink> GetAllDrinks();
        Task<Drink?> GetDrinkById(int id);
        IQueryable<Drink> FilterDrinks(string flavor);
    }
}
