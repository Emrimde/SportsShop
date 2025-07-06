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

        public List<OrderResponse> GetAllOrders(string id)
        {
            return _orderRepository.GetAllOrders(id).Select(item => item.ToOrderResponse()).ToList();
        }
    }
}
