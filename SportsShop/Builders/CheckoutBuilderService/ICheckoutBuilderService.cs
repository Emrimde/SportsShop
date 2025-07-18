using SportsShop.ViewModels;

namespace SportsShop.Builders.ICheckoutBuilderService;
    public interface ICheckoutBuilderService
    {
        Task<CheckoutViewModel> BuildCheckoutViewModel(Guid userId, int totalCost, decimal shippingCost, int? supplierId);
    }

