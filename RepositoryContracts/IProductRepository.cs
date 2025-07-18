using Entities.Models;

namespace RepositoryContracts;
    public interface IProductRepository
    {
        Task<Product?> GetProductById(int id);
    }

