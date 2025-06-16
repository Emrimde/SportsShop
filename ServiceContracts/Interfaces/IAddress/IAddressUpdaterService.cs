using Entities.Models;
using ServiceContracts.DTO;

namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressUpdaterService
    {
        Task<bool> EditAddress(AddressDTO model);
    }
}
