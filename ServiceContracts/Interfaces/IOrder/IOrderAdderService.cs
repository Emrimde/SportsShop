using ServiceContracts.DTO.OrderDto;

namespace ServiceContracts.Interfaces.IOrder
{
    public interface IOrderAdderService
    {
        Task<OrderResponse> AddOrder(OrderAddRequest model);
    }
}
