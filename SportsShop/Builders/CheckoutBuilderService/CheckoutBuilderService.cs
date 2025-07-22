using Microsoft.AspNetCore.Mvc.Rendering;
using SportsShop.Core.ServiceContracts.DTO.AddressDto;
using SportsShop.Core.ServiceContracts.DTO.CartItemDto;
using SportsShop.Core.ServiceContracts.DTO.SupplierDto;
using SportsShop.Core.ServiceContracts.Interfaces.IAddress;
using SportsShop.Core.ServiceContracts.Interfaces.ICart;
using SportsShop.Core.ServiceContracts.Interfaces.ISupplier;
using SportsShop.UI.Builders.CheckoutBuilderService;
using SportsShop.UI.ViewModels;

public class CheckoutBuilderService : ICheckoutBuilderService
{
    private readonly ICartGetterService _cartGetterService;
    private readonly IAddressGetterService _addressGetterService;
    private readonly ISupplierGetterService _supplierGetterService;

    public CheckoutBuilderService(ICartGetterService cartGetterService, IAddressGetterService addressGetterService, ISupplierGetterService supplierGetterService)
    {
        _cartGetterService = cartGetterService;
        _addressGetterService = addressGetterService;
        _supplierGetterService = supplierGetterService;
    }

    public async Task<CheckoutViewModel> BuildCheckoutViewModel(Guid userId, int totalCost, decimal shippingCost, int? supplierId)
    {
        int cartId = await _cartGetterService.GetCartIdByUserId(userId);
        IReadOnlyList<AddressResponse> addresses = await _addressGetterService.GetAllAddresses(userId);
        IReadOnlyList<SupplierResponse> suppliers = _supplierGetterService.GetAllSuppliers();
        List<SelectListItem> selectSuppliers = suppliers.Select(s => new SelectListItem
        {
            Value = s.Id.ToString(),
            Text = $"{s.Name} - ${s.Price}"
        }).ToList();

        IReadOnlyList<CartItemResponse> cartItems = await _cartGetterService.GetAllCartItems(cartId);

        return new CheckoutViewModel
        {
            Addresses = addresses,
            CartItems = cartItems,
            ItemsPrice = totalCost,
            ShippingCost = shippingCost,
            TotalCost = totalCost + shippingCost,
            SupplierId = supplierId,
            Supplierss = selectSuppliers
        };
    }
}
