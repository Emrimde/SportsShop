using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.AddressDto;
using SportsShop.Core.ServiceContracts.Interfaces.IAddress;

namespace SportsShop.Core.Services.AddressService;

/// <summary>
/// Interface for adding addresses to the database.
/// </summary>
public class AddressAdderService : IAddressAdderService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IAddressValidationService _addressValidationService;
    public AddressAdderService(IAddressRepository addressRepository, IAddressValidationService addressValidationService)
    {
        _addressRepository = addressRepository;
        _addressValidationService = addressValidationService;
    }

    /// <summary>
    /// Add to the database a new address for the specific user.
    /// </summary>
    /// <param name="addressAddRequest">Address</param>
    /// <param name="userId"></param>
    /// <returns>Address with Id</returns>
    public async Task<AddressResponse?> AddAddress(AddressAddRequest addressAddRequest, Guid userId)
    {
        if (addressAddRequest == null)
        {
            throw new ArgumentNullException(nameof(addressAddRequest));
        }
        if (await _addressValidationService.IsAddressValid(addressAddRequest))
        {
            Address address = addressAddRequest.ToAddress(userId);
            await _addressRepository.AddAddress(address);

            return address.ToAddressResponse();
        }
        return null;
    }
}
