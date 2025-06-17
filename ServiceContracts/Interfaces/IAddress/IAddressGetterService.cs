using Entities.Models;
using ServiceContracts.DTO;
using ServiceContracts.DTO.AddressDto;


namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressGetterService
    {
        // metoda do likwidacji
        Task<List<Address>> ShowAddresses(Guid userId);

        // metoda do likwidacji
        Task<Address> GetAddress(int? id);

        // Bardziej clean
        Task<AddressResponse?> GetAddressById(int? id);
        Task<List<AddressResponse>> GetAllAddresses(Guid userId);
    }
}
