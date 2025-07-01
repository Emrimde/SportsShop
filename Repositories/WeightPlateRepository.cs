using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class WeightPlateRepository : IWeightPlateRepository
    {
        private readonly SportsShopDbContext _context;

        public WeightPlateRepository(SportsShopDbContext context)
        {
            _context = context;
        }

        public IQueryable<WeightPlate> GetAllWeightPlates()
        {
            return _context.WeightPlates.Include(item => item.Product).Where(item => item.Product.IsActive).AsQueryable();
        }

        public async Task<WeightPlate?> GetWeightPlateById(int id)
        {
           return await _context.WeightPlates.Include(item => item.Product).FirstOrDefaultAsync(item => item.Product.Id == id && item.Product.IsActive);
        }
    }
}
