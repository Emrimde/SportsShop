using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsShop.Core.ServiceContracts.DTO.AddressDto;
using SportsShop.Core.ServiceContracts.DTO.OrderDto;
using SportsShop.Core.ServiceContracts.Interfaces.IAccount;
using SportsShop.Core.ServiceContracts.Interfaces.IOrder;
using SportsShop.Core.ServiceContracts.Results;

namespace SportsShop.UI.Controllers;
    public class OrderController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IOrderGetterService _orderGetterService;
        private readonly IOrderAdderService _orderAdderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderGetterService orderService, IOrderAdderService orderAdderService,  ILogger<OrderController> logger, IAccountService accountService)
        {
            _orderGetterService = orderService;
            _orderAdderService = orderAdderService; 
            _accountService = accountService;
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("History action method shows user's all orders");
            Guid userId = _accountService.GetUserId(User);
            IEnumerable<OrderResponse> orders = await _orderGetterService.GetAllOrders(userId);

            return View(orders);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderAddRequest orderAddRequest, AddressAddRequest addressAddRequest)
        {
            _logger.LogDebug("PlaceOrder action method. Parameters: orderAddRequest: {orderAddRequest}, addressAddRequest: {addressAddRequest}", orderAddRequest.ToString(), addressAddRequest!.ToString());
            Guid userId =  _accountService.GetUserId(User);
            Result result = await _orderAdderService.PlaceOrder(orderAddRequest, addressAddRequest, userId);

            if (result.Success)
            {
                return View();
            }
            else 
            {
                TempData["ErrorInformation"] = result.ErrorMessage;
                return RedirectToAction("Checkout", "Cart");
            }
       
        }
    }
