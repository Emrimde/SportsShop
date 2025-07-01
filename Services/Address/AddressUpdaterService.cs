using Entities.Models;
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
        
        public AddressUpdaterService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
   
        public async Task UpdateAddress(AddressUpdateRequest model)
        {
            Address address = model.ToAddress();
            await _addressRepository.UpdateAddress(address);
        }
    }
}
