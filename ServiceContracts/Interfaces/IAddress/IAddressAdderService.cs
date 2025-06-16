using Entities.Models;
using ServiceContracts.DTO;

namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressAdderService
    {
        Task<int> AddAddress(AddressDTO model, string UserId);
    }
}
