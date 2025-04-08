using SportsShop.Models;
using ServiceContracts;
using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
namespace Services
{
    public class DrinksService : IDrinksService
    {
        private readonly SportsShopDbContext _context;

        public DrinksService(SportsShopDbContext context)
        {
            _context = context;
        }

        public List<Drink> GetDrinks()
        {
            return _context.Drinks.Include(item => item.Product).Where(item => item.Product.IsActive).ToList();
        }
    }
}
