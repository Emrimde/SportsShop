using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly SportsShopDbContext _context;
        public CartRepository(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem> AddCartItem(CartItem cartItem)
        {
            cartItem.IsActive = true;
            cartItem.CreatedDate = DateTime.Now;
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public Task<CartItem> AddCartItem(Entities.Migrations.CartItem cartItem)
        {
            throw new NotImplementedException();
        }

        public async Task ClearCart(string userId)
        {
            Cart? cart = await GetCartByUserId(userId);
            cart.CartItems.Clear();
            await _context.SaveChangesAsync();
        }

        public IQueryable<CartItem> GetAllCartItems(int cartId)
        {
            return _context.CartItems.Include(item => item.Product)
                .Where(item => item.CartId == cartId && item.IsActive);
        }

        public async Task<Cart?> GetCartByUserId(string userId)
        {
            return await _context.Carts.Include(item => item.CartItems).FirstOrDefaultAsync(item => item.UserId.ToString() == userId && item.IsActive);
        }


        public async Task<bool> RemoveFromCart(int productId, int cartId)
        {
            CartItem? cartItem = await _context.CartItems.Include(item => item.Cart).FirstOrDefaultAsync(item => item.Id == productId && item.CartId == cartId);

            if (cartItem == null)
            {
                return false;
            }
            cartItem.IsActive = false;
            int deletedCount = await _context.SaveChangesAsync();
            return deletedCount > 0;
        }

        public async Task UpdateCartItemQuantityIfInTheCart(int cartItemId, int quantity)
        {
            CartItem? item = await _context.CartItems.FindAsync(cartItemId);
            item!.Quantity += quantity;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemQuantity(int cartItem, int quantity)
        {
            CartItem? item = await _context.CartItems.FindAsync(cartItem);
            item!.Quantity = quantity;
            await _context.SaveChangesAsync();
        }

        public async Task<CartItem?> GetCartItemByProductAndCartId(int productId, int cartId)
        {
            return await _context.CartItems.FirstOrDefaultAsync(item => item.ProductId == productId && item.CartId == cartId && item.IsActive);
        }
    }
}
