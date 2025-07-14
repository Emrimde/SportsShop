using Entities.Models;

namespace RepositoryContracts
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order model);
        Task<IEnumerable<Order>> GetAllOrders(string id);
    }
}
