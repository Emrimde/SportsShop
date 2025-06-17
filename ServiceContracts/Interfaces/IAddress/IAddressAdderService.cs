using Entities.Models;
using ServiceContracts.DTO;
using ServiceContracts.DTO.AddressDto;


namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressAdderService
    {
        // metoda do likwidacji
        Task<int> AddAddress(AddressDTO model, string UserId);
        Task<AddressResponse?> AddAddress(AddressAddRequest model, Guid userId);
    }
}
