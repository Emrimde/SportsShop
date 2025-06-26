using Entities.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.WeightPlateDto;
using ServiceContracts.Interfaces.IWeightPlate;

namespace Services.WeightPlate
{
    public class WeightPlateGetterService : IWeightPlateGetterService
    {
        private readonly SportsShopDbContext _context;

        public WeightPlateGetterService(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<WeightPlateResponse>> GetAllWeightPlates()
        {
            return await _context.WeightPlates.Include(item => item.Product).Where(item => item.Product.IsActive).Select(item => item.ToWeightPlateResponse()).ToListAsync();
        }

        public async Task<WeightPlateResponse?> GetWeightPlateById(int id)
        {
            return await _context.WeightPlates.Include(item => item.Product).Where(item => item.Product.IsActive && item.ProductId == id).Select(item => item.ToWeightPlateResponse()).FirstOrDefaultAsync();
        }
    }
}
