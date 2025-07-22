using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.AddressDto;
using SportsShop.Core.ServiceContracts.Interfaces.IAddress;

namespace SportsShop.Core.Services.AddressServices
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
   
        public async Task<AddressResponse?> UpdateAddress(AddressUpdateRequest model, Guid userId)
        {
            Address? address = await _addressRepository.GetAddressById(model.Id);
            if(address == null || address.UserId != userId)
            {
                return null;
            }
            Address? updatedAddress = await _addressRepository.UpdateAddress(model.ToAddress());

            return updatedAddress!.ToAddressResponse();
        }
    }
}
