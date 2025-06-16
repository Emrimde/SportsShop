using Entities.Models;

namespace ServiceContracts.Interfaces.ICart
{
    public interface ICartGetterService
    {
        Task<List<CartItem>> GetCartItems(string userId);
        int GetTotalCost(List<CartItem> cartItems);
        Task<Cart> GetCart(string userId);
        
    }
}
