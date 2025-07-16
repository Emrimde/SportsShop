namespace ServiceContracts.Interfaces.ICart
{
    public interface ICartDeleterService
    {
        Task<bool> RemoveFromCart(int productId, int cartId);
        Task ClearCart(Guid userId);
    }
}
