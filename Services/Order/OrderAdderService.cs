using Entities.DatabaseContext;
using Entities.Models;
using ServiceContracts.DTO.OrderDto;
using ServiceContracts.Interfaces.IOrder;

namespace Services
{
    public class OrderAdderService : IOrderAdderService
    {
        private readonly SportsShopDbContext _context;
        public OrderAdderService(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> AddOrder(OrderAddRequest model)
        {
            Order order = model.ToOrder();
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.ToOrderResponse();
        }
    }
}
