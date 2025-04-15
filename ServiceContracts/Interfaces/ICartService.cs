using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddToCart(int productId, string userId,int quantity,string type);
        Task<bool> RemoveFromCart(int productId, string userId);
        Task<bool> ClearCart(string userId);
        Task<List<CartItem>> GetCartItems(string userId);
        Task UpdateCartItemQuantity(int cartItem, int quantity);
        int GetTotalCost(List<CartItem> cartItems, bool isCouponValid);
    }
}
