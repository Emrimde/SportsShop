using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.Interfaces.ICart;
using SportsShop.Core.ServiceContracts.Results;

namespace SportsShop.Core.Services.CartServices;
public class CartDeleterService : ICartDeleterService
{
    private readonly ICartRepository _cartRepository;
    private readonly ICartGetterService _cartGetterService;
    public CartDeleterService( ICartRepository cartRepository, ICartGetterService cartGetterService)
    {
        _cartRepository = cartRepository;
        _cartGetterService = cartGetterService;
    }

    public async Task ClearCart(Guid userId)
    {
        await _cartRepository.ClearCart(userId);
    }

    public async Task<CartItemResult> RemoveFromCart(int productId, Guid userId)
    {
        if (productId <= 0)
        {
            return CartItemResult.Fail("Invalid product ID");
        }
        int cartId = await _cartGetterService.GetCartIdByUserId(userId);
        await _cartRepository.RemoveFromCart(productId, cartId);
        return CartItemResult.Ok("Product successfully deleted");
    }
}
