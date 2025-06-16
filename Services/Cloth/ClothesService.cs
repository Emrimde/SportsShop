
using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Interfaces;
using ServiceContracts.Interfaces.ICloth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class ClothGetterService : IClothGetterService
    {
        private readonly SportsShopDbContext _context;

        public ClothGetterService(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cloth>> FilterCloth(string size, string gender, string type)
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
            return await clothes.ToListAsync();
        }

        public async Task<List<Cloth>> GetAllClothes()
        {
            return await _context.Clothes.Include(item => item.Product).Where(item => item.Product.IsActive).ToListAsync();
        }

        public async Task<Cloth?> GetCloth(int id)
        {
            return await _context.Clothes.Include(item => item.Product).FirstOrDefaultAsync(item => item.ProductId == id && item.Product.IsActive);
        }
    }
}
