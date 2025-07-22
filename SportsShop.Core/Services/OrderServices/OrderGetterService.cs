using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.OrderDto;
using SportsShop.Core.ServiceContracts.Interfaces.IOrder;

namespace SportsShop.Core.Services.OrderServices;
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
