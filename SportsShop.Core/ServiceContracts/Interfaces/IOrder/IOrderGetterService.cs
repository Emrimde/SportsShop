using SportsShop.Core.ServiceContracts.DTO.OrderDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.IOrder;
public interface IOrderGetterService
{
    Task<IEnumerable<OrderResponse>> GetAllOrders(Guid userId);
}
