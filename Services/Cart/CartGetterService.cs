using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.CartItemDto;
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

        public async Task<List<CartItemResponse>> GetAllCartItems(string userId)
        {
            List <CartItemResponse> cartitems = await _context.Carts.Include(item => item.CartItems).ThenInclude(item => item.Product)
                .Where(item => item.UserId.ToString() == userId).SelectMany(item => item.CartItems.Where(item => item.IsActive)).Select(item => item.ToCartItemResponse()).ToListAsync();

            return cartitems;
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

        public Task<List<CartItem>> GetCartItems(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalCostOfAllCartItems(string userId)
        {
            return Convert.ToInt32((await GetAllCartItems(userId)).Sum(item => item.Price * item.Quantity));
        }

    }
}
