
using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.ClothDto;
using ServiceContracts.Interfaces.ICloth;

namespace Services
{
    public class ClothGetterService : IClothGetterService
    {
        private readonly SportsShopDbContext _context;

        public ClothGetterService(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClothResponse>> FilterClothes(string size, string gender, string type)
        {
            IQueryable<Cloth> clothes = _context.Clothes.Include(item => item.Product).Where(item => item.Product.IsActive).AsQueryable();

            if (gender != "select")
            {
                clothes = clothes.Where(item => item.Gender == gender);
            }
            if (size != "select")
            {
                clothes = clothes.Where(item => item.Size == size);
            }
            if (type != "select")
            {
                clothes = clothes.Where(item => item.Type == type);
            }
            List<ClothResponse> clothResponses =await clothes.Select(item => item.ToClothResponse()).ToListAsync();

            return clothResponses;
        }

        public async Task<List<ClothResponse>> GetAllClothes()
        {
            return await _context.Clothes.Include(item => item.Product).Where(item => item.Product.IsActive).Select(item => item.ToClothResponse()).ToListAsync();
        }

        public async Task<ClothResponse?> GetClothById(int id)
        {
            return await _context.Clothes.Include(item => item.Product).Where(item => item.ProductId == id && item.Product.IsActive).Select(item => item.ToClothResponse()).FirstOrDefaultAsync();
        }
    }
}
