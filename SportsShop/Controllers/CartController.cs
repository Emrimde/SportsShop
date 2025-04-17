using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using ServiceContracts.Interfaces;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICartService _cartService;
        private readonly IAddressesService _addressesService;
        private readonly ISupplierService _supplierService;
        public CartController(UserManager<User> userManager, ICartService cartService, IAddressesService addressesService, ISupplierService supplierService)
        {
            _userManager = userManager;
            _cartService = cartService;
            _addressesService = addressesService;
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? coupon)
        {
            ViewBag.CouponMessage = false;
            if (coupon == "PROMO2025")
            {
                ViewBag.CouponMessage = true;
            }

            string? user = _userManager.GetUserId(User);
            if (user == null)
                return RedirectToAction("SignIn", "Account");

            var cartItems = await _cartService.GetCartItems(user);

            //if(cartItems.Count == 0)
            //{

            //}
            int totalCost = _cartService.GetTotalCost(cartItems, ViewBag.CouponMessage);
            List<CartItemViewModel> cartItemViewModel = new List<CartItemViewModel>();
            foreach (var item in cartItems)
            {
                cartItemViewModel.Add(new CartItemViewModel
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Price = (int)item.Price,
                    ProductName = item.Product.Name,
                    ProductDescription = item.Product.Description,
                    Producer = item.Product.Producer,
                    ProductId = item.ProductId,
                    Type = item.Type!,

                });

            }
            
            ViewBag.TotalCost = totalCost;



            return View(cartItemViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, int quantity, string type)
        {
            var user = _userManager.GetUserId(User);
            if (user == null)
                return RedirectToAction("SignIn", "Account");

            bool result = await _cartService.AddToCart(id, user, quantity, type);
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
            bool ok = await _cartService.RemoveFromCart(id, userId);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            await _cartService.UpdateCartItemQuantity(id, quantity);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(string? coupon,decimal shippingCost, int? supplierId)
        {
            //if(checkoutFromForm.SupplierId != null)
            //{
            //    return View(checkoutFromForm);
            //}
            User? user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("SignIn", "Account");

            ViewBag.CouponMessage = false;
            if (coupon == "PROMO2025")
            {
                ViewBag.CouponMessage = true;
            }

            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
            checkoutViewModel.Addresses = await _addressesService.ShowAddresses(user.Id);
            checkoutViewModel.Suppliers = await _supplierService.GetAllSuppliers();
            checkoutViewModel.CartItems = await _cartService.GetCartItems(user.Id.ToString());
            int itemsPrice = _cartService.GetTotalCost(checkoutViewModel.CartItems, ViewBag.CouponMessage);
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
            decimal shippingCost = await _supplierService.GetSupplierPriceById(supplierId);
            return RedirectToAction("Checkout", new {shippingCost = shippingCost, supplierId = supplierId });
        }
    }
}
