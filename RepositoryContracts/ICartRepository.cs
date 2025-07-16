using Entities.Models;

namespace RepositoryContracts
{
    public interface ICartRepository
    {
        Task<bool> RemoveFromCart(int productId, int cartId);
        Task<CartItem> AddCartItem(CartItem cartItem);
        Task ClearCart(Guid userId);
        IQueryable<CartItem> GetAllCartItems(int cartId);
        Task<Cart?> GetCartByUserId(Guid userId);
        Task UpdateCartItemQuantity(int cartItem, int quantity);
        Task UpdateCartItemQuantityIfInTheCart(int cartItemId, int quantity);
        Task<CartItem?> GetCartItemByProductAndCartId(int productId, int cartId);
        Task<Cart> AddCartToTheUser(Cart cart);
    }
}
