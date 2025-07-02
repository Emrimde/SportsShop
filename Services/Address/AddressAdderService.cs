using Entities.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public AddressAdderService(IAddressRepository addressRepository, UserManager<User> userManager)
        {
            _addressRepository = addressRepository;
            _userManager = userManager;
        }
       
        /// <summary>
        /// Add to the database a new address for the specific user.
        /// </summary>
        /// <param name="model">Address</param>
        /// <param name="userId"></param>
        /// <returns>Address with Id</returns>
        public async Task<AddressResponse?> AddAddress(AddressAddRequest model, Guid userId)
        {
            User? user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null || model == null)
            {
                return null;
            }

            Address address = model.ToAddress(user.Id);

            await _addressRepository.AddAddress(address);

            return address.ToAddressResponse();
        }
    }
}
