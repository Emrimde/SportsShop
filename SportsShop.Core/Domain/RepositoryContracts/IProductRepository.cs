using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.Domain.RepositoryContracts;
public interface IProductRepository
{
    Task<Product?> GetProductById(int id);
    Task UpdateProductQuantity(List<Product> products);
}

