using SportsShop.Core.Domain.Models;
using SportsShop.Core.ServiceContracts.DTO.CartItemDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.ICart;
public interface ICartGetterService
{
    Task<IReadOnlyList<CartItemResponse>> GetAllCartItems(int cartId);
    Task<int> GetTotalCostOfAllCartItems(int cartId);
    Task<Cart> GetCartByUserId(Guid userId);
    Task<int> GetCartIdByUserId(Guid userId);
}
