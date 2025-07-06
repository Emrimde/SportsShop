using ServiceContracts.DTO.OrderDto;

namespace ServiceContracts.Interfaces.IOrder
{
    public interface IOrderGetterService
    {
        List<OrderResponse> GetAllOrders(string id);
    }
}
