using Entities.Models;
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

        public async Task<IEnumerable<OrderResponse>> GetAllOrders(Guid userId)
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllOrders(userId);
            return orders.Select(item => item.ToOrderResponse());
        }
    }
}
