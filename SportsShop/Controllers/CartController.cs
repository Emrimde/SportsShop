using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.DTO.CartItemDto;
using ServiceContracts.Interfaces.IAddress;
using ServiceContracts.Interfaces.ICart;
using ServiceContracts.Interfaces.ISupplier;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICartGetterService _cartGetterService;
        private readonly ICartAdderService _cartAdderService;
        private readonly ICartDeleterService _cartDeleterService;
        private readonly ICartUpdaterService _cartUpdaterService;
        private readonly IAddressGetterService _addressGetterService;
        private readonly ISupplierGetterService _supplierGetterService;
        private readonly ILogger<CartController> _logger;
        
        public CartController(UserManager<User> userManager, ICartAdderService cartAdderService,ICartGetterService cartGetterService, ICartUpdaterService cartUpdaterService , ICartDeleterService cartDeleterService, IAddressGetterService addressGetterService, ISupplierGetterService supplierGetterService, ILogger<CartController> logger)
        {
            _userManager = userManager;
            _cartGetterService = cartGetterService;
            _cartAdderService = cartAdderService;
            _cartUpdaterService = cartUpdaterService;
            _cartDeleterService = cartDeleterService;
            _addressGetterService = addressGetterService;
            _supplierGetterService = supplierGetterService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("Index action method displays all cart items.");

            string? user = _userManager.GetUserId(User);

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

            string? user = _userManager.GetUserId(User);

            if (user == null)
            {
                _logger.LogWarning("User not found. Redirecting to SingIn view");
                return RedirectToAction("SignIn", "Account");
            }

            bool result = await _cartAdderService.AddToCart(cartItemAddRequest, user);

            if (!result)
            {
                _logger.LogWarning("Adding failed - AddToCart action");
                return NotFound("Adding failed");
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            _logger.LogDebug("RemoveFromCart action method. Parameter: id: {id}", id);

            string userId = _userManager.GetUserId(User)!;
            if (userId == null)
            {
                return RedirectToAction("SignIn", "Account");
            }

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

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            _logger.LogDebug("UpdateQuantity action method. Parameters: id: {id}, quantity: {quantity}", id, quantity);

            await _cartUpdaterService.UpdateCartItemQuantity(id, quantity);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(string? coupon,decimal shippingCost, int? supplierId)
        {
            _logger.LogDebug("Checkout action method. Parameters: coupon: {coupon}, shippingCost: {shippingCost}, supplierId: {supplierId}", coupon, shippingCost, supplierId);

            User? user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("User not found. Redirecting to SingIn view of AccountController");
                return RedirectToAction("SignIn", "Account");

            }
            Cart? cart = await _cartGetterService.GetCartByUserId(user.Id.ToString());

            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
            checkoutViewModel.Addresses = _addressGetterService.GetAllAddresses(user.Id);
            checkoutViewModel.Suppliers =  _supplierGetterService.GetAllSuppliers();
            checkoutViewModel.CartItems = await _cartGetterService.GetAllCartItems(cart.Id);
            int itemsPrice = await _cartGetterService.GetTotalCostOfAllCartItems(cart.Id);
            checkoutViewModel.ItemsPrice = itemsPrice;
            if(shippingCost == 0m || itemsPrice >= 300m)
            {
                checkoutViewModel.ShippingCost = 0m;
            }
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
