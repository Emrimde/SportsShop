using ServiceContracts.Results;

namespace ServiceContracts.Interfaces.ICart
{
    public interface ICartDeleterService
    {
        Task<CartItemResult> RemoveFromCart(int productId, Guid userId);
        Task ClearCart(Guid userId);
    }
}
