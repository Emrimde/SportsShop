using Microsoft.EntityFrameworkCore;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Infrastructure.DatabaseContext;

namespace SportsShop.Infrastructure.Repositories;
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

    public async Task ClearCart(Guid userId)
    {
        Cart? cart = await GetCartByUserId(userId);
        cart.CartItems.Clear();
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CartItem>> GetAllCartItems(int cartId)
    {
        return await _context.CartItems.Include(item => item.Product)
            .Where(item => item.CartId == cartId && item.IsActive).ToListAsync();
    }

    public async Task<Cart?> GetCartByUserId(Guid userId)
    {
        return await _context.Carts.FirstOrDefaultAsync(item => item.UserId == userId && item.IsActive);
    }

    public async Task<bool> RemoveFromCart(int productId, int cartId)
    {
        CartItem? cartItem = await _context.CartItems.FirstOrDefaultAsync(item => item.Id == productId && item.CartId == cartId);

        if (cartItem == null)
        {
            return false;
        }
        cartItem.IsActive = false;
        cartItem.DeleteDate = DateTime.UtcNow;
        int deletedCount = await _context.SaveChangesAsync();
        return deletedCount > 0;
    }

    public async Task IncreaseCartItemQuantity(int cartItemId, int quantity)
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

    public async Task<Cart> AddCartToTheUser(Cart cart)
    {
        cart.CreatedDate = DateTime.Now;
        cart.IsActive = true;
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task<int?> GetCartIdByUserId(Guid userId)
    {
        return await _context.Carts.Where(item => item.UserId == userId).Select(item => item.Id).FirstOrDefaultAsync();
    }
}
