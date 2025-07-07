using Entities.Models;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.Interfaces.IAddress;

namespace Services
{
    /// <summary>
    /// Its a service for managing user's addresses. They can input their address, edit it, delete it and show all addresses before order time.
    /// </summary>
    public class AddressUpdaterService : IAddressUpdaterService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressUpdaterService> _logger;    
        
        public AddressUpdaterService(IAddressRepository addressRepository, ILogger<AddressUpdaterService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }
   
        public async Task<AddressResponse> UpdateAddress(AddressUpdateRequest model)
        {
            _logger.LogDebug("UpdateAddress action method. Parameter: model: {model}", model.ToString());

            Address updatedAddress = await _addressRepository.UpdateAddress(model.ToAddress());
            return updatedAddress.ToAddressResponse();
        }
    }
}
