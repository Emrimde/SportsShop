using ServiceContracts.DTO.OrderDto;

namespace ServiceContracts.Interfaces.IOrder
{
    public interface IOrderGetterService
    {
        Task<IEnumerable<OrderResponse>> GetAllOrders(string id);
    }
}
