using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.CartItemDto;
using ServiceContracts.Interfaces.Account;
using ServiceContracts.Interfaces.IAddress;
using ServiceContracts.Interfaces.ICart;
using ServiceContracts.Interfaces.ISupplier;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartGetterService _cartGetterService;
        private readonly ICartAdderService _cartAdderService;
        private readonly ICartDeleterService _cartDeleterService;
        private readonly ICartUpdaterService _cartUpdaterService;
        private readonly IAddressGetterService _addressGetterService;
        private readonly ISupplierGetterService _supplierGetterService;
        private readonly IAccountService _accountService;
        private readonly ILogger<CartController> _logger;
        
        public CartController(ICartAdderService cartAdderService,ICartGetterService cartGetterService, ICartUpdaterService cartUpdaterService , ICartDeleterService cartDeleterService, IAddressGetterService addressGetterService, ISupplierGetterService supplierGetterService, ILogger<CartController> logger, IAccountService accountService)
        {
            _cartGetterService = cartGetterService;
            _cartAdderService = cartAdderService;
            _cartUpdaterService = cartUpdaterService;
            _cartDeleterService = cartDeleterService;
            _addressGetterService = addressGetterService;
            _supplierGetterService = supplierGetterService;
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("Index action method displays all cart items.");

            Guid user = _accountService.GetUserId(User);

            if (user == null)
            {
                _logger.LogWarning("User not found. Redirecting to SingIn view");
                return RedirectToAction("SignIn", "Account");
            }

            Cart? cart = await _cartGetterService.GetCartByUserId(user);

            List<CartItemResponse> cartItems = await _cartGetterService.GetAllCartItems(cart!.Id);

            int totalCost = await _cartGetterService.GetTotalCostOfAllCartItems(cart.Id);
            ViewBag.TotalCost = totalCost;

            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(CartItemAddRequest cartItemAddRequest)
        {
            _logger.LogDebug("[HttpPost]AddToCart action method. Parameter: cartItemAddRequest: {dto}", cartItemAddRequest.ToString());

            Guid userId = _accountService.GetUserId(User);
            bool result = await _cartAdderService.AddToCart(cartItemAddRequest, userId);

            if (!result)
            {
                _logger.LogWarning("Adding failed - AddToCart action");
                return NotFound("Adding failed");
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            _logger.LogDebug("RemoveFromCart action method. Parameter: id: {id}", id);

            Guid userId = _accountService.GetUserId(User);
            Cart? cart = await _cartGetterService.GetCartByUserId(userId);
            bool ok = await _cartDeleterService.RemoveFromCart(id, cart!.Id);

            if (ok)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogError("Removing from cart failed");
                throw new InvalidOperationException();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            _logger.LogDebug("UpdateQuantity action method. Parameters: id: {id}, quantity: {quantity}", id, quantity);
            await _cartUpdaterService.UpdateCartItemQuantity(id, quantity);

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Checkout(string? coupon,decimal shippingCost, int? supplierId)
        {
            _logger.LogDebug("Checkout action method. Parameters: coupon: {coupon}, shippingCost: {shippingCost}, supplierId: {supplierId}", coupon, shippingCost, supplierId);

            Guid userId = _accountService.GetUserId(User);
            Cart? cart = await _cartGetterService.GetCartByUserId(userId);

            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
            checkoutViewModel.Addresses = await _addressGetterService.GetAllAddresses(userId);
            checkoutViewModel.Suppliers =  _supplierGetterService.GetAllSuppliers();
            checkoutViewModel.CartItems = await _cartGetterService.GetAllCartItems(cart!.Id);
            int itemsPrice = await _cartGetterService.GetTotalCostOfAllCartItems(cart.Id);
            checkoutViewModel.ItemsPrice = itemsPrice;
            //if(shippingCost == 0m || itemsPrice >= 300m)
            //{
            //    checkoutViewModel.ShippingCost = 0m;
            //}
            checkoutViewModel.ShippingCost = shippingCost;
            checkoutViewModel.TotalCost = itemsPrice + shippingCost;
            checkoutViewModel.SupplierId = supplierId;

            return View(checkoutViewModel);
        }

        public async Task<IActionResult> GetShippingCost(int supplierId)
        {
            _logger.LogDebug("GetShippingCost action method. Parameter: supplierId: {supplierId}", supplierId);
            decimal shippingCost = await _supplierGetterService.GetSupplierPriceById(supplierId);

            return RedirectToAction("Checkout", new {shippingCost = shippingCost, supplierId = supplierId });
        }
    }
}
