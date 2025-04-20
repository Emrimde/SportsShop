using SportsShop.Models;
using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Interfaces;
namespace Services
{
    public class DrinksService : IDrinksService
    {
        private readonly SportsShopDbContext _context;

        public DrinksService(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Drink>> FilterDrink(string flavor)
        {
            IQueryable<Drink> drinks = _context.Drinks.Include(item => item.Product).Where(item => item.Product.IsActive);
            if(flavor != "select")
            {
                drinks = drinks.Where(item => item.Flavor == flavor);
            }
            return await drinks.ToListAsync();
        }

        public async Task<Drink> GetDrink(int id)
        {
            Drink? drink = await _context.Drinks.Include(item => item.Product).FirstOrDefaultAsync(item => item.ProductId == id && item.Product.IsActive);
            if (drink == null)
            {
                return null!;
            }
            return drink;
        }

        public async Task<List<Drink>> GetDrinks()
        {
            return await _context.Drinks.Include(item => item.Product).Where(item => item.Product.IsActive).ToListAsync();
        }
    }
}
