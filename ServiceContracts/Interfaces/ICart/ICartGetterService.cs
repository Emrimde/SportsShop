using Entities.Models;
using ServiceContracts.DTO.CartItemDto;

namespace ServiceContracts.Interfaces.ICart
{
    public interface ICartGetterService
    {
        Task<List<CartItemResponse>> GetAllCartItems(int cartId);
        Task<int> GetTotalCostOfAllCartItems(int cartId);
        Task<Cart?> GetCartByUserId(string userId);
    }
}
