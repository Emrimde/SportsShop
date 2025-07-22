using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.CartItemDto;
using SportsShop.Core.ServiceContracts.Interfaces.ICart;

namespace SportsShop.Core.Services.CartServices;
public class CartGetterService : ICartGetterService
{
    private readonly ICartRepository _cartRepository;
    public CartGetterService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<IReadOnlyList<CartItemResponse>> GetAllCartItems(int cartId)
    {
        IEnumerable<CartItem> cartItems = await _cartRepository.GetAllCartItems(cartId);
        return cartItems.Select(item => item.ToCartItemResponse()).ToList();
    }

    public async Task<Cart> GetCartByUserId(Guid userId)
    {
        Cart? cart = await _cartRepository.GetCartByUserId(userId);
        if (cart == null)
        {
            throw new InvalidOperationException($"Cart for user with ID {userId} was not found.");
        }

        return cart;
    }

    public async Task<int> GetCartIdByUserId(Guid userId)
    {
        int? cartId = await _cartRepository.GetCartIdByUserId(userId);
        if (cartId == null) 
        {
            throw new InvalidOperationException($"CartId for user with ID {userId} was not found.");
        }

        return cartId.Value;
    }

    public async Task<int> GetTotalCostOfAllCartItems(int cartId)
    {
        return Convert.ToInt32((await GetAllCartItems(cartId)).Sum(item => item.Price * item.Quantity));
    }
}
