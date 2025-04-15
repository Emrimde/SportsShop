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
        public CartController(UserManager<User> userManager, ICartService cartService)
        {
            _userManager = userManager;
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? coupon)
        {
            ViewBag.CouponMessage = false;
            if (coupon == "PROMO2025")
            {
                ViewBag.CouponMessage = true;
                
                // Możesz zapisać informację o aktywnym kuponie do sesji lub przekazać do widoku jako część ViewModelu
            }

            string? user = _userManager.GetUserId(User);
            if (user == null)
                return RedirectToAction("SignIn", "Account");

            var cartItems = await _cartService.GetCartItems(user);
            int totalCost = _cartService.GetTotalCost(cartItems, ViewBag.CouponMessage);
            List<CartItemViewModel> cartItemViewModel = new List<CartItemViewModel>();
            foreach (var item in cartItems)
            {
                cartItemViewModel.Add(new CartItemViewModel
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Price =(int) item.Price,
                    ProductName = item.Product.Name,
                    ProductDescription = item.Product.Description,
                    Producer = item.Product.Producer,
                    ProductId = item.ProductId,
                    Type = item.Type!,

                });
                
            }
            //ViewBag.CouponMessage = "";
            ViewBag.TotalCost = totalCost;

            

            return View(cartItemViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, int quantity, string type)
        {
            var user = _userManager.GetUserId(User);
            if (user == null)
                return RedirectToAction("SignIn", "Account");

            bool result = await _cartService.AddToCart(id, user, quantity,type);
            if (!result)
                return NotFound("Product not found");

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            string userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
             bool ok =await _cartService.RemoveFromCart(id, userId);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            await _cartService.UpdateCartItemQuantity(id, quantity);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }
        
    }
}
