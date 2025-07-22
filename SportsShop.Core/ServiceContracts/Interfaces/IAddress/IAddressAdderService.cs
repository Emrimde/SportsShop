using SportsShop.Core.ServiceContracts.DTO.AddressDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.IAddress;
public interface IAddressAdderService
{
    Task<AddressResponse?> AddAddress(AddressAddRequest model, Guid userId);
}
