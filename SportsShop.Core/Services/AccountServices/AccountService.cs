using Microsoft.AspNetCore.Identity;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.AccountDto;
using SportsShop.Core.ServiceContracts.Interfaces.IAccount;
using System.Security.Claims;

namespace SportsShop.Core.Services.AccountServices;
public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ICartRepository _cartRepository;
    
    public AccountService( UserManager<User> userManager, SignInManager<User> signInManager, ICartRepository cartRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _cartRepository = cartRepository;
    }

    public Guid GetUserId(ClaimsPrincipal user)
    {
        string? id = _userManager.GetUserId(user);
        Guid guid;
        if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out guid))
        {
            throw new UnauthorizedAccessException("Unable to get a valid user id");
        }

        return Guid.Parse(id);
    }

    public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
    {
        User user = registerDto.ToUser();

        IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return result;
        }

        await _signInManager.SignInAsync(user, isPersistent: true);
        var cart = new Cart
        {
            UserId = user.Id,
        };

        await _cartRepository.AddCartToTheUser(cart);
        return result;
    }

    public async Task<SignInResult> SignInAsync(SignInDto signInDto)
    {
        var result = await _signInManager.PasswordSignInAsync(signInDto.Email, signInDto.Password, isPersistent: false, lockoutOnFailure: true);
        return result;
    }
}
