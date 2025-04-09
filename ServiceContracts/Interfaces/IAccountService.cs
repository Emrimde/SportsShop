using Microsoft.AspNetCore.Identity;
using ServiceContracts.DTO;

namespace ServiceContracts.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> SignInAsync(SignInDTO model);
        Task<IdentityResult> RegisterAsync(RegisterDTO model);
        //Task<bool> AddAddress(AddressDTO model, string UserId);
        //Task<List<Address>> ShowAddresses(Guid userId);
        //Task<bool> DeleteAddress(int id);
        //Task<Address> GetAddress(int id);
        //Task<bool> EditAddress(AddressDTO model);
    }
}
