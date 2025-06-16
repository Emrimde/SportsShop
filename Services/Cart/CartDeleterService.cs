using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Interfaces.ICart;

namespace Services
{
    public class CartDeleterService : ICartDeleterService
    {
        private readonly SportsShopDbContext _context;

        public CartDeleterService(SportsShopDbContext context)
        {
            _context = context;
        }

        public Task<bool> ClearCart(string userId)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> RemoveFromCart(int productId, string userId)
        {
            CartItem? cartItem = await _context.CartItems.Include(item => item.Cart).FirstOrDefaultAsync(item => item.Id == productId && item.Cart.UserId.ToString() == userId);

            if (cartItem == null)
            {
                return false;
            }
            cartItem.IsActive = false;
            await _context.SaveChangesAsync();
            return true;


        }

    }
}
