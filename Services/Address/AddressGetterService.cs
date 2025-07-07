using Entities.Models;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.Interfaces.IAddress;

namespace Services.IAddress
{
    /// <summary>
    /// Its a service for managing user's addresses. They can input their address, edit it, delete it and show all addresses before order time.
    /// </summary>
    public class AddressGetterService : IAddressGetterService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressGetterService> _logger;

        public AddressGetterService(IAddressRepository addressRepository, ILogger<AddressGetterService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }
        
        public async Task<AddressResponse?> GetAddressById(int? id)
        {
            _logger.LogDebug("GetAddressById method. Parameter: id {id}", id);

            if (id == null)
            {
                return null;
            }

            Address? address = await _addressRepository.GetAddressById(id);

            if (address == null)
            {
                return null;
            }

            return address.ToAddressResponse();
        }

        public List<AddressResponse> GetAllAddresses(Guid userId)
        {
            _logger.LogDebug("GetAllAddresses method. Parameter: userId {userId}" , userId);

            return _addressRepository.GetAllAddresses(userId).Select(item => item.ToAddressResponse()).ToList();
        }

        public bool IsAddressProvided(AddressAddRequest addressAddRequest)
        {
            _logger.LogDebug("IsAddressProvided method. Parameter: userId {addressAddRequest}", addressAddRequest.ToString());

            if (!string.IsNullOrEmpty(addressAddRequest.ZipCode) || !string.IsNullOrEmpty(addressAddRequest.Country) || !string.IsNullOrEmpty(addressAddRequest.Street) || !string.IsNullOrEmpty(addressAddRequest.City))
            {
                return true;
            }

            return false;
        }
    }
}
