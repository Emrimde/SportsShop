using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.DTO.CartItemDto;
using ServiceContracts.DTO.OrderDto;
using ServiceContracts.Interfaces.IAddress;
using ServiceContracts.Interfaces.ICart;
using ServiceContracts.Interfaces.IOrder;
using ServiceContracts.Interfaces.ISupplier;

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
        private readonly ISupplierGetterService _supplierGetterService;
        private readonly ICartDeleterService _cartDeleterService;
        public OrderController(UserManager<User> userManager, IAddressGetterService addressesService, IAddressAdderService addressAdderService, ICartGetterService cartService, ICartAdderService cartAdderService, IOrderGetterService orderService, IOrderAdderService orderAdderService, ISupplierGetterService supplierGetterService, ICartDeleterService cartDeleterService)
        {
            _userManager = userManager;
            _addressGetterService = addressesService;
            _addressAdderService = addressAdderService;
            _cartGetterService = cartService;
            _orderGetterService = orderService;
            _orderAdderService = orderAdderService;
            _cartAdderService = cartAdderService;  
            _supplierGetterService = supplierGetterService;
            _cartDeleterService = cartDeleterService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderAddRequest orderRequest, AddressAddRequest addressRequest)
        {
            User? user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }

            Cart? cart = await _cartGetterService.GetCartByUserId(user.Id.ToString());
            List<CartItemResponse> cartItems = await _cartGetterService.GetAllCartItems(cart.Id);
            int totalCost = await _cartGetterService.GetTotalCostOfAllCartItems(cart.Id);
            
            orderRequest.CartItems = cartItems.Select(item => new CartItemAddRequest() {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price,
                Type = item.Type,
            }).ToList();

            orderRequest.ShippingCost = await _supplierGetterService.GetSupplierPriceById(orderRequest.SupplierId);
            orderRequest.TotalCost = totalCost;
            orderRequest.UserId = user.Id;

            if(_addressGetterService.IsAddressProvided(addressRequest))
            {
                AddressResponse? addressResponse = await _addressAdderService.AddAddress(addressRequest, user.Id);
                if (addressResponse != null) 
                { 
                    orderRequest.AddressId = addressResponse.Id;
                }
            }
          
            await _orderAdderService.AddOrder(orderRequest);
            await _cartDeleterService.ClearCart(user.Id.ToString());
            
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
