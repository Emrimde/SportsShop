using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Interfaces;


namespace Services
{
    public class SupplementsService : ISupplementsService
    {
        private readonly SportsShopDbContext _context;

        public SupplementsService(SportsShopDbContext context)
        {
            _context = context;
        }
        public async Task<List<Supplement>> GetAllSupplements()
        {
            return await _context.Supplements.Include(item => item.Product).Where(item=> item.Product.IsActive).ToListAsync();
        }

        public async Task<Supplement> GetSupplement(int id)
        {
            Supplement? supplement = await _context.Supplements.Include(item=>item.Product).FirstOrDefaultAsync(item => item.ProductId == id && item.Product.IsActive);
            if (supplement == null)
            {
                return null!;
            }
            return supplement;
        }
    }
}
