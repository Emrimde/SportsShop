using RepositoryContracts;
using ServiceContracts.Interfaces.Account;
using ServiceContracts.Interfaces.IAddress;
using ServiceContracts.Interfaces.ICart;
using ServiceContracts.Interfaces.IProduct;
using ServiceContracts.Interfaces.ISupplier;
using ServiceContracts.Results;

namespace Services
{
    public class CartUpdaterService : ICartUpdaterService
    {
        private ICartRepository _cartRepository;
        private readonly ICartGetterService _cartGetterService;
        private readonly ICartAdderService _cartAdderService;
        private readonly ICartDeleterService _cartDeleterService;
        private readonly IProductValidationService _productValidationService;
        private readonly IAddressGetterService _addressGetterService;
        private readonly ISupplierGetterService _supplierGetterService;
        private readonly IAccountService _accountService;

        public CartUpdaterService(ICartRepository cartRepository, ICartAdderService cartAdderService, ICartGetterService cartGetterService, ICartDeleterService cartDeleterService, IAddressGetterService addressGetterService, ISupplierGetterService supplierGetterService, IAccountService accountService, IProductValidationService productValidationService)
        {
            _cartRepository = cartRepository; 
            _cartGetterService = cartGetterService;
            _cartAdderService = cartAdderService;
            _cartDeleterService = cartDeleterService;
            _addressGetterService = addressGetterService;
            _supplierGetterService = supplierGetterService;
            _accountService = accountService;
            _productValidationService = productValidationService;
        }

        public async Task UpdateCartItemQuantityIfInTheCart(int cartItemId, int quantity)
        {
            await _cartRepository.IncreaseCartItemQuantity(cartItemId, quantity);
        }

        public async Task<CartItemResult> UpdateQuantity(int cartItemId, int productId, int quantity, Guid userId)
        {
            if (productId <= 0)
            {
                return CartItemResult.Fail("Invalid productId");
            }

            if (quantity <= 0)
            {
                CartItemResult result = await _cartDeleterService.RemoveFromCart(cartItemId, userId);

                if (result.Success)
                {
                    return CartItemResult.Ok(null);
                }
                else
                {
                    return CartItemResult.Fail("Cannot delete");
                }
            }

            if (!await _productValidationService.IsEnoughProductInMagazine(productId, quantity))
            {
                return CartItemResult.Fail("Insufficient stock for the requested quantity");
            }

            await _cartRepository.UpdateCartItemQuantity(cartItemId, quantity);
            return CartItemResult.Ok(null);
        }
    }
}
