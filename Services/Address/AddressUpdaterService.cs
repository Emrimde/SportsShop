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
   
        public async Task<AddressResponse?> UpdateAddress(AddressUpdateRequest model, string userId)
        {
            Address? address = await _addressRepository.GetAddressById(model.Id);
            if(address == null || address.UserId != Guid.Parse(userId))
            {
                return null;
            }
            Address? updatedAddress = await _addressRepository.UpdateAddress(model.ToAddress());
            return updatedAddress!.ToAddressResponse();
        }
    }
}
