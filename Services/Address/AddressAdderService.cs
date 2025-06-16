using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ServiceContracts.DTO;
using ServiceContracts.Interfaces.IAddress;

namespace Services
{
    /// <summary>
    /// Its a service for managing user's addresses. They can input their address, edit it, delete it and show all addresses before order time.
    /// </summary>
    public class AddressAdderService : IAddressAdderService
    {
        private readonly SportsShopDbContext _context;
        private readonly UserManager<User> _userManager;

        public AddressAdderService(SportsShopDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = dbContext;
            _userManager = userManager;
        }
        public async Task<int> AddAddress(AddressDTO model, string UserId)
        {
            User? user = await _userManager.FindByIdAsync(UserId.ToString());

            if (user == null)
            {
                return -1;
            }

            Address address = new Address
            {
                UserId = user.Id,
                Country = model.Country,
                City = model.City,
                Street = model.Street,
                ZipCode = model.ZipCode,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address.Id;
        }
       
    }
}
