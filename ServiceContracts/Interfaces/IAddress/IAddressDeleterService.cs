namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressDeleterService
    {
        Task<bool> DeleteAddress(int id, string userId);  
    }
}
