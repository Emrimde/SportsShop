using Entities.DatabaseContext;

using Microsoft.EntityFrameworkCore;
using ServiceContracts.Interfaces.IOrder;
using Entities.Models;

namespace Services
{
    public class OrderAdderService : IOrderAdderService
    {
        private readonly SportsShopDbContext _context;
        public OrderAdderService(SportsShopDbContext context)
        {
            _context = context;
        }
        public async Task<Order> AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
