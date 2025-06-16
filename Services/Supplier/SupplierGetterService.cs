using ServiceContracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entities.DatabaseContext;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Supplier>> GetAllSuppliers()
        {
            return await _context.Suppliers.Where(item => item.IsActive).ToListAsync();
        }

        public async Task<decimal> GetSupplierPriceById(int id)
        {
            return await _context.Suppliers.Where(item => item.Id == id).Select(item => item.Price)
                .FirstOrDefaultAsync();
        }
    }
}
