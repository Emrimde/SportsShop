using RepositoryContracts;
using ServiceContracts.Interfaces.ICart;
using ServiceContracts.Results;

namespace Services
{
    public class CartDeleterService : ICartDeleterService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartGetterService _cartGetterService;
        public CartDeleterService( ICartRepository cartRepository, ICartGetterService cartGetterService)
        {
            _cartRepository = cartRepository;
            _cartGetterService = cartGetterService;
        }

        public async Task ClearCart(Guid userId)
        {
            await _cartRepository.ClearCart(userId);
        }

        public async Task<CartItemResult> RemoveFromCart(int productId, Guid userId)
        {
            if (productId <= 0)
            {
                return CartItemResult.Fail("Invalid product ID");
            }
            int cartId = await _cartGetterService.GetCartIdByUserId(userId);
            await _cartRepository.RemoveFromCart(productId, cartId);
            return CartItemResult.Ok("Product successfully deleted");
        }
    }
}
