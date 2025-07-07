using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.Interfaces.ICart;

namespace Services
{
    public class CartUpdaterService : ICartUpdaterService
    {
        private ICartRepository _cartRepository;
        private readonly ILogger<CartUpdaterService> _logger;
        public CartUpdaterService(ICartRepository cartRepository, ILogger<CartUpdaterService> logger)
        {
            _cartRepository = cartRepository; 
            _logger = logger;
        }

        public async Task UpdateCartItemQuantityIfInTheCart(int cartItemId, int quantity)
        {
            _logger.LogDebug("UpdateCartItemQuantityIfInTheCart service method. Parameters: cartItemId: {cartItemId}, quantity: {quantity}", cartItemId, quantity);

            await _cartRepository.UpdateCartItemQuantityIfInTheCart(cartItemId, quantity);
        }

        public async Task UpdateCartItemQuantity(int cartItemId, int quantity)
        {
            _logger.LogDebug("UpdateCartItemQuantity service method. Parameters: cartItemId: {cartItemId}, quantity: {quantity}", cartItemId, quantity);

            await _cartRepository.UpdateCartItemQuantity(cartItemId, quantity);
        }
    }
}
