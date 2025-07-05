using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        
        public CartController(UserManager<User> userManager, ICartAdderService cartAdderService,ICartGetterService cartGetterService, ICartUpdaterService cartUpdaterService , ICartDeleterService cartDeleterService, IAddressGetterService addressGetterService, ISupplierGetterService supplierGetterService)
        {
            _userManager = userManager;
            _cartGetterService = cartGetterService;
            _cartAdderService = cartAdderService;
            _cartUpdaterService = cartUpdaterService;
            _cartDeleterService = cartDeleterService;
            _addressGetterService = addressGetterService;
            _supplierGetterService = supplierGetterService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? user = _userManager.GetUserId(User);
            if (user == null)
                return RedirectToAction("SignIn", "Account");
            Cart? cart = await _cartGetterService.GetCartByUserId(user);

            List<CartItemResponse> cartItems = await _cartGetterService.GetAllCartItems(cart!.Id);

            int totalCost = await _cartGetterService.GetTotalCostOfAllCartItems(cart.Id);
            ViewBag.TotalCost = totalCost;

            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(CartItemAddRequest cartItemAddRequest)
        {
            string? user = _userManager.GetUserId(User);
            if (user == null)
                return RedirectToAction("SignIn", "Account");

            bool result = await _cartAdderService.AddToCart(cartItemAddRequest, user);

            if (!result)
                return NotFound("Product not found");

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveFromCart(int id)
        {
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
                throw new InvalidOperationException();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            await _cartUpdaterService.UpdateCartItemQuantity(id, quantity);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(string? coupon,decimal shippingCost, int? supplierId)
        {
            
            User? user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("SignIn", "Account");
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
            decimal shippingCost = await _supplierGetterService.GetSupplierPriceById(supplierId);
            return RedirectToAction("Checkout", new {shippingCost = shippingCost, supplierId = supplierId });
        }
    }
}
