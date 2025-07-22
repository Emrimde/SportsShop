using Microsoft.AspNetCore.Identity;
using SportsShop.Core.ServiceContracts.DTO.AccountDto;
using System.Security.Claims;

namespace SportsShop.Core.ServiceContracts.Interfaces.IAccount;
public interface IAccountService
{
    Task<SignInResult> SignInAsync(SignInDto model);
    Task<IdentityResult> RegisterAsync(RegisterDto model);
    Guid GetUserId(ClaimsPrincipal user);
}
