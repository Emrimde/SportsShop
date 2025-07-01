using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        
        public AddressGetterService(IAddressRepository addressRepository, SignInManager<User> signInManager)
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

        public async Task<int> GetAddressId(int id)
        {
            return await _addressRepository.GetAddressId(id);
        }

        public async Task<List<AddressResponse>> GetAllAddresses(Guid userId)
        {
            return await _addressRepository.GetAllAddresses(userId).Select(item => item.ToAddressResponse()).ToListAsync();
        }

        public bool IsAddressProvided(AddressAddRequest request)
        {
            if(!string.IsNullOrEmpty(request.ZipCode) || !string.IsNullOrEmpty(request.Country) || !string.IsNullOrEmpty(request.Street) || !string.IsNullOrEmpty(request.City))
            {
                return true;
            }

            return false;
        }
    }
}
