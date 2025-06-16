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
            Cart? cart = await _context.Carts.Include(item=>item.CartItems).FirstOrDefaultAsync(item => item.UserId.ToString() == userId);
            if (cart == null)
            {
                return false;
            }

            Console.WriteLine(cart.CartItems);

            if (cart.CartItems.Any(item => item.ProductId == productId && item.Type == type))
            {
                CartItem? existingCartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId && item.Type == type);
                //if (existingCartItem != null)
                //{
                // 4a. Jeśli jest, to tylko zwiększamy ilość
                existingCartItem!.Quantity += quantity;
                //}

                


            }
            else
            {
                // 4b. Jeśli nie ma, to tworzymy nowy
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

        public Task SaveToDb()
        {
            return _context.SaveChangesAsync();
        }
    }
}
