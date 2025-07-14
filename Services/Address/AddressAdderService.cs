using Entities.Models;
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
        public AddressAdderService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        /// <summary>
        /// Add to the database a new address for the specific user.
        /// </summary>
        /// <param name="addressAddRequest">Address</param>
        /// <param name="userId"></param>
        /// <returns>Address with Id</returns>
        public async Task<AddressResponse?> AddAddress(AddressAddRequest addressAddRequest, string userId)
        {
            if (addressAddRequest == null || string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException();
            }

            Guid userIdGuid = Guid.Parse(userId);
            Address address = addressAddRequest.ToAddress(userIdGuid);
            await _addressRepository.AddAddress(address);

            return address.ToAddressResponse();
        }
    }
}
