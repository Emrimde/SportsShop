using Entities.Models;

namespace RepositoryContracts
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order model);
        IQueryable<Order> GetAllOrders(string id);
    }
}
