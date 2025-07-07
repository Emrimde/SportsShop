using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.OrderDto;
using ServiceContracts.Interfaces.IOrder;

namespace Services
{
    public class OrderGetterService : IOrderGetterService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderGetterService> _logger;

        public OrderGetterService(IOrderRepository orderRepository, ILogger<OrderGetterService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public List<OrderResponse> GetAllOrders(string id)
        {
            _logger.LogDebug("GetAllOrders service method. Parameter: id: {id}", id);
            return _orderRepository.GetAllOrders(id).Select(item => item.ToOrderResponse()).ToList();
        }
    }
}
