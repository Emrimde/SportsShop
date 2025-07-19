using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SportsShopDbContext _context;

        public OrderRepository(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddOrder(Order model, int cartId)
        {
            model.IsActive = true;
            model.CreatedDate = DateTime.Now;
            foreach (var item in model.CartItems)
            {
                
                Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException("Product don't exist");
                }
                if (product.Quantity < item.Quantity)
                {
                    throw new InvalidOperationException($"Not enough stock for product ID {product.Id}");
                }

                _context.Orders.Add(model);
                product.Quantity -= item.Quantity;
            }
                await _context.SaveChangesAsync();

            return model;
        }

        public async Task<IEnumerable<Order>> GetAllOrders(Guid userId)
        {
            IEnumerable<Order> orders = 
           await _context.Orders.Include(item => item.CartItems)
               .ThenInclude(ci => ci.Product).Where(item => item.UserId == userId && item.IsActive).ToListAsync();
            return orders;
        }
    }
}
