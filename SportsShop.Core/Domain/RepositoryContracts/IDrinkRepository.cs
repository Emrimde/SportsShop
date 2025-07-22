using SportsShop.Core.Domain.Models;

namespace RepositoryContracts;
public interface IDrinkRepository
{
    Task<IEnumerable<Drink>> GetAllDrinks();
    Task<Drink?> GetDrinkById(int id);
    IQueryable<Drink> FilterDrinks(string flavor);
}
