using ServiceContracts.DTO.AddressDto;
using ServiceContracts.DTO.OrderDto;
using ServiceContracts.Results;

namespace ServiceContracts.Interfaces.IOrder
{
    public interface IOrderAdderService
    {
        Task<Result> PlaceOrder(OrderAddRequest orderAddRequest, AddressAddRequest address, Guid userId);
        Task<OrderResponse> AddOrder(OrderAddRequest model, int cartId);
    }
}
