namespace ServiceContracts.Interfaces.ICart
{
    public interface ICartUpdaterService
    {
        Task UpdateCartItemQuantity(int cartItem, int quantity);
        Task UpdateCartItemQuantityIfInTheCart(int cartItemId, int quantity);
    }
}
