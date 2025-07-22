using Microsoft.EntityFrameworkCore;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Infrastructure.DatabaseContext;

namespace SportsShop.Infrastructure.Repositories;
public class ClothRepository : IClothRepository
{
    private readonly SportsShopDbContext _context;

    public ClothRepository(SportsShopDbContext context)
    {
        _context = context;
    }

    public IQueryable<Cloth> FilterClothes(string size, string gender, string type)
    {
        return _context.Clothes.Include(item => item.Product).Where(item => item.Product.IsActive).AsQueryable();
    }

    public async Task<IEnumerable<Cloth>> GetAllClothes()
    {
        IEnumerable<Cloth> clothes = await _context.Clothes.AsNoTracking()
            .Include(item => item.Product)
            .Where(item => item.Product.IsActive)
            .ToListAsync();

        return clothes;
    }

    public async Task<Cloth?> GetClothById(int id)
    {
        return await _context.Clothes.Include(item => item.Product).FirstOrDefaultAsync(item => item.Product.IsActive && item.ProductId == id);
    }
}
