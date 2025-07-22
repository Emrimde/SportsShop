using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.Interfaces.IProduct;

namespace SportsShop.Core.Services.ProductServices;
public class ProductValidationService : IProductValidationService
{
    private readonly IProductRepository _productRepository;
    public ProductValidationService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> IsEnoughProductInMagazine(int productId, int quantity)
    {
        Product? product = await _productRepository.GetProductById(productId);
        if(product!.Quantity > quantity)
        {
            return true;
        }

        return false;
    }
}

