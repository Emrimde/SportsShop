using ServiceContracts.DTO.AddressDto;

namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressGetterService
    {
        Task<AddressResponse?> GetAddressById(int id, string userId);
        Task<IReadOnlyList<AddressResponse>> GetAllAddresses(string userId);
        bool IsAddressProvided(AddressAddRequest request);
    }
}
