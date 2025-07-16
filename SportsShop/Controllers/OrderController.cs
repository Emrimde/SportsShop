using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.DTO.CartItemDto;
using ServiceContracts.DTO.OrderDto;
using ServiceContracts.Interfaces.Account;
using ServiceContracts.Interfaces.IAddress;
using ServiceContracts.Interfaces.ICart;
using ServiceContracts.Interfaces.IOrder;
using ServiceContracts.Interfaces.ISupplier;

namespace SportsShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAddressGetterService _addressGetterService;
        private readonly IAddressAdderService _addressAdderService;
        private readonly IAccountService _accountService;
        private readonly ICartGetterService _cartGetterService;
        private readonly IOrderGetterService _orderGetterService;
        private readonly IOrderAdderService _orderAdderService;
        private readonly ISupplierGetterService _supplierGetterService;
        private readonly ICartDeleterService _cartDeleterService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IAddressGetterService addressesService, IAddressAdderService addressAdderService, ICartGetterService cartService, ICartAdderService cartAdderService, IOrderGetterService orderService, IOrderAdderService orderAdderService, ISupplierGetterService supplierGetterService, ICartDeleterService cartDeleterService, ILogger<OrderController> logger, IAccountService accountService)
        {
            _addressGetterService = addressesService;
            _addressAdderService = addressAdderService;
            _cartGetterService = cartService;
            _orderGetterService = orderService;
            _orderAdderService = orderAdderService; 
            _supplierGetterService = supplierGetterService;
            _cartDeleterService = cartDeleterService;
            _accountService = accountService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogDebug("Index action method. Displays all orders");
            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderAddRequest orderAddRequest, AddressAddRequest addressAddRequest)
        {
            _logger.LogDebug("PlaceOrder action method. Parameters: orderAddRequest: {orderAddRequest}, addressAddRequest: {addressAddRequest}", orderAddRequest.ToString(), addressAddRequest.ToString());
            
            Guid userId =  _accountService.GetUserId(User);

            Cart? cart = await _cartGetterService.GetCartByUserId(userId);
            List<CartItemResponse> cartItems = await _cartGetterService.GetAllCartItems(cart!.Id);
            int totalCost = await _cartGetterService.GetTotalCostOfAllCartItems(cart.Id);
            
            orderAddRequest.CartItems = cartItems.Select(item => new CartItemAddRequest() {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price,
                Type = item.Type,
            }).ToList();

            orderAddRequest.ShippingCost = await _supplierGetterService.GetSupplierPriceById(orderAddRequest.SupplierId);
            orderAddRequest.TotalCost = totalCost;
            orderAddRequest.UserId = userId;

            if(_addressGetterService.IsAddressProvided(addressAddRequest))
            {
                AddressResponse? addressResponse = await _addressAdderService.AddAddress(addressAddRequest, userId!);
                if (addressResponse != null) 
                { 
                    orderAddRequest.AddressId = addressResponse.Id;
                }
            }
          
            await _orderAdderService.AddOrder(orderAddRequest);
            await _cartDeleterService.ClearCart(userId);
            
            return View();
        }

        [Authorize]
        public async Task<IActionResult> History()
        {
            _logger.LogDebug("History action method shows user's all orders");
            Guid userId = _accountService.GetUserId(User);
            IEnumerable<OrderResponse> orders = await _orderGetterService.GetAllOrders(userId);
            
            return View(orders);
        }
    }
}
