using ServiceContracts.DTO.AddressDto;

namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressAdderService
    {
        Task<AddressResponse?> AddAddress(AddressAddRequest model, string userId);
    }
}
