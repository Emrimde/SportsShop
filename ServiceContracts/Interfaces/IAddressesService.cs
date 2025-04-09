using Entities.Models;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces
{
    public interface IAddressesService
    {
        Task<bool> AddAddress(AddressDTO model, string UserId);
        Task<List<Address>> ShowAddresses(Guid userId);
        Task<bool> DeleteAddress(int id);
        Task<Address> GetAddress(int id);
        Task<bool> EditAddress(AddressDTO model);
    }
}
