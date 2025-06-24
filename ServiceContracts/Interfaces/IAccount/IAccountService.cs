using Microsoft.AspNetCore.Identity;
using ServiceContracts.DTO.AccountDto;

namespace ServiceContracts.Interfaces.Account
{
    public interface IAccountService
    {
        Task<SignInResult> SignInAsync(SignInDto model);
        Task<IdentityResult> RegisterAsync(RegisterDto model);
    }
}
