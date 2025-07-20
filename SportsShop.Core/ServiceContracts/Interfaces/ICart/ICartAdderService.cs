using ServiceContracts.DTO.CartItemDto;
using ServiceContracts.Results;

namespace ServiceContracts.Interfaces.ICart
{
    public interface ICartAdderService
    {
        Task<CartItemResult> AddToCart(CartItemAddRequest cartItemAddRequest, Guid userId);
    }
}
