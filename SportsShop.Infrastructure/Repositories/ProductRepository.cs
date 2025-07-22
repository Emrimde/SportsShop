using Microsoft.EntityFrameworkCore;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Infrastructure.DatabaseContext;

namespace SportsShop.Infrastructure.Repositories;
    public class ProductRepository : IProductRepository
    {
        private readonly SportsShopDbContext _context;
        public ProductRepository(SportsShopDbContext context)
        {
            _context = context;
        }
        public async Task<Product?> GetProductById(int id)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(item => item.Id == id && item.IsActive);

            return product;
        }

    public Task UpdateProductQuantity(List<Product> products)
    {
        throw new NotImplementedException();
    }
}

