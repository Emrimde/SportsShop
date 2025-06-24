using ServiceContracts.DTO.OrderDto;

namespace ServiceContracts.Interfaces.IOrder
{
    public interface IOrderGetterService
    {
        Task<List<OrderResponse>> GetAllOrders(string id);
    }
}
