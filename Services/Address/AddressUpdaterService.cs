using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ServiceContracts.DTO;
using ServiceContracts.Interfaces.IAddress;
using Services.IAddress;

namespace Services
{
    /// <summary>
    /// Its a service for managing user's addresses. They can input their address, edit it, delete it and show all addresses before order time.
    /// </summary>
    public class AddressUpdaterService : IAddressUpdaterService
    {
        private readonly SportsShopDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IAddressGetterService _addressGetterService;

        public AddressUpdaterService(SportsShopDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager, IAddressGetterService addressGetterService)
        {
            _context = dbContext;
            _userManager = userManager;
            _addressGetterService = addressGetterService;
        }
      
        public async Task<bool> EditAddress(AddressDTO model)
        {
            Address? address = await _addressGetterService.GetAddress(model.Id);
            if (address == null)
            {
                return false;
            }
            address.Country = model.Country;
            address.City = model.City;
            address.Street = model.Street;
            address.ZipCode = model.ZipCode;
            address.EditDate = DateTime.UtcNow;
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
