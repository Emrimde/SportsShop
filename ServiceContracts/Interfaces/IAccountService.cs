using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

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
