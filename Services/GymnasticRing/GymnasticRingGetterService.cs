using Entities.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.GymnasticRingDto;
using ServiceContracts.Interfaces.IGymnasticRing;

namespace Services.GymnasticRing
{
    public class GymnasticRingGetterService : IGymnasticRingGetterService
    {
        private readonly SportsShopDbContext _context;

        public GymnasticRingGetterService(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<GymnasticRingResponse>> GetAllGymnasticRings()
        {
            return await _context.GymnasticRings.Include(item => item.Product).Where(item => item.Product.IsActive).Select(item => item.ToGymnasticResponse()).ToListAsync();
        }

        public async Task<GymnasticRingResponse?> GetGymnasticRingById(int id)
        {
            return await _context.GymnasticRings.Include(item => item.Product).Where(item => item.Product.IsActive && item.ProductId == id).Select(item => item.ToGymnasticResponse()).FirstOrDefaultAsync();
        }
    }
}
