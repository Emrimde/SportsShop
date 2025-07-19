using Entities.Models;

namespace RepositoryContracts
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order model, int cartId);
        Task<IEnumerable<Order>> GetAllOrders(Guid userId);
    }
}
