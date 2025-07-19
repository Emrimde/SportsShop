using Entities.Models;
using RepositoryContracts;
using ServiceContracts.Interfaces.ICart;
using ServiceContracts.Interfaces.IProduct;

namespace Services;
public class ProductValidationService : IProductValidationService
{
    private readonly IProductRepository _productRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ICartGetterService _cartGetterService;
    public ProductValidationService(IProductRepository productRepository, ICartRepository cartRepository, ICartGetterService cartGetterService)
    {
        _productRepository = productRepository;
        _cartRepository = cartRepository;
        _cartGetterService = cartGetterService;
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

