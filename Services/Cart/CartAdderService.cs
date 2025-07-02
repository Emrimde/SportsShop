using Entities.Models;
using RepositoryContracts;
using ServiceContracts.Interfaces.ICart;

namespace Services
{
    public class CartAdderService : ICartAdderService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public CartAdderService( IProductRepository productRepository, ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }
        public async Task<bool> AddToCart(int productId, string userId, int quantity, string type)
        {
            Product? product = await _productRepository.GetProductById(productId);
            Cart? cart = await _cartRepository.GetCartByUserId(userId);

            if (product == null || cart == null)
            {
                return false;
            }

            CartItem? existingCartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId && item.Type == type && item.IsActive);


            // it executes if the cart item is existing in the cart and we want to increase quantity of the item.
            if(existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }

            //it executes if we add a new cart item which wasn't present in the cart.
            else
            {
                var cartItem = new CartItem
                {
                    ProductId = productId,
                    CartId = cart.Id,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Quantity = quantity,
                    Price = product.Price,
                    Type = type,
                };
                await _cartRepository.AddCartItem(cartItem);
            }

            return true;
        }
    }
}
 