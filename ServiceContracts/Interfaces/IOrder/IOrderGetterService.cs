using Entities.Models;
using ServiceContracts.DTO.OrderDto;

namespace ServiceContracts.Interfaces.IOrder
{
    public interface IOrderGetterService
    {
        Task<List<Order>> GetAllOrders(string id);

        Task<List<OrderResponse>> GetAllOrderss(string id);
    }
}
