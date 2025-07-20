using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories;
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

