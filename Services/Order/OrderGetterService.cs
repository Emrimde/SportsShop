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

        public async Task<IEnumerable<OrderResponse>> GetAllOrders(string id)
        {
            if(id == null)
            {
                throw new ArgumentNullException(nameof(id), "Id is null");
            }
            IEnumerable<Order> orders = await _orderRepository.GetAllOrders(id);
   
            return orders.Select(item => item.ToOrderResponse()).ToList();
        }
    }
}
