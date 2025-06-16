namespace ServiceContracts.Interfaces.ICart
{
    public interface ICartUpdaterService
    {
        Task UpdateCartItemQuantity(int cartItem, int quantity);
    }
}
