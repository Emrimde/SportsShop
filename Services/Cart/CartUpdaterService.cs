using RepositoryContracts;
using ServiceContracts.Interfaces.ICart;

namespace Services
{
    public class CartUpdaterService : ICartUpdaterService
    {
        private ICartRepository _cartRepository;
        
        public CartUpdaterService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository; 
        }

        public async Task UpdateCartItemQuantityIfInTheCart(int cartItemId, int quantity)
        {
            await _cartRepository.UpdateCartItemQuantityIfInTheCart(cartItemId, quantity);
        }

        public async Task UpdateCartItemQuantity(int cartItemId, int quantity)
        {
            await _cartRepository.UpdateCartItemQuantity(cartItemId, quantity);
        }
    }
}
