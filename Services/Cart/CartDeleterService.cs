using RepositoryContracts;
using ServiceContracts.Interfaces.ICart;

namespace Services
{
    public class CartDeleterService : ICartDeleterService
    {
        private readonly ICartRepository _cartRepository;
        public CartDeleterService( ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task ClearCart(string userId)
        {
            await _cartRepository.ClearCart(userId);
        }

        public async Task<bool> RemoveFromCart(int productId, int cartId)
        {
            return await _cartRepository.RemoveFromCart(productId, cartId);
        }
    }
}
