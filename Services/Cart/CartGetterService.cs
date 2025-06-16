using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Interfaces.ICart;

namespace Services
{
    public class CartGetterService : ICartGetterService
    {
        private readonly SportsShopDbContext _context;
        public CartGetterService(SportsShopDbContext context)
        {
            _context = context;
        }
        public async Task<Cart> GetCart(string userId)
        {
            Cart? cart = await _context.Carts
                .Include(item => item.CartItems)
                .FirstOrDefaultAsync(item =>
                    item.UserId.ToString() == userId &&
                    item.IsActive);
            if (cart != null)
                return cart;
            return null!;
        }
        public async Task<List<CartItem>> GetCartItems(string userId)
        {
            Cart? cart = await _context.Carts.Include(item => item.CartItems).ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(item => item.UserId.ToString() == userId);
            List<CartItem> cartItems = cart.CartItems.Where(item => item.IsActive).ToList();
            return cartItems;
        }
        public int GetTotalCost(List<CartItem> cartItems)
        {
                //decimal price = cartItems.Sum(item => item.Price * item.Quantity) * 0.75m;
                //return Convert.ToInt32(price);   
            return Convert.ToInt32(cartItems.Sum(item => item.Price * item.Quantity));
        }
    }
}
