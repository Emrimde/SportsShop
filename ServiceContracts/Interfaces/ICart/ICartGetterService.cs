using Entities.Models;
using ServiceContracts.DTO.CartItemDto;

namespace ServiceContracts.Interfaces.ICart
{
    public interface ICartGetterService
    {
        Task<List<CartItemResponse>> GetAllCartItems(string userId);
        Task<int> GetTotalCostOfAllCartItems(string userId);
        Task<Cart> GetCart(string userId);
    }
}
