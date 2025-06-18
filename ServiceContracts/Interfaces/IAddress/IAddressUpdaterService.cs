using ServiceContracts.DTO.AddressDto;

namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressUpdaterService
    {
        Task UpdateAddress(AddressUpdateRequest model);
    }
}
