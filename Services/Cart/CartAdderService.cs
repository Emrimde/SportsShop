using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Interfaces.ICart;

namespace Services
{
    public class CartAdderService : ICartAdderService
    {
        private readonly SportsShopDbContext _context;
        public CartAdderService(SportsShopDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddToCart(int productId, string userId, int quantity, string type)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(item => item.Id == productId);

            if (product == null)
            {
                return false;
            }

            Cart? cart = await _context.Carts.Include(item=> item.CartItems).FirstOrDefaultAsync(item => item.UserId.ToString() == userId);

            if (cart == null)
            {
                return false;
            }

            CartItem? existingCartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId && item.Type == type && item.IsActive);

            // it executes if the cart item is existing in the cart and we want to increase quantity of the item.
            if(existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }

            //it executes if we add a new cart item which wasn't present in the cart
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
                cart.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
 