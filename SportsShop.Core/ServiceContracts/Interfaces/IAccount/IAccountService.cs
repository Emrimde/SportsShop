using Microsoft.AspNetCore.Identity;
using ServiceContracts.DTO.AccountDto;
using System.Security.Claims;

namespace ServiceContracts.Interfaces.Account
{
    public interface IAccountService
    {
        Task<SignInResult> SignInAsync(SignInDto model);
        Task<IdentityResult> RegisterAsync(RegisterDto model);
        Guid GetUserId(ClaimsPrincipal user);
    }
}
