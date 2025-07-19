using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly SportsShopDbContext _context;

        public SupplierRepository(SportsShopDbContext context)
        {
            _context = context;
        }   

        public IQueryable<Supplier> GetAllSuppliers()
        {
            return _context.Suppliers.Where(item => item.IsActive).AsQueryable();
        }

        public async Task<decimal> GetSupplierPriceById(int id)
        {
            return await _context.Suppliers.Where(item => item.Id == id).Select(item => item.Price)
                 .FirstOrDefaultAsync();
        }
    }
}
