﻿using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

            var cartItems = await _cartGetterService.GetCartItems(user);

            
            int totalCost = _cartGetterService.GetTotalCost(cartItems);
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
                    ImagePath = item.Product.ImagePath ?? "https://via.placeholder.com/150",

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

            bool result = await _cartAdderService.AddToCart(id, user, quantity, type);

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
            bool ok = await _cartDeleterService.RemoveFromCart(id, userId);
            return RedirectToAction("Index");

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

            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
            checkoutViewModel.Addresses = await _addressGetterService.ShowAddresses(user.Id);
            checkoutViewModel.Suppliers = await _supplierGetterService.GetAllSuppliers();
            checkoutViewModel.CartItems = await _cartGetterService.GetCartItems(user.Id.ToString());
            int itemsPrice = _cartGetterService.GetTotalCost(checkoutViewModel.CartItems);
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
