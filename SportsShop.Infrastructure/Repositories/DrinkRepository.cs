using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly SportsShopDbContext _context;

        public DrinkRepository(SportsShopDbContext context)
        {
            _context = context;
        }

        public IQueryable<Drink> FilterDrinks(string flavor)
        {
            return _context.Drinks.Include(item => item.Product).Where(item => item.Product.IsActive).AsQueryable();
        }

        public async Task<IEnumerable<Drink>> GetAllDrinks()
        {
            IEnumerable<Drink> drinks = await _context.Drinks.AsNoTracking()
            .Include(item => item.Product)
            .Where(item => item.Product.IsActive)
            .ToListAsync();

            return drinks;
        }

        public async Task<Drink?> GetDrinkById(int id)
        {
            return await _context.Drinks.Include(item => item.Product).FirstOrDefaultAsync(item => item.ProductId == id && item.Product.IsActive);
        }
    }
}
