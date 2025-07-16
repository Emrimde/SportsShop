using ServiceContracts.DTO.AddressDto;

namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressValidationService
    {
        Task<bool> IsAddressValid(AddressAddRequest addressAddRequest);
    }
}
