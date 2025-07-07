using Entities.Migrations;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.CartItemDto;
using ServiceContracts.Interfaces.ICart;

namespace Services
{
    public class CartGetterService : ICartGetterService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartGetterService> _logger;
        public CartGetterService(ICartRepository cartRepository, ILogger<CartGetterService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<List<CartItemResponse>> GetAllCartItems(int cartId)
        {
            _logger.LogDebug("GetAllCartItems service method. Parameter: cartId: {cartId}", cartId);

            return await _cartRepository.GetAllCartItems(cartId).Select(item => item.ToCartItemResponse()).ToListAsync();
        }

        public async Task<Cart?> GetCartByUserId(string userId)
        {
            _logger.LogDebug("GetCartByUserId service method. Parameter: userId: {userId}", userId);

            Cart? cart = await _cartRepository.GetCartByUserId(userId);
            if (cart != null)
                return cart;
            return null!;
        }

        public async Task<int> GetTotalCostOfAllCartItems(int cartId)
        {
            _logger.LogDebug("GetTotalCostOfAllCartItems service method. Parameter: cartId: {cartId}", cartId);

            return Convert.ToInt32((await GetAllCartItems(cartId)).Sum(item => item.Price * item.Quantity));
        }
    }
}
