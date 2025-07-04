using ServiceContracts.DTO.AddressDto;

namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressGetterService
    {
        Task<AddressResponse?> GetAddressById(int? id);
        List<AddressResponse> GetAllAddresses(Guid userId);
        bool IsAddressProvided(AddressAddRequest request);
    }
}
