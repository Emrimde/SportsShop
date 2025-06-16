using Entities.Models;
using ServiceContracts.DTO;

namespace ServiceContracts.Interfaces.IAddress
{
    public interface IAddressDeleterService
    {
        Task<bool> DeleteAddress(int id);  
    }
}
