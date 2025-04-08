using Entities.Models;

namespace ServiceContracts.Interfaces
{
    public interface IDrinksService
    {
        List<Drink> GetDrinks();
    }
}
