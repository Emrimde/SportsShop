using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
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

        public IQueryable<Cloth> GetAllClothes()
        {
           return _context.Clothes.Include(item => item.Product)
                .Where(item => item.Product.IsActive)
                .AsQueryable(); 
        }

        public async Task<Cloth?> GetClothById(int id)
        {
            return await _context.Clothes.Include(item => item.Product).FirstOrDefaultAsync(item => item.Product.IsActive && item.ProductId == id);
        }
    }
}
