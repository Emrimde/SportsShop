using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.CartItemDto;
using SportsShop.Core.ServiceContracts.Interfaces.ICart;
using SportsShop.Core.ServiceContracts.Results;

namespace SportsShop.Core.Services.CartServices;
public class CartAdderService : ICartAdderService
{
    private readonly ICartRepository _cartRepository;
    private readonly ICartGetterService _cartGetterService;
    private readonly IProductRepository _productRepository;
    public CartAdderService(ICartRepository cartRepository, ICartGetterService cartGetterService, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _cartGetterService = cartGetterService;
        _productRepository = productRepository;
    }

    public async Task<CartItemResult> AddToCart(CartItemAddRequest cartItemAddRequest, Guid userId)
    {
        Cart cart = await _cartGetterService.GetCartByUserId(userId);
        CartItem? existingCartItem = await _cartRepository.GetCartItemByProductAndCartId(cartItemAddRequest.ProductId, cart.Id);
        Product? product = await _productRepository.GetProductById(cartItemAddRequest.ProductId);

        if (product == null)
        {
            return CartItemResult.Fail($"Product with ID {cartItemAddRequest.ProductId} was not found");
        }

        //check if there isn't enough quantity in the magazine
        if(existingCartItem?.Quantity + cartItemAddRequest.Quantity > product.Quantity)
        {
            return CartItemResult.NotEnoughProductQuantity("Not enough product quantity in magazine");
        }

        // it executes if the cart item is existing in the cart and we want to update quantity of the item
        if (existingCartItem != null)
        {
            await _cartRepository.IncreaseCartItemQuantity(existingCartItem.Id, cartItemAddRequest.Quantity);
        }

        //it executes if we add a new cart item which wasn't present in the cart
        else
        {
            CartItem cartItem = cartItemAddRequest.ToCartItem();
            cartItem.CartId = cart.Id;
            await _cartRepository.AddCartItem(cartItem);
        }

        return CartItemResult.Ok(null);
    }
}