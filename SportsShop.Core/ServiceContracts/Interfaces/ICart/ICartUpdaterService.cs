using SportsShop.Core.ServiceContracts.Results;

namespace SportsShop.Core.ServiceContracts.Interfaces.ICart;
public interface ICartUpdaterService
{
    Task UpdateCartItemQuantityIfInTheCart(int cartItemId, int quantity);
    Task<CartItemResult> UpdateQuantity(int cartItemId, int productId, int quantity, Guid userId);
}
