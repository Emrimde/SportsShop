using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;
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

            List<CartItem> cartItems = await _cartGetterService.GetCartItems(user.Id.ToString());
            int totalCost = _cartGetterService.GetTotalCost(cartItems);
            Order order = new Order();

            if (checkoutViewModel.AddressId > 0)
            {
                order = new Order()
                {
                    TotalCost = totalCost,
                    ShippingCost = shippingCost,
                    IsPaid = true,
                    OrderDate = DateTime.Now,
                    AddressId = checkoutViewModel.AddressId ?? 0,
                    UserId = user.Id,
                    SupplierId = checkoutViewModel.SupplierId ?? 0,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    CartItems = cartItems.Select(item => new CartItem()
                    {
                        ProductId = item.ProductId,
                        //CartId = item.Cart.Id,bez cartId I tyle?
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Quantity = item.Quantity,
                        Price = item.Product.Price,
                        Type = item.Type,
                    }).ToList()
                };
            }
            else if (ModelState.IsValid)
            {
                AddressDTO address = new AddressDTO()
                {
                    City = checkoutViewModel.Address.City,
                    Country = checkoutViewModel.Address.Country,
                    Street = checkoutViewModel.Address.Street,
                    ZipCode = checkoutViewModel.Address.ZipCode
                };

                int addressId = await _addressAdderService.AddAddress(address, user.Id.ToString());
                order = new Order()
                {
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsPaid = true,
                    ShippingCost = shippingCost,
                    TotalCost = totalCost,
                    AddressId = addressId,
                    SupplierId = checkoutViewModel.SupplierId ?? 0,
                    OrderDate = DateTime.Now,
                    UserId = user.Id,
                    CartItems = cartItems.Select(item => new CartItem()
                    {
                        ProductId = item.ProductId,
                        
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Quantity = item.Quantity,
                        Price = item.Product.Price,
                        Type = item.Type,
                    }).ToList()
                };
            }
            else
            {
                return RedirectToAction("GetShippingCost", "Cart", new { supplierId = checkoutViewModel.SupplierId });
            }

            await _orderAdderService.AddOrder(order);
            
            //cartItems.ForEach(item =>
            //{
            //    item.Order = order;
            //    item.OrderId = order.Id;
            //    item.IsActive = false;
            //    item.DeleteDate = DateTime.Now;
            //});
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
            List<Order> orders = await _orderGetterService.GetAllOrders(user.Id.ToString());
            if (orders == null)
            {
                return BadRequest();
            }

            List<OrderViewModel> ordersViewModel = orders.Select(item => new OrderViewModel()
            {
                IsPaid = item.IsPaid,
                OrderDate = item.OrderDate,
                TotalCost = item.TotalCost,
                ShippingCost = item.ShippingCost,
                CartItems = item.CartItems
            }).ToList();

            return View(ordersViewModel);
        }
    }
}
