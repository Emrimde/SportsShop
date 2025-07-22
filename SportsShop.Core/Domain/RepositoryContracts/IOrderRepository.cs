using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.Domain.RepositoryContracts;
public interface IOrderRepository
{
    Task<Order> AddOrder(Order model, int cartId);
    Task<IEnumerable<Order>> GetAllOrders(Guid userId);
}
