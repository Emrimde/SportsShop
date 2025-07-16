using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts.DTO.CartItemDto;
using ServiceContracts.Interfaces.ICart;

namespace Services
{
    public class CartGetterService : ICartGetterService
    {
        private readonly ICartRepository _cartRepository;
        public CartGetterService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<List<CartItemResponse>> GetAllCartItems(int cartId)
        {
            return await _cartRepository.GetAllCartItems(cartId).Select(item => item.ToCartItemResponse()).ToListAsync();
        }

        public async Task<Cart?> GetCartByUserId(Guid userId)
        {
            Cart? cart = await _cartRepository.GetCartByUserId(userId);
            if (cart != null)
                return cart;
            return null!;
        }

        public async Task<int> GetTotalCostOfAllCartItems(int cartId)
        {
            return Convert.ToInt32((await GetAllCartItems(cartId)).Sum(item => item.Price * item.Quantity));
        }
    }
}
