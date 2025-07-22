using Microsoft.EntityFrameworkCore;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Infrastructure.DatabaseContext;

namespace SportsShop.Infrastructure.Repositories;
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
