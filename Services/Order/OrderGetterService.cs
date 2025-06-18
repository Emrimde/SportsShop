using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.OrderDto;
using ServiceContracts.Interfaces.IOrder;

namespace Services
{
    public class OrderGetterService : IOrderGetterService
    {
        private readonly SportsShopDbContext _context;
        public OrderGetterService(SportsShopDbContext context)
        {
            _context = context;
        }

        public Task<List<OrderResponse>> GetAllOrderss(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetAllOrders(string id)
        {
            return await _context.Orders.Include(item => item.User).Include(o => o.CartItems)
            .ThenInclude(ci => ci.Product).Where(item => item.UserId.ToString() == id && item.IsActive).ToListAsync();
        }
    }
}
