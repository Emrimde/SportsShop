using SportsShop.UI.ViewModels;

namespace SportsShop.UI.Builders.CheckoutBuilderService;
    public interface ICheckoutBuilderService
    {
        Task<CheckoutViewModel> BuildCheckoutViewModel(Guid userId, int totalCost, decimal shippingCost, int? supplierId);
    }

