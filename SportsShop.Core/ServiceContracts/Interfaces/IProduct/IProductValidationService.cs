namespace SportsShop.Core.ServiceContracts.Interfaces.IProduct;
    public interface IProductValidationService
    {
        Task<bool> IsEnoughProductInMagazine(int productId, int quantity);
    }

