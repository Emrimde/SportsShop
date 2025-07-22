using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.Interfaces.IAddress;

namespace SportsShop.Core.Services.AddressServices;

/// <summary>
/// Its a service for managing user's addresses. They can input their address, edit it, delete it and show all addresses before order time.
/// </summary>
public class AddressDeleterService : IAddressDeleterService
{
    private readonly IAddressRepository _addressRepository;
    
    public AddressDeleterService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<bool> DeleteAddress(int id, Guid userId)
    {
        Address? address = await _addressRepository.GetAddressById(id);
        if (address == null || address.UserId != userId)
        {
            return false;
        }

        return await _addressRepository.DeleteAddress(address);
    }
}
