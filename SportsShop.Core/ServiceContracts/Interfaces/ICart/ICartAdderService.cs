using SportsShop.Core.ServiceContracts.DTO.CartItemDto;
using SportsShop.Core.ServiceContracts.Results;

namespace SportsShop.Core.ServiceContracts.Interfaces.ICart;
public interface ICartAdderService
{
    Task<CartItemResult> AddToCart(CartItemAddRequest cartItemAddRequest, Guid userId);
}
