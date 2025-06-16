using Entities.Models;
using ServiceContracts.DTO;


namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressGetterService
    {
        Task<List<Address>> ShowAddresses(Guid userId);
        Task<Address> GetAddress(int? id);
    }
}
