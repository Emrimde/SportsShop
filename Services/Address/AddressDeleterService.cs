using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.Interfaces.IAddress;

namespace Services
{
    /// <summary>
    /// Its a service for managing user's addresses. They can input their address, edit it, delete it and show all addresses before order time.
    /// </summary>
    public class AddressDeleterService : IAddressDeleterService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressDeleterService> _logger;

        public AddressDeleterService(IAddressRepository addressRepository, ILogger<AddressDeleterService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<bool> DeleteAddress(int id)
        {
            _logger.LogDebug("DeleteAddress method. Parameter: id: {id}", id);
            
            return await _addressRepository.DeleteAddress(id);
        }
    }
}
