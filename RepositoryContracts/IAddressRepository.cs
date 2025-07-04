using Entities.Models;

namespace RepositoryContracts
{
    public interface IAddressRepository
    {
        Task<Address> AddAddress(Address model);
        Task<bool> DeleteAddress(int id);
        Task<Address?> GetAddressById(int? id);
        IQueryable<Address> GetAllAddresses(Guid userId);
        Task<int> GetAddressId(int id);
        Task<Address> UpdateAddress(Address model);
    }
}
