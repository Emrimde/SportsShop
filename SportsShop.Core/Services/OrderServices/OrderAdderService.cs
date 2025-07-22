using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.AddressDto;
using SportsShop.Core.ServiceContracts.DTO.CartItemDto;
using SportsShop.Core.ServiceContracts.DTO.OrderDto;
using SportsShop.Core.ServiceContracts.Interfaces.IAddress;
using SportsShop.Core.ServiceContracts.Interfaces.ICart;
using SportsShop.Core.ServiceContracts.Interfaces.IOrder;
using SportsShop.Core.ServiceContracts.Interfaces.ISupplier;
using SportsShop.Core.ServiceContracts.Results;

namespace SportsShop.Core.Services.OrderServices;
public class OrderAdderService : IOrderAdderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartGetterService _cartGetterService;
    private readonly ICartDeleterService _cartDeleterService;
    private readonly IAddressGetterService _addressGetterService;
    private readonly ISupplierGetterService _supplierGetterService;
    private readonly IAddressAdderService _addressAdderService;

    public OrderAdderService(IOrderRepository orderRepository, ICartGetterService cartGetterService, ICartDeleterService cartDeleterService, IAddressGetterService addressGetterService, ISupplierGetterService supplierGetterService, IAddressAdderService addressAdderService)
    {
        _orderRepository = orderRepository;
        _cartGetterService = cartGetterService;
        _addressGetterService = addressGetterService;
        _supplierGetterService = supplierGetterService;
        _cartDeleterService = cartDeleterService;
        _addressAdderService = addressAdderService;
    }

    public async Task<OrderResponse> AddOrder(OrderAddRequest model, int cartId)
    {
        Order order = model.ToOrder();
        await _orderRepository.AddOrder(order, cartId);
        return order.ToOrderResponse();
    }

    public async Task<Result> PlaceOrder(OrderAddRequest orderAddRequest, AddressAddRequest address, Guid userId)
    {
        if(orderAddRequest.SupplierId == 0)
        {
            return Result.Fail("Supplier not selected!");
        }
        if(orderAddRequest.AddressId == 0 && !_addressGetterService.IsAddressProvided(address))
        {
            return Result.Fail("Fill the field with the address!");
        }
        if(_addressGetterService.IsAddressProvided(address)) 
        {
            AddressResponse? addressResponse = await _addressAdderService.AddAddress(address, userId);
            if (addressResponse != null)
            {
                orderAddRequest.AddressId = addressResponse.Id;
            }
            else
            {
                return Result.Fail("Invalid address");
            }
        }
        int cartId = await _cartGetterService.GetCartIdByUserId(userId);
        IEnumerable<CartItemResponse> cartItems = await _cartGetterService.GetAllCartItems(cartId);
        int totalCost = await _cartGetterService.GetTotalCostOfAllCartItems(cartId);

        orderAddRequest.CartItems = cartItems.Select(item => new CartItemAddRequest
        {
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            Price = item.Price,
            Type = item.Type,
            CartId = cartId,
        }).ToList();

        orderAddRequest.ShippingCost = await _supplierGetterService.GetSupplierPriceById(orderAddRequest.SupplierId);
        orderAddRequest.TotalCost = totalCost;
        orderAddRequest.UserId = userId;


        await AddOrder(orderAddRequest, cartId);
        await _cartDeleterService.ClearCart(userId);

        return Result.Ok();
    }
}
