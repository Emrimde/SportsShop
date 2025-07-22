using SportsShop.Core.ServiceContracts.DTO.AddressDto;
using SportsShop.Core.ServiceContracts.DTO.OrderDto;
using SportsShop.Core.ServiceContracts.Results;

namespace SportsShop.Core.ServiceContracts.Interfaces.IOrder;
public interface IOrderAdderService
{
    Task<Result> PlaceOrder(OrderAddRequest orderAddRequest, AddressAddRequest address, Guid userId);
    Task<OrderResponse> AddOrder(OrderAddRequest model, int cartId);
}
