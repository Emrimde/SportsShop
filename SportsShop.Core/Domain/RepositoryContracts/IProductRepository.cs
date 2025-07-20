using Entities.Models;

namespace RepositoryContracts;
    public interface IProductRepository
    {
        Task<Product?> GetProductById(int id);
        Task UpdateProductQuantity(List<Product> products);
    }

