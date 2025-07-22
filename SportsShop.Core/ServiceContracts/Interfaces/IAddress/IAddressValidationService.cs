using SportsShop.Core.ServiceContracts.DTO.AddressDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.IAddress;
public interface IAddressValidationService
{
    Task<bool> IsAddressValid(AddressAddRequest addressAddRequest);
}
