using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.Domain.RepositoryContracts;
public interface IAddressRepository
{
    Task<Address> AddAddress(Address model);
    Task<bool> DeleteAddress(Address model);
    Task<Address?> GetAddressById(int? id);
    Task<IEnumerable<Address>> GetAllAddresses(Guid userId);
    Task<int> GetAddressId(int id);
    Task<Address?> UpdateAddress(Address model);
}
