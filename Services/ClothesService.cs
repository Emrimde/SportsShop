
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
    public class ClothesService : IClothesService
    {
        private readonly SportsShopDbContext _context;

        public ClothesService(SportsShopDbContext context)
        {
            _context = context;
        } 
        public async Task<List<Cloth>> GetAllClothes()
        {
           return await _context.Clothes.Include(item => item.Product).Where(item => item.Product.IsActive).ToListAsync();
        }

        public async Task<Cloth?> GetCloth(int id)
        {
           return await _context.Clothes.Include(item=> item.Product).FirstOrDefaultAsync(item => item.ProductId == id && item.Product.IsActive);
        }
    }
}
