using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts.DTO.OrderDto;
using ServiceContracts.Interfaces.IOrder;

namespace Services
{
    public class OrderGetterService : IOrderGetterService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderGetterService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderResponse>> GetAllOrders(string id)
        {
            return await _orderRepository.GetAllOrders(id).Select(item => item.ToOrderResponse()).ToListAsync();
        }
    }
}
