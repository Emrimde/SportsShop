using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class GymnasticRingRepository : IGymnasticRingRepository
    {
        private readonly SportsShopDbContext _context;

        public GymnasticRingRepository(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GymnasticRing>> GetAllGymnasticRings()
        {
            return await _context.GymnasticRings.Include(item => item.Product).Where(item => item.Product.IsActive).ToListAsync();
        }

        public async Task<GymnasticRing?> GetGymnasticRingById(int id)
        {
            return await _context.GymnasticRings.Include(item => item.Product).Where(item => item.Product.IsActive && item.ProductId == id).FirstOrDefaultAsync();
        }
    }
}
