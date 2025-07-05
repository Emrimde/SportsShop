using ServiceContracts.DTO.CartItemDto;

namespace ServiceContracts.Interfaces.ICart
{
    public interface ICartAdderService
    {
        Task<bool> AddToCart(CartItemAddRequest cartItemAddRequest, string userId);
    }
}
