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

        public async Task<Order> AddOrder(Order model)
        {
            model.IsActive = true;
            model.CreatedDate = DateTime.Now;
            _context.Orders.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public IQueryable<Order> GetAllOrders(string id)
        {
            return _context.Orders.Include(item => item.User).Include(o => o.CartItems)
               .ThenInclude(ci => ci.Product).Where(item => item.UserId.ToString() == id && item.IsActive);
        }
    }
}
