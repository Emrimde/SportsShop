using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.Interfaces.ICart;

namespace Services
{
    public class CartDeleterService : ICartDeleterService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartDeleterService> _logger;

        public CartDeleterService( ICartRepository cartRepository, ILogger<CartDeleterService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task ClearCart(string userId)
        {
            _logger.LogDebug("ClearCart service method. Parameter: userId: {userId}", userId);
            await _cartRepository.ClearCart(userId);
        }

        public async Task<bool> RemoveFromCart(int productId, int cartId)
        {
            _logger.LogDebug("RemoveFromCart service method. Parameters:  productId: {productId}, cartId: {cartId}", productId, cartId);
            return await _cartRepository.RemoveFromCart(productId, cartId);
        }
    }
}
