using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly SportsShopDbContext _context;
        public CountryRepository(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CountryExists(int countryId)
        {
            return await _context.Countries.AnyAsync(item => item.Id == countryId);
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _context.Countries.AsNoTracking().ToListAsync();
        }
    }
}
