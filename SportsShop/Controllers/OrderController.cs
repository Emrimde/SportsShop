using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.DTO.CartItemDto;
using ServiceContracts.DTO.OrderDto;
using ServiceContracts.Interfaces.IAddress;
using ServiceContracts.Interfaces.ICart;
using ServiceContracts.Interfaces.IOrder;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IAddressGetterService _addressGetterService;
        private readonly IAddressAdderService _addressAdderService;
        private readonly ICartGetterService _cartGetterService;
        private readonly ICartAdderService _cartAdderService;
        private readonly IOrderGetterService _orderGetterService;
        private readonly IOrderAdderService _orderAdderService;
        public OrderController(UserManager<User> userManager, IAddressGetterService addressesService, IAddressAdderService addressAdderService, ICartGetterService cartService, ICartAdderService cartAdderService, IOrderGetterService orderService, IOrderAdderService orderAdderService)
        {
            _userManager = userManager;
            _addressGetterService = addressesService;
            _addressAdderService = addressAdderService;
            _cartGetterService = cartService;
            _orderGetterService = orderService;
            _orderAdderService = orderAdderService;
            _cartAdderService = cartAdderService;   
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel checkoutViewModel, decimal shippingCost)
        {
            User? user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }

            List<CartItemResponse> cartItems = await _cartGetterService.GetAllCartItems(user.Id.ToString());
            int totalCost = await _cartGetterService.GetTotalCostOfAllCartItems(user.Id.ToString());
            OrderAddRequest order = new OrderAddRequest();

            if (checkoutViewModel.AddressId > 0)
            {
                order = new OrderAddRequest()
                {
                    TotalCost = totalCost,
                    ShippingCost = shippingCost,
                    IsPaid = true,
                    OrderDate = DateTime.Now,
                    AddressId = checkoutViewModel.AddressId ?? 0,
                    UserId = user.Id,
                    SupplierId = checkoutViewModel.SupplierId ?? 0,
                    
                    CartItems = cartItems.Select(item => new CartItemAddRequest()
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Type = item.Type,
                    }).ToList()
                };
            }
            else if (ModelState.IsValid)
            {
                AddressAddRequest address = checkoutViewModel.Address;
                AddressResponse? adressResponse = await _addressAdderService.AddAddress(address, user.Id);

                int addressId = await _addressGetterService.GetAddressId(adressResponse!.Id);

                order = new OrderAddRequest()
                {
                    IsPaid = true,
                    ShippingCost = shippingCost,
                    TotalCost = totalCost,
                    AddressId = addressId,
                    SupplierId = checkoutViewModel.SupplierId ?? 0,
                    OrderDate = DateTime.Now,
                    UserId = user.Id,
                    CartItems = cartItems.Select(item => new CartItemAddRequest()
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Type = item.Type,
                    }).ToList()
                };
            }
            else
            {
                return RedirectToAction("GetShippingCost", "Cart", new { supplierId = checkoutViewModel.SupplierId });
            }

            await _orderAdderService.AddOrder(order);
            await _cartAdderService.SaveToDb();

            Cart? cart = await _cartGetterService.GetCart(user.Id.ToString());

            if (cart != null)
            {
                cart.CartItems.Clear();
                await _cartAdderService.SaveToDb();
            }

            return View();
        }
        public async Task<IActionResult> History()
        {
            User? user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }
            List<OrderResponse> orders = await _orderGetterService.GetAllOrders(user.Id.ToString());
            if (orders == null)
            {
                return BadRequest();
            }

            return View(orders);
        }
    }
}
