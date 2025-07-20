using Entities.Models;
using ServiceContracts.DTO.CartItemDto;

namespace ServiceContracts.Interfaces.ICart
{
    public interface ICartGetterService
    {
        Task<IReadOnlyList<CartItemResponse>> GetAllCartItems(int cartId);
        Task<int> GetTotalCostOfAllCartItems(int cartId);
        Task<Cart> GetCartByUserId(Guid userId);
        Task<int> GetCartIdByUserId(Guid userId);
    }
}
