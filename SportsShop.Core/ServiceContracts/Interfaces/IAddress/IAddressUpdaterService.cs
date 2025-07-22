using SportsShop.Core.ServiceContracts.DTO.AddressDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.IAddress;
public interface IAddressUpdaterService
{
    Task<AddressResponse?> UpdateAddress(AddressUpdateRequest model, Guid userId);
}
