using Entities.Models;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.OrderDto;
using ServiceContracts.Interfaces.IOrder;

namespace Services
{
    public class OrderAdderService : IOrderAdderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderAdderService> _logger;

        public OrderAdderService(IOrderRepository orderRepository, ILogger<OrderAdderService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<OrderResponse> AddOrder(OrderAddRequest model)
        {
            _logger.LogDebug("AddOrder service method. Parameter: model: {model}", model.ToString());
            Order order = model.ToOrder();
            await _orderRepository.AddOrder(order);
            return order.ToOrderResponse();
        }
    }
}
