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
            if (checkoutViewModel.AddressId > 0)
            {
                List<CartItem> cartItems = await _cartService.GetCartItems(user.Id.ToString());
                int totalCost = _cartService.GetTotalCost(cartItems, false);
                Order order = new Order()
                {
                    TotalCost =totalCost,
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
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                cartItems.ForEach(item => item.Order = order);
                await _context.SaveChangesAsync();

                Cart? cart = await _context.Carts
                .Include(item => item.CartItems)
                .FirstOrDefaultAsync(item =>
                item.UserId.ToString() == user.Id.ToString() &&
                item.IsActive);

                if (cart != null)
                {
                    cart.CartItems.Clear();
                    await _context.SaveChangesAsync();
                }

                return View();
            }

            //jezeli wypelnilespole
            else if (ModelState.IsValid)
            {
                AddressDTO address = new AddressDTO()
                {
                    City = checkoutViewModel.Address.City,
                    Country = checkoutViewModel.Address.Country,
                    Street = checkoutViewModel.Address.Street,
                    ZipCode = checkoutViewModel.Address.ZipCode
                };
                List<CartItem> cartItems = await _cartService.GetCartItems(user.Id.ToString());
                int totalCost = _cartService.GetTotalCost(cartItems, false);

                int addressId = await _addressService.AddAddress(address, user.Id.ToString());
                Order order = new Order()
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
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                cartItems.ForEach(item => item.Order = order);
                await _context.SaveChangesAsync();
                Cart? cart = await _context.Carts
               .Include(item => item.CartItems)
               .FirstOrDefaultAsync(item =>
               item.UserId.ToString() == user.Id.ToString() &&
               item.IsActive);

                if (cart != null)
                {
                    cart.CartItems.Clear();
                    await _context.SaveChangesAsync();
                }
                //await _addressService.GetAddress(); // tu trzeba id podaj ale skąd??
                //dwa przypadki - jeden jesli jest adresId, a drugi jesli jest adres z formularza
                //jesli jest adresId
                return View();
            }
            else
            {
                return RedirectToAction("GetShippingCost", "Cart", new { supplierId = checkoutViewModel.SupplierId });
            }
        }
    
        public async Task<IActionResult> History()
        {
            return View();
        }
    }
}
