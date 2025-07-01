using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class SupplementRepository : ISupplementRepository
    {
        private readonly SportsShopDbContext _context;

        public SupplementRepository(SportsShopDbContext context)
        {
            _context = context;
        }

        //public IQueryable<Supplement> FilterSupplements(string type, string flavor)
        //{
        //    throw new NotImplementedException();
        //}

        public IQueryable<Supplement> GetAllSupplements()
        {
            return _context.Supplements.Include(item => item.Product).Where(item => item.Product.IsActive).AsQueryable();
        }

        public async Task<Supplement?> GetSupplementById(int id)
        {
            return await _context.Supplements.Include(item => item.Product).FirstOrDefaultAsync(item => item.ProductId == id && item.Product.IsActive);
        }
    }
}
