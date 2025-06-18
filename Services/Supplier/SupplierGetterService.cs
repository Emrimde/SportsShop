using Entities.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.SupplierDto;
using ServiceContracts.Interfaces.ISupplier;


namespace Services
{
    public class SupplierGetterService : ISupplierGetterService
    {
        private readonly SportsShopDbContext _context;

        public SupplierGetterService(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetSupplierPriceById(int id)
        {
            return await _context.Suppliers.Where(item => item.Id == id).Select(item => item.Price)
                .FirstOrDefaultAsync();
        }

        public async Task<List<SupplierResponse>> GetAllSuppliers()
        {
            return await _context.Suppliers.Where(item => item.IsActive).Select(item => item.ToSupplierResponse()).ToListAsync();
        }
    }
}
