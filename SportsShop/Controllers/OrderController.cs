using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO;
using ServiceContracts.Interfaces;
using Services;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly SportsShopDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IAddressesService _addressService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        public OrderController(SportsShopDbContext context, UserManager<User> userManager, IAddressesService addressesService, ICartService cartService, IOrderService orderService)
        {
            _context = context;
            _userManager = userManager;
            _addressService = addressesService;
            _cartService = cartService;
            _orderService = orderService;
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

            // Common code for both paths
            List<CartItem> cartItems = await _cartService.GetCartItems(user.Id.ToString());
            int totalCost = _cartService.GetTotalCost(cartItems, false);
            Order order = new Order();

            // Handle address selection
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
                    CartItems = cartItems
                };
            }
            // Handle new address
            else if (ModelState.IsValid)
            {
                AddressDTO address = new AddressDTO()
                {
                    City = checkoutViewModel.Address.City,
                    Country = checkoutViewModel.Address.Country,
                    Street = checkoutViewModel.Address.Street,
                    ZipCode = checkoutViewModel.Address.ZipCode
                };

                int addressId = await _addressService.AddAddress(address, user.Id.ToString());
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
                    CartItems = cartItems
                };
            }
            else
            {
                return RedirectToAction("GetShippingCost", "Cart", new { supplierId = checkoutViewModel.SupplierId });
            }

            // Save the order and update cart items
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Update cart items with order ID
            cartItems.ForEach(item =>
            {
                item.Order = order;
                item.OrderId = order.Id;
                item.IsActive = false;
                item.DeleteDate = DateTime.Now;
            });
            await _context.SaveChangesAsync();

            // Get the user's cart and mark all items as inactive
            Cart? cart = await _context.Carts
                .Include(item => item.CartItems)
                .FirstOrDefaultAsync(item =>
                    item.UserId.ToString() == user.Id.ToString() &&
                    item.IsActive);

            if (cart != null)
            {
                foreach (var item in cart.CartItems)
                {
                    if (item.OrderId == null)
                    {
                        item.IsActive = false;
                        item.DeleteDate = DateTime.Now;
                    }
                }
                await _context.SaveChangesAsync();
            }

            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> PlaceOrder(CheckoutViewModel checkoutViewModel, decimal shippingCost)
        //{
        //    User? user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }
        //    //jezeli wybiore w selekcie
        //    if (checkoutViewModel.AddressId > 0)
        //    {
        //        List<CartItem> cartItems = await _cartService.GetCartItems(user.Id.ToString());
        //        int totalCost = _cartService.GetTotalCost(cartItems, false);
        //        Order order = new Order()
        //        {
        //            TotalCost =totalCost,
        //            ShippingCost = shippingCost,
        //            IsPaid = true,
        //            OrderDate = DateTime.Now,
        //            AddressId = checkoutViewModel.AddressId ?? 0,
        //            UserId = user.Id,
        //            SupplierId = checkoutViewModel.SupplierId ?? 0,
        //            CreatedDate = DateTime.Now,
        //            IsActive = true,
        //            CartItems = cartItems

        //        };
        //        _context.Orders.Add(order);
        //        await _context.SaveChangesAsync();
        //        cartItems.ForEach(item =>
        //        {
        //            item.Order = order;
        //            item.OrderId = order.Id; 
        //        });
        //        await _context.SaveChangesAsync();


        //        //Cart? cart = await _context.Carts
        //        //.Include(item => item.CartItems)
        //        //.FirstOrDefaultAsync(item =>
        //        //item.UserId.ToString() == user.Id.ToString() &&
        //        //item.IsActive);

        //        //if (cart != null)
        //        //{
        //        //    cart.CartItems.Clear();
        //        //    await _context.SaveChangesAsync();
        //        //}

        //        return View();
        //    }

        //    //jezeli wypelnilespole
        //    else if (ModelState.IsValid)
        //    {
        //        AddressDTO address = new AddressDTO()
        //        {
        //            City = checkoutViewModel.Address.City,
        //            Country = checkoutViewModel.Address.Country,
        //            Street = checkoutViewModel.Address.Street,
        //            ZipCode = checkoutViewModel.Address.ZipCode
        //        };
        //        List<CartItem> cartItems = await _cartService.GetCartItems(user.Id.ToString());
        //        int totalCost = _cartService.GetTotalCost(cartItems, false);

        //        int addressId = await _addressService.AddAddress(address, user.Id.ToString());
        //        Order order = new Order()
        //        {
        //            CreatedDate = DateTime.Now,
        //            IsActive = true,
        //            IsPaid = true,
        //            ShippingCost = shippingCost,
        //            TotalCost = totalCost,
        //            AddressId = addressId,
        //            SupplierId = checkoutViewModel.SupplierId ?? 0,
        //            OrderDate = DateTime.Now,
        //            UserId = user.Id,
        //            CartItems = cartItems
        //        };
        //        _context.Orders.Add(order);
        //        await _context.SaveChangesAsync();
        //        cartItems.ForEach(item => item.Order = order);
        //        await _context.SaveChangesAsync();
        //        Cart? cart = await _context.Carts
        //       .Include(item => item.CartItems)
        //       .FirstOrDefaultAsync(item =>
        //       item.UserId.ToString() == user.Id.ToString() &&
        //       item.IsActive);

        //        foreach (var item in cart.CartItems)
        //        {
        //            if (item.OrderId == null)
        //            {
        //                item.IsActive = false;
        //                item.DeleteDate = DateTime.Now;
        //            }
        //        }
        //        await _context.SaveChangesAsync();

        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("GetShippingCost", "Cart", new { supplierId = checkoutViewModel.SupplierId });
        //    }
        //}

        public async Task<IActionResult> History()
        {
            User? user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return BadRequest();
            }
            List<Order> orders =await _orderService.GetAllOrders(user.Id.ToString());
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
