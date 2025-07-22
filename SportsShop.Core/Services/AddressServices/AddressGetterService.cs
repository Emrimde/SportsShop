using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.AddressDto;
using SportsShop.Core.ServiceContracts.Interfaces.IAddress;

namespace SportsShop.Core.Services.AddressServices
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
        
        public async Task<AddressResponse?> GetAddressById(int id, Guid userId)
        {
            Address? address = await _addressRepository.GetAddressById(id);
            if (address == null || address.UserId != userId)
            {
                return null;
            }

            return address.ToAddressResponse();
        }

        public async Task<IReadOnlyList<AddressResponse>> GetAllAddresses(Guid userId)
        {
            IEnumerable<Address> addresses = await _addressRepository.GetAllAddresses(userId);
            return addresses.Select(item => item.ToAddressResponse()).ToList();
        }

        public bool IsAddressProvided(AddressAddRequest addressAddRequest)
        {
            if (!string.IsNullOrEmpty(addressAddRequest.ZipCode) || addressAddRequest.CountryId > 0 || !string.IsNullOrEmpty(addressAddRequest.Street) || !string.IsNullOrEmpty(addressAddRequest.City))
            {
                return true;
            }

            return false;
        }
    }
}
