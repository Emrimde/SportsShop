using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CartService : ICartService
    {
        private readonly SportsShopDbContext _context;
        private readonly UserManager<User> _userManager;
        public CartService(SportsShopDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> AddToCart(int productId, string userId, int quantity, string type)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(item=> item.Id == productId);
            if (product == null)
            {
                return false;
            }
            Cart? cart =  await _context.Carts.FirstOrDefaultAsync(item => item.UserId.ToString() == userId);
            if(cart == null)
            {
                return false;
            }

            CartItem cartItem = new CartItem
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
            _context.CartItems.Add(cartItem);
           await  _context.SaveChangesAsync();
            return true;

        }

        public Task<bool> ClearCart(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CartItem>> GetCartItems(string userId)
        {
            Cart? cart = await _context.Carts.Include(item=> item.CartItems).ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(item => item.UserId.ToString() == userId);
            List<CartItem> cartItems = cart.CartItems.Where(item => item.IsActive).ToList();
            return cartItems;
        }

        public int GetTotalCost(List<CartItem> cartItems, bool isCouponValid)
        {
            if (isCouponValid)
            {
                decimal price = cartItems.Sum(item => item.Price * item.Quantity) * 0.75m;
                return Convert.ToInt32(price);
            }
            return Convert.ToInt32(cartItems.Sum(item => item.Price * item.Quantity));
        }

        public async Task<bool> RemoveFromCart(int productId, string userId)
        {
            CartItem? cartItem = await _context.CartItems.Include(item => item.Cart).FirstOrDefaultAsync(item => item.Id == productId && item.Cart.UserId.ToString() == userId);

            if(cartItem == null)
            {
                return false;
            }
            cartItem.IsActive = false;
            await _context.SaveChangesAsync();
            return true;


        }

        public async Task UpdateCartItemQuantity(int cartItemId, int quantity)
        {
            CartItem? item = await _context.CartItems.FindAsync(cartItemId);
            item!.Quantity = quantity;
            await  _context.SaveChangesAsync();
      
        }
    }
}
