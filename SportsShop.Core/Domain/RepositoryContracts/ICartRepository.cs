using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.Domain.RepositoryContracts;
public interface ICartRepository
{
    Task<bool> RemoveFromCart(int productId, int cartId);
    Task<CartItem> AddCartItem(CartItem cartItem);
    Task ClearCart(Guid userId);
    Task<IEnumerable<CartItem>> GetAllCartItems(int cartId);
    Task<Cart?> GetCartByUserId(Guid userId);
    Task UpdateCartItemQuantity(int cartItem, int quantity);
    Task IncreaseCartItemQuantity(int cartItemId, int quantity);
    Task<CartItem?> GetCartItemByProductAndCartId(int productId, int cartId);
    Task<Cart> AddCartToTheUser(Cart cart);
    Task<int?> GetCartIdByUserId(Guid userId);
}
