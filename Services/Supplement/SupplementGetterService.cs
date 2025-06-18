using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.SupplementDto;
using ServiceContracts.Interfaces.ISupplement;

namespace Services
{
    public class SupplementGetterService : ISupplementGetterService
    {
        private readonly SportsShopDbContext _context;

        public SupplementGetterService(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<SupplementResponse>> GetAllSupplements()
        {
            return await _context.Supplements.Include(item => item.Product).Where(item => item.Product.IsActive).Select(item => item.ToSupplementResponse()).ToListAsync();
        }

        public async Task<SupplementResponse> GetSupplementById(int id)
        {
            Supplement? supplement = await _context.Supplements.Include(item => item.Product).FirstOrDefaultAsync(item => item.ProductId == id && item.Product.IsActive);

            if (supplement == null)
            {
                return null!;
            }

            return supplement.ToSupplementResponse();
        }

        public async Task<List<SupplementResponse>> FilterSupplements(string type, string flavor)
        {
            IQueryable<SupplementResponse> supplements = _context.Supplements.Include(item => item.Product).Where(item => item.Product.IsActive).Select(item => item.ToSupplementResponse());

            if (type != "select")
            {
                supplements = supplements.Where(item => item.Type == type);
            }

            if (flavor != "select")
            {
                supplements = supplements.Where(item => item.Flavor == flavor);
            }

            return await supplements.ToListAsync();
        }
    }
}
