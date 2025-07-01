using Entities.Models;
using RepositoryContracts;
using ServiceContracts.DTO.OrderDto;
using ServiceContracts.Interfaces.IOrder;

namespace Services
{
    public class OrderAdderService : IOrderAdderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderAdderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse> AddOrder(OrderAddRequest model)
        {
            Order order = model.ToOrder();
            await _orderRepository.AddOrder(order);
            return order.ToOrderResponse();
        }
    }
}
