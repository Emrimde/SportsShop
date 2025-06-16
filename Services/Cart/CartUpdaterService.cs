using Entities.DatabaseContext;
using Entities.Models;
using ServiceContracts.Interfaces.ICart;

namespace Services
{
    public class CartUpdaterService : ICartUpdaterService
    {
        private readonly SportsShopDbContext _context;
        public CartUpdaterService(SportsShopDbContext context)
        {
            _context = context;
        }
        public async Task UpdateCartItemQuantity(int cartItemId, int quantity)
        {
            CartItem? item = await _context.CartItems.FindAsync(cartItemId);
            item!.Quantity = quantity;
            await _context.SaveChangesAsync();

        }
    }
}
