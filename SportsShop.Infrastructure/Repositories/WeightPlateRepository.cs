using Microsoft.EntityFrameworkCore;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Infrastructure.DatabaseContext;

namespace SportsShop.Infrastructure.Repositories;
public class WeightPlateRepository : IWeightPlateRepository
{
    private readonly SportsShopDbContext _context;

    public WeightPlateRepository(SportsShopDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WeightPlate>> GetAllWeightPlates()
    {
        return await _context.WeightPlates.Include(item => item.Product).Where(item => item.Product.IsActive).ToListAsync();
    }

    public async Task<WeightPlate?> GetWeightPlateById(int id)
    {
       return await _context.WeightPlates.Include(item => item.Product).FirstOrDefaultAsync(item => item.Product.Id == id && item.Product.IsActive);
    }
}
