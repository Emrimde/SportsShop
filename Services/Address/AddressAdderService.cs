using Entities.Models;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.Interfaces.IAddress;

namespace Services
{
    /// <summary>
    /// Interface for adding addresses to the database.
    /// </summary>
    public class AddressAdderService : IAddressAdderService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressAdderService> _logger;
        public AddressAdderService(IAddressRepository addressRepository, ILogger<AddressAdderService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        /// <summary>
        /// Add to the database a new address for the specific user.
        /// </summary>
        /// <param name="addressAddRequest">Address</param>
        /// <param name="userId"></param>
        /// <returns>Address with Id</returns>
        public async Task<AddressResponse?> AddAddress(AddressAddRequest addressAddRequest, Guid userId)
        {
            _logger.LogDebug("AddAddress method in AddressAdderService. Parameters: addressAddRequest: {addressAddRequest.ToString()}, userId: {userId}", addressAddRequest.ToString(), userId);

            if (addressAddRequest == null || userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(addressAddRequest));
            }

            Address address = addressAddRequest.ToAddress(userId);

            await _addressRepository.AddAddress(address);

            return address.ToAddressResponse();
        }
    }
}
