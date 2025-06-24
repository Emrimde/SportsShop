namespace ServiceContracts.Interfaces.ICart
{
    public interface ICartDeleterService
    {
        Task<bool> RemoveFromCart(int productId, string userId);
        Task ClearCart(string userId);
    }
}
