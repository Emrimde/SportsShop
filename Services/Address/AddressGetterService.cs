using Entities.Models;
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
        

        public AddressGetterService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        
        public async Task<AddressResponse?> GetAddressById(int? id)
        {
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
            return _addressRepository.GetAllAddresses(userId).Select(item => item.ToAddressResponse()).ToList();
        }

        public bool IsAddressProvided(AddressAddRequest addressAddRequest)
        {
            if (!string.IsNullOrEmpty(addressAddRequest.ZipCode) || !string.IsNullOrEmpty(addressAddRequest.Country) || !string.IsNullOrEmpty(addressAddRequest.Street) || !string.IsNullOrEmpty(addressAddRequest.City))
            {
                return true;
            }

            return false;
        }
    }
}
