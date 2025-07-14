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

        public async Task<IReadOnlyList<AddressResponse>> GetAllAddresses(string userId)
        {
            if (userId == null)
            { 
                throw new ArgumentNullException(nameof(userId), "User ID cannot be null.");
            }

            Guid userGuid = Guid.Parse(userId);
            IEnumerable<Address> addresses = await _addressRepository.GetAllAddresses(userGuid);
            return addresses.Select(item => item.ToAddressResponse()).ToList();
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
