using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.Interfaces.ICart;
using SportsShop.Core.ServiceContracts.Interfaces.IProduct;
using SportsShop.Core.ServiceContracts.Results;

namespace SportsShop.Core.Services.CartServices;
public class CartUpdaterService : ICartUpdaterService
{
    private ICartRepository _cartRepository;
    private readonly ICartDeleterService _cartDeleterService;
    private readonly IProductValidationService _productValidationService;
    public CartUpdaterService(ICartRepository cartRepository, ICartDeleterService cartDeleterService, IProductValidationService productValidationService)
    {
        _cartRepository = cartRepository; 
        _cartDeleterService = cartDeleterService;
        _productValidationService = productValidationService;
    }

    public async Task UpdateCartItemQuantityIfInTheCart(int cartItemId, int quantity)
    {
        await _cartRepository.IncreaseCartItemQuantity(cartItemId, quantity);
    }

    public async Task<CartItemResult> UpdateQuantity(int cartItemId, int productId, int quantity, Guid userId)
    {
        if (productId <= 0)
        {
            return CartItemResult.Fail("Invalid productId");
        }

        if (quantity <= 0)
        {
            CartItemResult result = await _cartDeleterService.RemoveFromCart(cartItemId, userId);

            if (result.Success)
            {
                return CartItemResult.Ok(null);
            }
            else
            {
                return CartItemResult.Fail("Cannot delete");
            }
        }

        if (!await _productValidationService.IsEnoughProductInMagazine(productId, quantity))
        {
            return CartItemResult.Fail("Insufficient stock for the requested quantity");
        }

        await _cartRepository.UpdateCartItemQuantity(cartItemId, quantity);
        return CartItemResult.Ok(null);
    }
}
