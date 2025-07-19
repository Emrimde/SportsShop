using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.CartItemDto;
using ServiceContracts.Interfaces.Account;
using ServiceContracts.Interfaces.ICart;
using ServiceContracts.Interfaces.ISupplier;
using ServiceContracts.Results;
using SportsShop.Builders.ICheckoutBuilderService;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartGetterService _cartGetterService;
        private readonly ICartAdderService _cartAdderService;
        private readonly ICartDeleterService _cartDeleterService;
        private readonly ICartUpdaterService _cartUpdaterService;
        private readonly ISupplierGetterService _supplierGetterService;
        private readonly ICheckoutBuilderService _checkoutBuilderService;
        private readonly IAccountService _accountService;
        private readonly ILogger<CartController> _logger;
        
        public CartController(ICartAdderService cartAdderService,ICartGetterService cartGetterService, ICartUpdaterService cartUpdaterService , ICartDeleterService cartDeleterService, ISupplierGetterService supplierGetterService, ILogger<CartController> logger, IAccountService accountService, ICheckoutBuilderService checkoutBuilderService)
        {
            _cartGetterService = cartGetterService;
            _cartAdderService = cartAdderService;
            _cartUpdaterService = cartUpdaterService;
            _cartDeleterService = cartDeleterService;
            _supplierGetterService = supplierGetterService;
            _accountService = accountService;
            _checkoutBuilderService = checkoutBuilderService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("Index action method displays all cart items.");

            Guid userId = _accountService.GetUserId(User);
            int cartId = await _cartGetterService.GetCartIdByUserId(userId);
            IReadOnlyList<CartItemResponse> cartItems = await _cartGetterService.GetAllCartItems(cartId);
            int totalCost = await _cartGetterService.GetTotalCostOfAllCartItems(cartId);
            CartViewModel viewModel = new CartViewModel
            {
                CartItems = cartItems,
                TotalCost = totalCost
            };
            
            return View(viewModel);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddToCart(CartItemAddRequest cartItemAddRequest, string? returnUrl)
        {
            _logger.LogDebug("[HttpPost]AddToCart action method. Parameter: cartItemAddRequest: {dto}", cartItemAddRequest.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Guid userId = _accountService.GetUserId(User);
            CartItemResult result = await _cartAdderService.AddToCart(cartItemAddRequest, userId);
 
            if (!result.Success)
            {
                _logger.LogWarning("Adding failed - AddToCart action");
                return BadRequest(result.ErrorMessage);
            }
            else if (!result.EnoughProduct)
            {
                _logger.LogWarning("Adding failed - not enough product quantity");
                TempData["Information"] = result.ErrorMessage;
                return Redirect(returnUrl!);
            }

                return RedirectToAction("Index");
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            _logger.LogDebug("RemoveFromCart action method. Parameter: id: {id}", productId);

            Guid userId = _accountService.GetUserId(User);
            CartItemResult result = await _cartDeleterService.RemoveFromCart(productId, userId);

            if(!result.Success)
            {
                _logger.LogError("Removing from cart failed");
                TempData["CartError"] = result.ErrorMessage;
            }
            else
            {
                TempData["CartSuccess"] = result.Message;
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId,int productId, int quantity)
        {
            _logger.LogDebug("UpdateQuantity called for cartItemId={CartItemId}, productId={ProductId}, quantity={Quantity}",cartItemId, productId, quantity);

            Guid userId = _accountService.GetUserId(User);
            CartItemResult result = await _cartUpdaterService.UpdateQuantity(cartItemId, productId, quantity, userId);
            if (!result.Success)
            {
                TempData["UpdateError"] = result.ErrorMessage;
            }
            
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Checkout(int totalCost,string? coupon,decimal shippingCost, int? supplierId)
        {
            _logger.LogDebug("Checkout action method");
            Guid userId = _accountService.GetUserId(User);
            Cart? cart = await _cartGetterService.GetCartByUserId(userId);
            CheckoutViewModel checkoutViewModel = await _checkoutBuilderService.BuildCheckoutViewModel(userId, totalCost, shippingCost, supplierId);
            
           
            return View(checkoutViewModel);
        }

        [Authorize]
        public async Task<IActionResult> GetShippingCost(int supplierId)
        {
            _logger.LogDebug("GetShippingCost action method. Parameter: supplierId: {supplierId}", supplierId);
            decimal shippingCost = await _supplierGetterService.GetSupplierPriceById(supplierId);

            return Json(new {price = shippingCost});
        }
    }
}
