using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
