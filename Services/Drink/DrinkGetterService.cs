using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.DrinkDto;
using ServiceContracts.Interfaces.IDrink;

namespace Services
{
    public class DrinkGetterService : IDrinkGetterService
    {
        private readonly SportsShopDbContext _context;

        public DrinkGetterService(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<DrinkResponse>> FilterDrinks(string flavor)
        {
            IQueryable<DrinkResponse> drinks = _context.Drinks.Include(item => item.Product).Where(item => item.Product.IsActive).Select(item => item.ToDrinkResponse());
            if (flavor != "select")
            {
                drinks = drinks.Where(item => item.Flavor == flavor);
            }
            return await drinks.ToListAsync();
        }

        public async Task<DrinkResponse> GetDrinkById(int id)
        {
            Drink? drink = await _context.Drinks.Include(item => item.Product).FirstOrDefaultAsync(item => item.ProductId == id && item.Product.IsActive);

            if (drink == null)
            {
                return null!;
            }

            return drink.ToDrinkResponse();
        }

        public async Task<List<DrinkResponse>> GetAllDrinks()
        {
            return await _context.Drinks.Include(item => item.Product).Where(item => item.Product.IsActive).Select(item => item.ToDrinkResponse()).ToListAsync();
        }
    }
}
