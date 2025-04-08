using Entities.Models;

namespace ServiceContracts
{
    public interface IDrinksService
    {
        List<Drink> GetDrinks();
    }
}
