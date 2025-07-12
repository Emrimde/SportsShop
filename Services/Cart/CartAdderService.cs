using Entities.Models;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.CartItemDto;
using ServiceContracts.Interfaces.ICart;

namespace Services
{
    public class CartAdderService : ICartAdderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartAdderService> _logger;

        public CartAdderService(ICartRepository cartRepository, ILogger<CartAdderService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<bool> AddToCart(CartItemAddRequest cartItemAddRequest, string userId)
        {
            Cart? cart = await _cartRepository.GetCartByUserId(userId);

            if (cart == null)
            {
                _logger.LogError("Cart not found. AddToCart service method");
                throw new InvalidOperationException(); 
            }

            CartItem? existingCartItem = await _cartRepository.GetCartItemByProductAndCartId(cartItemAddRequest.ProductId, cart.Id);

            // it executes if the cart item is existing in the cart and we want to update quantity of the item
            if (existingCartItem != null)
            {
                await _cartRepository.UpdateCartItemQuantityIfInTheCart(existingCartItem.Id, cartItemAddRequest.Quantity);
            }

            //it executes if we add a new cart item which wasn't present in the cart
            else
            {
                CartItem cartItem = cartItemAddRequest.ToCartItem();
                cartItem.CartId = cart.Id;
                await _cartRepository.AddCartItem(cartItem);
            }

            return true;
        }
    }
}