using Microsoft.AspNetCore.Identity;
using ServiceContracts.DTO;

namespace ServiceContracts.Interfaces.Account
{
    public interface IAccountService
    {
        Task<SignInResult> SignInAsync(SignInDTO model);
        Task<IdentityResult> RegisterAsync(RegisterDTO model);
    }
}
