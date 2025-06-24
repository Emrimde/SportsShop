using ServiceContracts.DTO.AddressDto;

namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressGetterService
    {
        Task<AddressResponse?> GetAddressById(int? id);
        Task<List<AddressResponse>> GetAllAddresses(Guid userId);
        Task<int> GetAddressId(int id);
    }
}
