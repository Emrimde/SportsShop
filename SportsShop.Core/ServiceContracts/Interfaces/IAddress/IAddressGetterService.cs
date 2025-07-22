using SportsShop.Core.ServiceContracts.DTO.AddressDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.IAddress;
public interface IAddressGetterService
{
    Task<AddressResponse?> GetAddressById(int id, Guid userId);
    Task<IReadOnlyList<AddressResponse>> GetAllAddresses(Guid userId);
    bool IsAddressProvided(AddressAddRequest request);
}
