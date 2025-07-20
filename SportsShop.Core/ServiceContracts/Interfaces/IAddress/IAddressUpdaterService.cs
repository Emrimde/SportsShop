using ServiceContracts.DTO.AddressDto;

namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressUpdaterService
    {
        Task<AddressResponse?> UpdateAddress(AddressUpdateRequest model, Guid userId);
    }
}
