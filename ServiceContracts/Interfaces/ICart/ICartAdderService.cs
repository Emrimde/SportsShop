namespace ServiceContracts.Interfaces.ICart
{
    public interface ICartAdderService
    {
        Task<bool> AddToCart(int productId, string userId,int quantity,string type);
        Task SaveToDb();
    }
}
