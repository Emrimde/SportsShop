using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.Interfaces.IAddress;

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
   
        public async Task UpdateAddress(AddressUpdateRequest model)
        {
            Address? address = await _context.Addresses.FindAsync(model.Id);
            address!.Country = model.Country;
            address.City = model.City;
            address.Street = model.Street;
            address.ZipCode = model.ZipCode;
            await _context.SaveChangesAsync();
        }
    }
}
